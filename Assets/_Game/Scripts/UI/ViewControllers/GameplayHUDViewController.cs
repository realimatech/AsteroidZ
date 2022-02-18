using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace realima.asterioidz
{
    public class GameplayHUDViewController : MonoBehaviour
    {
        [SerializeField] CloneElementHandler _lifeCounter;

        [SerializeField] Slider _scoreSlider;

        [SerializeField] TMP_Text _tmpScore;
        [SerializeField] TMP_Text _tmpHighScore;

        [SerializeField] Toggle _pauseToggle;

        private bool _gotHighScore;

        private void Awake()
        {
            _scoreSlider.value = 0;
            _scoreSlider.maxValue = GameManager.Instance.gameSave.HighScore;
            _tmpHighScore.text = "Record:" + _scoreSlider.maxValue.ToString("00");
            AudioManager.BGM.Playlist();
        }

        private void OnEnable()
        {
            GameplayManager.onGameplayPaused += TogglePause;
            GameplayManager.onScoreRaised += UpdateScore;
        }

        private void OnDisable()
        {
            GameplayManager.onGameplayPaused -= TogglePause;
            GameplayManager.onScoreRaised -= UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _tmpScore.text = score.ToString("0000");
            if(score > _scoreSlider.maxValue)
            {
                _scoreSlider.maxValue = score;
                _tmpHighScore.text = "Record:" + score.ToString("00");
                if (!_gotHighScore)
                {
                    _gotHighScore = true;
                    AudioManager.SFX.Play("Record");
                }
            }
            _scoreSlider.value = score;
        }

        public void UIPauseToggleChange(bool state)
        {
            TogglePause(state);
        }

        public void TogglePause(bool state)
        {
            if (_pauseToggle.isActiveAndEnabled)
                _pauseToggle.isOn = state;
        }
    }
}
