using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doge : MonoBehaviour
{
    [SerializeField] float speed;

    bool running;
    Vector2 dir = Vector2.zero;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (dir != Vector2.zero)
        {
            rb.AddForce(dir * speed);
        }
    }

    public void FuckingZoom()
    {
        if (!running)
        {
            gameObject.layer = 6;
            GetComponent<Animator>().SetTrigger("run");
            GetComponent<Animator>().SetBool("running", running);
            InvokeRepeating("UpdateDirection", 0f, 3f);
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void UpdateDirection()
    {
        dir = Random.insideUnitCircle.normalized;
    }
}
