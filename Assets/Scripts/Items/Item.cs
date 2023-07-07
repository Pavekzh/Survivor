using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private LayerMask canPickUpLayers;

    protected abstract void Execute(Collider2D founder);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool canPickUp = canPickUpLayers == (canPickUpLayers | (1 << collision.gameObject.layer));

        if (!canPickUp)
            return;

        Execute(collision);
        Destroy(gameObject);
    }
}