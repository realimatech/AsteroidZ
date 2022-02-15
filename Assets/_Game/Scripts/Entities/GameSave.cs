using System;
using UnityEngine;

namespace realima.asterioidz
{
    [CreateAssetMenu(fileName = "save@", menuName = "AsteroidZ/New Save", order = 0)]
    public class GameSave : ScriptableObject
    {
        public int playerLife = 5;

        public int HighScore = 1000;

        public static GameSave Load(GameSave defaultSave = null)
        {
            var save = (GameSave)defaultSave?.MemberwiseClone();
            save = save ?? new GameSave();
            save.HighScore = PlayerPrefs.GetInt("HighScore", save.HighScore);
            return save;
        }

        internal void Save()
        {
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }
    }
}