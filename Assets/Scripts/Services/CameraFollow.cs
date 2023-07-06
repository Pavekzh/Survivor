using System;
using UnityEngine;

public class CameraFollow:MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothFactor = 0.2f;

    Vector3 offset;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 1 / smoothFactor * Time.deltaTime);
    }
}

