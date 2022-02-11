using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

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
                DestroyInstance(this);
                destroyable.DestroyInstance();
            }
        }

        public int DestroyInstance(IDestroyable destroyer = null)
        {
            if (destroyer != null && destroyer == (IDestroyable)this)
            {
                Debug.Log("Destroyed Ship");
                GameplayManager.Instance.PlayerShipDestroyed();
                //LATER: Spawn DestructionParticles
                gameObject.SetActive(false);
            }
            return 0;
        }

        public void TriggerPause(CallbackContext context)
        {
            if(context.performed)
                GameplayManager.Instance.PauseGameplay();
        }
    }
}
