using System;
using UnityEngine;

namespace realima.asterioidz
{
    [CreateAssetMenu(fileName = "save@", menuName = "AsteroidZ/New Save", order = 0)]
    public class GameSave : ScriptableObject
    {
        public int playerLife = 5;

        public int HighScore { get; set; }

        public static GameSave Load(GameSave defaultSave = null)
        {
            var save = defaultSave ?? new GameSave();
            save.HighScore = PlayerPrefs.GetInt("HighScore");
            return save;
        }

        internal void Save()
        {
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }
    }
}