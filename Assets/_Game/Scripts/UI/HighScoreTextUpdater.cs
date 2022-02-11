using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace realima.asterioidz
{
    public class HighScoreTextUpdater : MonoBehaviour
    {
        [SerializeField] TMP_Text _tmp;
        [SerializeField] string _format;

        private void OnEnable()
        {
            GameManager.onGameLoaded += () => UpdateText(_format);
        }

        private void OnDisable()
        {
            GameManager.onGameLoaded -= () => UpdateText(_format);
        }

        private void Start()
        {
            UpdateText(_format);
        }

        private void UpdateText(string format)
        {
            if (_tmp && GameManager.Instance) _tmp.text = String.Format(format, GameManager.Instance.gameSave.HighScore.ToString("00"));
        }
    }
}