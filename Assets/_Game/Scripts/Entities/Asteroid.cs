using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    [CreateAssetMenu(fileName = "asteroid@", menuName = "AsteroidZ/New Asteroid", order = 0)]
    public class Asteroid : ScriptableObject
    {
        [SerializeField] int _level = 1;
        [SerializeField] float _startVelocityMin = 5;
        [SerializeField] float _startVelocityMax = 50;
        [SerializeField] float _constantForce = 50;
        [SerializeField] int _destructionScore = 70;
        [SerializeField] LayerMask _destructionLayers = ~0;
        [SerializeField] int _debris;

        public int Level { get => _level; }
        public float StartVelocityMin { get => _startVelocityMin; }
        public float StartVelocityMax { get => _startVelocityMax; }
        public float ConstantForce { get => _constantForce; }
        public int DestructionScore { get => _destructionScore; }
        public LayerMask DestructionLayers { get => _destructionLayers; }
        public int DebriCount { get => _debris;  }
    }
}
