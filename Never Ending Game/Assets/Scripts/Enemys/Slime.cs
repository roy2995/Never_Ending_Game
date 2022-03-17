using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("private Settings")]
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;
    private float healthPoints;
    private float nextFireTime;
    private float nextMove;

    [Header("Slime Movement")]
    [Range(100, 500)] public float walkSpeed;
    [Range(0.1f, 2)] public float moverate;
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Collider2D bodycollider;
    public LayerMask groundLayer;


    [Header("Slime Attack Settings")]
    [Range(3, 10f)]public float range;
    [Range(0.1f, 1f)] public float fireRate;
    [Range(2, 10)] public float distToPlayer;
    public Transform player, ballPOS;
    public SlimeBall slimeBall;

    [Header("Slime Health Settings")]
    [Range(1, 50)] public float maxHealthPoints = 10;


    
    void Start()
    {
        healthPoints = maxHealthPoints;
        mustPatrol = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        if(player != null)
        { 
        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            mustPatrol = false;
            rb.velocity = Vector2.zero;
            if (nextFireTime < Time.time)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            mustPatrol = true;
        }

        }
        else
        {
            mustPatrol = true;
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            //Checks for ground. When no ground is present on groundCheckPOS mustTurn will be true.
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn)
        {
            Flip();
        }
        //moves slime
        if(nextMove < Time.time)
        {
            rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
            nextMove = Time.time + moverate;

        }
        
    }

    void Flip()
    {
        //Moves back forth on platform, stop at edges of platform and turn around continuing to patrol
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    void Shoot()
    {
            //Spawn slime ball projectile
            Instantiate(slimeBall, ballPOS.position, ballPOS.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        //Color
        Gizmos.color = Color.green;
        //Displays sphere showing ranges in editor
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, distToPlayer);
    }

    public void TakeHit(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Debug.Log("Slime died");
            Destroy(gameObject);
        }


    }
}
