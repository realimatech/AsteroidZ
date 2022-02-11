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

        [SerializeField] SceneAsset _envScene;
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
            if (!_envScene) { Debug.LogError("No Environment Scene mentioned. Try check GameManager or builded scenes."); yield break; }
            var env = SceneManager.GetSceneByName(_envScene.name);
            if (env != null && env.buildIndex > -1)
            {
                yield return new WaitForEndOfFrame();
                if (SceneManager.GetActiveScene() != env) SceneManager.SetActiveScene(env);
            }
            else
            {
                SceneManager.LoadSceneAsync(_envScene.name, LoadSceneMode.Additive).completed += (op) =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(_envScene.name));
                };
            }
        }

        public void StartGameplay()
        {
            //LATER: FadeIn
            SceneManager.LoadSceneAsync(_envScene.name);

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