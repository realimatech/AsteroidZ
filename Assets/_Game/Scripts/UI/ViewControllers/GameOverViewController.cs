using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace realima.asterioidz
{
    public class GameOverViewController : MonoBehaviour
    {
        [SerializeField] TMP_Text _tmpScore;

        private void Start()
        {
            if (_tmpScore) _tmpScore.text = GameplayManager.Instance.ScoreCount.ToString("0000");
            int score = GameplayManager.Instance.ScoreCount;
            if(GameManager.Instance.gameSave.HighScore < score) GameManager.Instance.gameSave.HighScore = score;
            GameManager.Instance.gameSave.Save();
        }

        public static void PopUp()
        {
            if(!SceneManager.GetSceneByName("GameOverMenu").isLoaded)
                SceneManager.LoadSceneAsync("GameOverMenu", LoadSceneMode.Additive);
        }

        public void PopOut()
        {
            //LATER: Popout Animation
            SceneManager.UnloadSceneAsync("GameOverMenu");
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