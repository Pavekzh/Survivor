using System;
using UnityEngine;

public class CameraFollow:MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] Transform target;
    [SerializeField] float smoothFactor = 0.2f;

    private BoxCollider2D boundBox;

    private Vector2 minBounds { get => boundBox.bounds.min; }
    private Vector2 maxBounds { get => boundBox.bounds.max; }

    private float halfWidth;
    private float halfHeight;

    public void InitDependencies(BoxCollider2D boundBox)
    {
        this.boundBox = boundBox;
    }

    private void Start()
    {
        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    private void Update()
    {
        Vector2 newPosition = Vector2.Lerp(transform.position, target.position, 1 / smoothFactor * Time.deltaTime);
        float clampedX = Mathf.Clamp(newPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(newPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}

