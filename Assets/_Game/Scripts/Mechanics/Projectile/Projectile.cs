using UnityEngine;

namespace realima.asterioidz
{
    [CreateAssetMenu(fileName = "projectile@", menuName = "AsteroidZ/New Projectile", order = 0)]
    public class Projectile : ScriptableObject
    {
        [SerializeField] float _constantForce = 15;
        /// <summary>
        /// Time the bullet travels before it disappears without explosion. Time unit in seconds.
        /// </summary>
        [SerializeField] float _shotDuration = 3;
        /// <summary>
        /// Time for the ship to spawn a new bullet. Time unit in seconds.
        /// </summary>
        [SerializeField] float _shotCooldown = .3f;

        public float ConstantForce => _constantForce;
        public float ShotDuration => _shotDuration;
        public float ShotCooldown => _shotCooldown;
    }
}