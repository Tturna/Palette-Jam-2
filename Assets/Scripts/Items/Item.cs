// This is a script for props that can be picked up and thrown

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Item : MonoBehaviour
{
    // General
    public enum ItemType { General, Drink, Food, Marker, Property }

    public ItemType itemType;
    [SerializeField] float pickUpRange;
    [SerializeField] float flightTime;
    [SerializeField] float flightSpeed;
    [SerializeField] float verticalMultiplier;
    [SerializeField] AnimationCurve verticalDeltaVelocity;
    CircleCollider2D col;

    // Event
    public delegate void OnItemLandHandler(GameObject item, Vector2 landPosition);
    public delegate void OnItemGrabHandler(GameObject item);
    public event OnItemLandHandler OnItemLand;
    public event OnItemGrabHandler OnItemGrab;

    // Event trigger
    protected virtual void TriggerOnItemLand(GameObject item, Vector2 landPosition)
    {
        OnItemLand?.Invoke(item, landPosition);
    }

    public virtual void TriggerOnGrabLand(GameObject item)
    {
        OnItemGrab?.Invoke(item);
    }

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = pickUpRange;
    }

    public void Yeet(Vector2 yeetDirection)
    {
        // Add a rigidbody and a collider so it can detect collisions
        gameObject.AddComponent<BoxCollider2D>();
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // Change layer so it doesn't collide with the player
        gameObject.layer = 6;

        StartCoroutine(Fly(flightTime, yeetDirection));
    }

    IEnumerator Fly(float duration, Vector2 dir)
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            // Calculate flight
            transform.Translate(dir * flightSpeed * Time.deltaTime);
            transform.Translate(Vector2.up * verticalMultiplier * verticalDeltaVelocity.Evaluate(t / duration) * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        Deth();
    }

    void Deth()
    {
        TriggerOnItemLand(gameObject, transform.position);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Deth();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}
