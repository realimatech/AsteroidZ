using UnityEngine;

namespace realima.asterioidz
{
    [CreateAssetMenu(fileName = "ship@", menuName = "AsteroidZ/New Ship", order = 0)]
    public class Ship : ScriptableObject
    {
        [SerializeField] float _acceleration = 10;
        [SerializeField] float _steering = 90;
        [Range(0, 1)]
        [SerializeField] float _damping = 0.1f;

        public float Acceleration { get => _acceleration; }
        public float Steering { get => _steering; }
        public float Damping { get => _damping; }
    }
}