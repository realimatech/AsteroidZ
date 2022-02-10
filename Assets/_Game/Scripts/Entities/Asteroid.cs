using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    [CreateAssetMenu(fileName = "asteroid@", menuName = "AsteroidZ/New Asteroid", order = 0)]
    public class Asteroid : ScriptableObject
    {
        [SerializeField] float _constantForce = 50;
        [SerializeField] int _destructionScore = 70;
        [SerializeField] LayerMask _destructionLayers = ~0;
        [SerializeField] Asteroid[] _debris;

        public float ConstantForce { get => _constantForce; }
        public int DestructionScore { get => _destructionScore; }
        public LayerMask DestructionLayers { get => _destructionLayers; }

        public Asteroid Debri(int index) => _debris[index];
    }
}
