using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace realima.asterioidz
{
    public class PauseViewController : MonoBehaviour
    {


        public static void PopUp()
        {
            if (!SceneManager.GetSceneByName("PauseMenu").isLoaded)
            {
                AudioManager.SFX.Play("Pause");
                Time.timeScale = 0;
                SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
            }
        }

        public static void PopOut()
        {
            //LATER: Popout Animation
            AudioManager.SFX.Play("Resume");
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("PauseMenu");
        }

        public void Restart()
        {
            PopOut();
            GameManager.Instance.StartGameplay();
        }

        public void Menu()
        {
            PopOut();
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        }
    }
}
