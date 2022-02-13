using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    public class AsteroidBehaviour : MonoBehaviour, IDestroyable
    {
        [SerializeField] Asteroid _data;
        public Asteroid Data => _data;

        public AsteroidSpawner spawner { get; set; }

        public int DestroyInstance(IDestroyable destroyer)
        {
            //Bisect
            AsteroidSpawner.Pool.Hide(gameObject);
            SpawnAsteroidDebris();
            return _data.DestructionScore;
        }

        private void SpawnAsteroidDebris()
        {
            if (_data.Level - 1 == 0) return;
            for (int i = 0; i < _data.DebriCount; i++)
            {
                spawner.Spawn(_data.Level - 1, transform.position);
            }
        }
    }
}
