using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace realima.asterioidz
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] AudioSource _source;
        [SerializeField] List<AudioSet> _audioSets;

        private AudioSet[] _playlist;
        private int _latestIndex;
        private AudioSet _latestSet;
        private bool _isPlaylist;

        private void LateUpdate()
        {
            if (_isPlaylist)
            {
                if (_source.isPlaying == false)
                {
                    NextTrack();
                }
            }
        }

        public void Play(string clipKey)
          {
            if (_latestSet == null || clipKey != _latestSet.clipKey)
            {
                _latestSet = _audioSets.Where(p => p.clipKey == clipKey).FirstOrDefault();
                if (_latestSet == null) throw new ArgumentException(name + " clip key \"" + clipKey + "\" wasn't found");
                _source.clip = _latestSet.clipFile;
            }
            _source.Play();
        }

        public void Play()
        {
            _isPlaylist = false;
            _source.Play();
        }

        public void Playlist(int index = -1, params string[] tracks)
        {
            _isPlaylist = true;
            if (tracks == null || tracks.Length == 0)
            {
                _playlist = _audioSets.ToArray();
            }
            else
            {
                _playlist = _audioSets.ToArray(); //TO FIX
            }
            _latestIndex = index > -1 ? index : UnityEngine.Random.Range(0, _playlist.Length);
            _latestSet = _playlist[_latestIndex];
            _source.clip = _latestSet.clipFile;
            _source.Play();
        }

        private void NextTrack()
        {
            _isPlaylist = true;
            _latestIndex = (_latestIndex + 1) % _playlist.Length;
            _latestSet = _playlist[_latestIndex];
            _source.clip = _latestSet.clipFile;
            _source.Play();
        }
    }
}
