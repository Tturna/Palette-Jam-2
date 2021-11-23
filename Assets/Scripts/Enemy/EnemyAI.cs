using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    
    Seeker seeker;
    Rigidbody2D rb;
    public bool canMove = true;

    public Transform enemySprite;
    public float stunTime;
    bool isBeingStunned;
    public bool canCatch = true;

    public Animator anim;
    Vector2 force;
    Vector2 direction;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && canMove)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }else
        {
            reachedEndOfPath = false;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(force.x >= 0.01f)
        {
            enemySprite.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }else if(force.x <= -0.01f)
        {
            enemySprite.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
    }

    void Update()
    {
        anim.SetFloat("Speed", force.sqrMagnitude);

        if(isBeingStunned)
        {
            anim.SetBool("Stunned", true);
        }else
        {
            anim.SetBool("Stunned", false);
        }

        if (GameObject.FindObjectOfType<TaskManager>().tasksDone <= 0 && GameObject.FindObjectOfType<TaskManager>().tasksDone <= GameObject.FindObjectOfType<TaskManager>().totalTasks)
        {
            canMove = false;
        }else
        {
            canMove = true;
        }
    }

    public IEnumerator Stun()
    {
        canMove = false;
        isBeingStunned = true;
        GetComponent<Collider2D>().isTrigger = true;
        int rand = Random.Range(0, SfxManager.instance.ManagerChasingPlayer.Count);
        SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.ManagerChasingPlayer[rand]);
        yield return new WaitForSeconds(stunTime);
        canMove = true;
        GetComponent<Collider2D>().isTrigger = false;
        isBeingStunned = false;
    }

    public void StunEnemy()
    {
        StartCoroutine(Stun());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Pickup")
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0 || other.gameObject.GetComponent<Rigidbody2D>().velocity.y != 0)
            {
              StunEnemy();  
            }
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && isBeingStunned == false && canCatch)
        {
            int rand = Random.Range(0, SfxManager.instance.ManagerCatchingPlayer.Count);

            SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.ManagerCatchingPlayer[rand]);

            other.gameObject.GetComponent<PlayerMovement>().canMove = false;
            Scenes.instance.Restart();
        }
    }
}
