using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        [SerializeField] AudioHandler _bgm;
        [SerializeField] AudioHandler _sfx;

        public static AudioHandler BGM => _instance._bgm;
        public static AudioHandler SFX => _instance._sfx;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _instance.transform.parent = null;
                DontDestroyOnLoad(_instance.gameObject);
            }
            else if (_instance != this)
            {
                var lastInstance = _instance;
                _instance = this;
                _instance.transform.parent = null;
                Destroy(lastInstance.gameObject);
                DontDestroyOnLoad(_instance.gameObject);
            }
        }
    }
}
