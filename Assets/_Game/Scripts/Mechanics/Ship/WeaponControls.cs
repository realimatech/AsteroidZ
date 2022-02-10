using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace realima.asterioidz
{
    public class WeaponControls : MonoBehaviour
    {
        [SerializeField] Rigidbody _body;
        [SerializeField] Transform _bulletSpot;

        private PoolManager _projectilePool;
        private float _shotCooldown;

        private void Awake()
        {
            if (!_body) _body.GetComponentInParent<Rigidbody>();
            if (!_body) { Debug.LogError("No reference to Rigidbody. Not event as parent of " + GetType().Name); }

            _projectilePool = FindObjectsOfType<PoolManager>().Where(p => p.name.StartsWith("Projectile")).FirstOrDefault();
            if (!_projectilePool) { Debug.LogError("No pool associated with projectiles were found in scene. Try start the name of the pool with \"Projectile\""); }
        }

        private void Update()
        {
            if (_shotCooldown > 0) _shotCooldown -= Time.deltaTime;
        }

        public void Shoot(CallbackContext callback)
        {
            if (enabled && callback.performed && _shotCooldown <= 0)
            {
                var projectile = _projectilePool.Show(0, _bulletSpot ? _bulletSpot.position : transform.position,
                    Quaternion.LookRotation(_bulletSpot ? _bulletSpot.forward : transform.forward)).GetComponent<ProjectileBehaviour>();
                projectile.SetPool(_projectilePool);
                projectile.Setup(_body.velocity.magnitude);
                _shotCooldown = projectile.Data.ShotCooldown;
            }
        }
    }
}
