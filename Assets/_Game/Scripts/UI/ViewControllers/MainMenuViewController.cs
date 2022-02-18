using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz {
    public class MainMenuViewController : MonoBehaviour
    {

        private void Start()
        {
            AudioManager.BGM.Play();
        }

        public void Play()
        {
            GameManager.Instance.StartGameplay();
            AudioManager.SFX.Play("BT_Play");
        }
    }
}