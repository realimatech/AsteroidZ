using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapBehaviour : MonoBehaviour
{
    private void Update()
    {
        HandleCameraWrap();
    }

    private void HandleCameraWrap()
    {
        transform.position = CameraWrapper.Instance.HandleWrap(transform.position);
    }
}
