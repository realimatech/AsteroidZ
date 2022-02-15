using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace realima.asterioidz
{
    public class GameplayManager : MonoBehaviour
    {
        private static GameplayManager _instance;
        public static GameplayManager Instance => _instance;

        [SerializeField] ShipBehaviour _playerShip;
        public ShipBehaviour PlayerShip => _playerShip;

        [SerializeField] Level _level;

        public delegate void PlayerShipDestroyEvent();
        public static PlayerShipDestroyEvent onPlayerShipDestroyed;
        public delegate void ScoreRaiseEvent(int score);
        public static ScoreRaiseEvent onScoreRaised;
        public delegate void GameplayPauseEvent(bool state);
        public static GameplayPauseEvent onGameplayPaused;

        public float EnlapsedMatchTime { get; private set; }
        public bool IsPaused { get; private set; }
        public bool DidStart { get; private set; }
        public int LifeCount { get; private set; }
        public int ScoreCount { get; private set; }


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                LifeCount = GameManager.Instance.gameSave.playerLife;
            }
            else if (_instance != this)
            {
                Destroy(_instance.gameObject);
            }
        }

        private void Start()
        {
            if (SceneManager.GetSceneByName("MainMenu").buildIndex == -1)
            {
                StartCoroutine(SpawnShip(Vector3.zero, () =>
                {
                    //OnSpawnAnimationEnd
                    SceneManager.LoadSceneAsync("GameplayHUD", LoadSceneMode.Additive);
                    DidStart = true;
                }));
            }
        }

        public void PlayerShipDestroyed()
        {
            LifeCount--;
            if (LifeCount == 0)
            {
                //Fix
                SceneManager.UnloadSceneAsync("GameplayHUD");
                GameOverViewController.PopUp();
            }
            else
            {
                StartCoroutine(SpawnShip(Vector3.zero, delay:1f));
            }
            onPlayerShipDestroyed?.Invoke();
        }

        private IEnumerator SpawnShip(Vector3 spawnPosition, UnityAction onShipSpawned = null, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            _playerShip.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _playerShip.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            _playerShip.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame(); //Await spawn animation;
            onShipSpawned?.Invoke();
            yield break;
        }

        public void AddScore(int increment)
        {
            ScoreCount += increment;
            onScoreRaised?.Invoke(ScoreCount);
        }

        private void OnApplicationPause(bool pause)
        {
            if(pause)
                PauseGameplay(true);
        }

        public void PauseGameplay()
        {
            PauseGameplay(!IsPaused);
        }

        public void PauseGameplay(bool pause)
        {
            IsPaused = pause;
            onGameplayPaused?.Invoke(IsPaused);
            if (pause)
            {
                PauseViewController.PopUp();
            }
            else
            {
                PauseViewController.PopOut();
            }
        }
    }
}