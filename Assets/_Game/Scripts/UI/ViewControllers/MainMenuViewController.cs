using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz {
    public class MainMenuViewController : MonoBehaviour
    {
        public void Play()
        {
            GameManager.Instance.StartGameplay();
        }
    }
}