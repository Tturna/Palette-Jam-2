// This is a script for props that can be picked up and thrown

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Item : MonoBehaviour
{
    [SerializeField] float pickUpRange;
    [SerializeField] float flightTime;
    [SerializeField] float flightSpeed;
    [SerializeField] float verticalMultiplier;
    [SerializeField] AnimationCurve verticalDeltaVelocity;
    CircleCollider2D col;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = pickUpRange;
    }

    void Update()
    {
        //Debugging Stuff
        if(ValueManager.instance != null)
        {
            if(ValueManager.instance.debuggable)
            {
                if(ValueManager.instance.valueManager.activeInHierarchy == true)
                {
                    if(!string.IsNullOrEmpty(ValueManager.instance.pickupRange.text))
                    {
                        pickUpRange = int.Parse(ValueManager.instance.pickupRange.text);
                    }

                    if(!string.IsNullOrEmpty(ValueManager.instance.flightTime.text))
                    {
                        flightTime = int.Parse(ValueManager.instance.flightTime.text);
                    }

                    if(!string.IsNullOrEmpty(ValueManager.instance.flightSpeed.text))
                    {
                        flightSpeed = int.Parse(ValueManager.instance.flightSpeed.text);
                    }
                }
            }
        }
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
            Debug.Log(verticalDeltaVelocity.Evaluate(t / duration));
            Vector2 u = Vector2.up * verticalMultiplier * verticalDeltaVelocity.Evaluate(t / duration) * Time.deltaTime;
            transform.Translate(u);
            Debug.Log(u);
            Debug.DrawRay(transform.position, u);

            yield return new WaitForEndOfFrame();
        }

        Deth();
    }

    void Deth()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Deth();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}
