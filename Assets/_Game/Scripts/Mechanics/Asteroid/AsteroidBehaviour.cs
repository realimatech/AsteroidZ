using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    public class AsteroidBehaviour : MonoBehaviour, IDestroyable
    {
        [SerializeField] Asteroid _data;
        public Asteroid Data => _data;

        public void DestroyInstance()
        {
            AsteroidSpawner.Pool.Hide(gameObject);
        }
    }
}
