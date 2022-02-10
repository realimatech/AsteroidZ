using UnityEngine;

namespace realima.asterioidz
{
    public class GameplayManager : MonoBehaviour
    {
        private static GameplayManager _instance;
        public static GameplayManager Instance => _instance;

        public float EnlapsedMatchTime { get; private set; }

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
            }else if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}