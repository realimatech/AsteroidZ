using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace realima.asterioidz
{
    /// <summary>
    /// Responsible for the control of a ship's body. It depends on a reference of a parent Rigidbody
    /// </summary>
    public class ShipControls : MonoBehaviour
    {
        [SerializeField] ShipBehaviour _ship;
        [SerializeField] Rigidbody _body;

        private bool _isAccelerating;
        //private bool _isBreaking;
        private Vector3 _steerSide;


        private void Awake()
        {
            if (!_body) _body = GetComponentInParent<Rigidbody>();
            if (!_body) { Debug.LogError("Rigidbody not referenced. Not even found on a parent GameObject"); return; }

            if (!_ship) _ship = _body.GetComponent<ShipBehaviour>();
            if (!_ship) { Debug.LogError("ShipBehavior not referenced. Not even found as a sibling from Rigidbody."); return; }
        }

        void FixedUpdate()
        {
            if (enabled)
            {
                UpdateSteering();
                UpdatePhysics();
            }
        }

        private void UpdateSteering()
        {
            Quaternion delta = Quaternion.Euler(_steerSide * _ship.FinalSteering * Time.fixedDeltaTime);
            _body.MoveRotation(_body.rotation * delta);
        }

        private void UpdatePhysics()
        {
            if (_isAccelerating)
            {
                Vector3 finalForce = _body.transform.forward * _ship.FinalAcceleration * 100 * Time.fixedDeltaTime;
                _body.AddForce(finalForce, ForceMode.Acceleration);
            }
        }

        #region InputCallbacks
        public void Accelerate(CallbackContext callback)
        {
            _isAccelerating = callback.ReadValueAsButton();
        }

        //public void Break(CallbackContext callback)
        //{
        //    _isBreaking = callback.ReadValueAsButton();
        //}

        public void Steer(CallbackContext callback)
        {
            _steerSide = new Vector3(0, callback.ReadValue<float>(), 0);
        }
        #endregion
    }
}
