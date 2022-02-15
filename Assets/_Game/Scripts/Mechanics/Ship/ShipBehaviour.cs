using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

namespace realima.asterioidz
{
    public class ShipBehaviour : MonoBehaviour, IDestroyable
    {
        [SerializeField] Ship _data;
        [SerializeField] GameObject _explosionParticle;
        [SerializeField] UnityEvent _onShipDestroyed;

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
                _explosionParticle.transform.position = transform.position;
                _explosionParticle.SetActive(true);
                _onShipDestroyed?.Invoke();
                gameObject.SetActive(false);
            }
            return 0;
        }

        public void TriggerPause(CallbackContext context)
        {
            if (context.phase == UnityEngine.InputSystem.InputActionPhase.Started)
                GameplayManager.Instance.PauseGameplay();
        }
    }
}
