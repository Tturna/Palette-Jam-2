// This is a script for props that can be picked up and thrown

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Item : SpriteThing
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

    bool canFly = true;

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
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Change layer so it doesn't collide with the player
        gameObject.layer = 6;

        if (itemType == Item.ItemType.General)
        {
            SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.plantThrow);
        }

        StartCoroutine(Fly(flightTime, yeetDirection));
    }

    IEnumerator Fly(float duration, Vector2 dir)
    {
        float t = 0;

        while (t < duration && canFly)
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

        // Remove physics components from the object so it behaves normally
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());

        // reset layer to default
        gameObject.layer = 0;

        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Deth();
        }   
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}
