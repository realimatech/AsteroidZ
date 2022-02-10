using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWrapper : MonoBehaviour
{
    static CameraWrapper _instance;
    public static CameraWrapper Instance => _instance ?? FindObjectOfType<CameraWrapper>();

    [SerializeField] Color _boundsColor = Color.red;
    [SerializeField] Bounds _wrapBounds = new Bounds(Vector3.zero, Vector3.one);

    public Bounds WrapBounds { get => _wrapBounds; }

    private void Awake()
    {
        _instance = this;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _boundsColor;
        Gizmos.DrawWireCube(_wrapBounds.center, _wrapBounds.size);
    }

    public Vector3 HandleWrap(Vector3 position)
    {
        if (_wrapBounds.size == Vector3.zero || _wrapBounds.Contains(position)) return position;
        float newX = position.x;
        float newY = position.y;
        float newZ = position.z;
        if (_wrapBounds.min.x > position.x || _wrapBounds.max.x < position.x) newX = -position.x;
        if (_wrapBounds.min.y > position.y || _wrapBounds.max.y < position.y) newY = -position.y;
        if (_wrapBounds.min.z > position.z || _wrapBounds.max.z < position.z) newZ = -position.z;
        return new Vector3(newX, newY, newZ);
    }
}
