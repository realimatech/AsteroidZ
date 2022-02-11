using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    public class AsteroidBehaviour : MonoBehaviour, IDestroyable
    {
        [SerializeField] Asteroid _data;
        public Asteroid Data => _data;

        public int DestroyInstance(IDestroyable destroyer)
        {
            //Bisect
            AsteroidSpawner.Pool.Hide(gameObject);
            return _data.DestructionScore;
        }
    }
}
