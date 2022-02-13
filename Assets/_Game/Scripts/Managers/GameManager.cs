using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace realima.asterioidz
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        [SerializeField] string _envSceneName;
        [SerializeField] GameSave _startingSave;

        public GameSave gameSave { get; set; }

        public delegate void GameLoadEvent();
        public static GameLoadEvent onGameLoaded;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                gameObject.transform.parent = null;
                DontDestroyOnLoad(gameObject);
                LoadPersistence();
                StartCoroutine(SetupEnvironment());
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator SetupEnvironment()
        {
            if (string.IsNullOrEmpty(_envSceneName)) { Debug.LogError("No Environment Scene Name mentioned. Try check GameManager or builded scenes."); yield break; }
            var env = SceneManager.GetSceneByName(_envSceneName);
            if (env != null && env.buildIndex > -1)
            {
                yield return new WaitForEndOfFrame();
                if (SceneManager.GetActiveScene() != env) SceneManager.SetActiveScene(env);
            }
            else
            {
                SceneManager.LoadSceneAsync(_envSceneName, LoadSceneMode.Additive).completed += (op) =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(_envSceneName));
                };
            }
        }

        public void StartGameplay()
        {
            //LATER: FadeIn
            SceneManager.LoadSceneAsync(_envSceneName);

            //LATER: FadeOut
        }

        private void LoadPersistence()
        {
            gameSave = GameSave.Load(_startingSave);
            onGameLoaded?.Invoke();
        }

        public void SetHighScore(int newScore)
        {
            gameSave.HighScore = newScore;
            gameSave.Save();
            onGameLoaded?.Invoke();
        }
    }
}