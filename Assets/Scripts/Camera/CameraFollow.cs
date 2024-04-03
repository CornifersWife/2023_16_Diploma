using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private Transform target;

    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 cameraPosition = target.position + _offset;
        transform.position = Vector3.Lerp (transform.position, cameraPosition, smoothing * Time.deltaTime);
    }
}
