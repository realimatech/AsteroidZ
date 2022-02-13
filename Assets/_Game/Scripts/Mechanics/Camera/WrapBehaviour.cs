using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.asterioidz
{
    public class WrapBehaviour : MonoBehaviour
    {
        private void LateUpdate()
        {
            HandleCameraWrap();
        }

        private void HandleCameraWrap()
        {
            transform.position = CameraWrapper.Instance.HandleWrap(transform.position);
        }
    }
}