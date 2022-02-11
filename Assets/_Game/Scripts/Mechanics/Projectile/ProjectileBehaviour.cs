using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileBehaviour : MonoBehaviour, IDestroyable
    {
        [SerializeField] Projectile _data;
        [SerializeField] ConstantForce _constantForce;

        public Projectile Data => _data;

        private PoolManager _pool;
        private Rigidbody _body;
        private Coroutine _shotDurationCoroutine;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();

            if (_constantForce) _constantForce.relativeForce = Vector3.forward * _data.ConstantForce;
        }

        public void Setup(float startVelocity)
        {
            _body.velocity = transform.forward * startVelocity;
            _shotDurationCoroutine = StartCoroutine(ShotDurationCountAsync(_data.ShotDuration));
        }

        public IEnumerator ShotDurationCountAsync(float secs)
        {
            yield return new WaitForSeconds(secs);
            DestroyInstance();
        }

        private void OnTriggerEnter(Collider col)
        {
            var destroyable = col.gameObject.GetComponent<IDestroyable>();
            if(destroyable != null)
            {
                int destructionValue = destroyable.DestroyInstance(this);
                GameplayManager.Instance.AddScore(destructionValue);
            }
            DestroyInstance(this);
        }

        public int DestroyInstance(IDestroyable destroyer = null)
        {
            _pool.Hide(gameObject);
            StopCoroutine(_shotDurationCoroutine);
            return 0;
        }

        internal void SetPool(PoolManager projectilePool)
        {
            _pool = projectilePool;
        }
    }
}
