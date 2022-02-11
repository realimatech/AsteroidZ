using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    public class ShipBehaviour : MonoBehaviour, IDestroyable
    {
        [SerializeField] Ship _data;

        public float FinalSteering { get => _data.Steering; }
        public float FinalAcceleration { get => _data.Acceleration; }

        private void OnTriggerEnter(Collider other)
        {
            var destroyable = other.GetComponentInParent<IDestroyable>();
            if (destroyable != null)
            {
                DestroyInstance();
                destroyable.DestroyInstance();
            }
        }

        public void DestroyInstance()
        {
            Debug.Log("Destroyed Ship");
            GameplayManager.Instance.PlayerShipDestroyed();
            //LATER: Spawn DestructionParticles
            gameObject.SetActive(false);
        }
    }
}
