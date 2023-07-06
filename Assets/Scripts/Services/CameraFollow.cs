using System;
using UnityEngine;

public class CameraFollow:MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothFactor = 2;

    Vector3 offset;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
    }
}

