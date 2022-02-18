using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace realima.asterioidz
{
    [RequireComponent(typeof(PoolManager))]
    public class AsteroidSpawner : MonoBehaviour
    {
        private static PoolManager _pool;
        public static PoolManager Pool => _pool;

        /// <summary>
        /// It defines how many asteroids are spawned at the beginning of the default match.
        /// </summary>
        [SerializeField] int _startingAsteroids = 10;
        /// <summary>
        /// It controls the asteroid average speed based on start velocity attributes. 
        /// </summary>
        [SerializeField] AnimationCurve _startVelocityCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        /// <summary>
        /// The highest durantion of a match with significant curve evaluation.
        /// </summary>
        [SerializeField] float _startVelocityLimitTime = 60;


        // Start is called before the first frame update
        void Awake()
        {
            _pool = GetComponent<PoolManager>();
        }

        void Start()
        {
            for (int i = 0; i < _startingAsteroids; i++)
            {
                Spawn(3);
            }
        }

        public void Spawn(int index)
        {
            var cameraBounds = CameraWrapper.Instance.WrapBounds;
            Vector3 pos = RandomSpotInBounds(cameraBounds);
            var fromSides = Random.Range(0, 2) == 0;

            if (fromSides)
                pos.x = pos.x > cameraBounds.center.x ? cameraBounds.max.x : cameraBounds.min.x;
            else
                pos.z = pos.z > cameraBounds.center.z ? cameraBounds.max.z : cameraBounds.min.z;

            Spawn(index, pos);
        }

        public void Spawn(int asteroidType, Vector3 position)
        {
            AsteroidBehaviour asteroid = _pool.Show(asteroidType - 1, position).GetComponent<AsteroidBehaviour>();
            asteroid.spawner = this;
            asteroid.GetComponent<Rigidbody>().velocity = (RandomSpotInBounds(CameraWrapper.Instance.WrapBounds) - asteroid.transform.position).normalized * SpawnSpeed(asteroid.Data);
        }

        private static Vector3 RandomSpotInBounds(Bounds cameraBounds)
        {
            return new Vector3(
                Random.Range(cameraBounds.min.x, cameraBounds.max.x),
                0,
                Random.Range(cameraBounds.min.z, cameraBounds.max.z)
                );
        }

        public float SpawnSpeed(Asteroid asteroid)
        {
            return asteroid.StartVelocityMin + (_startVelocityCurve.Evaluate(GameplayManager.Instance.EnlapsedMatchTime / _startVelocityLimitTime) * asteroid.StartVelocityMax);
        }

    }
}