using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Private settings")]
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;
    private float distToPlayer;
    

    [Header("Slime settings")]
    public Rigidbody2D rb;
    public float walkSpeed;
    public float range;
    public float slimeBallSpeed;
    public float TimeBTWSlimeballs;
    public Transform groundCheckPos;
    public Transform slimeBallPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player;
    public GameObject slimeBall;
    void Start()
    {
        mustPatrol = true;
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            //Checks for ground
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);
        if(distToPlayer <= range)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0 
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                mustPatrol=false;
                rb.velocity = Vector2.zero;
                Shoot();
            }
            else
            {
                mustPatrol = true;
            }
            
        }
    }

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        //moves slime
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        //Moves back forth on platform, stop at edges of platform and turn around continuing to patrol
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    IEnumerator Shoot()
    {
        //Shoot
        yield return new WaitForSeconds(TimeBTWSlimeballs);
        GameObject newSlimeBall = Instantiate(slimeBall, slimeBallPos.position, Quaternion.identity);

        newSlimeBall.GetComponent<Rigidbody2D>().velocity = new Vector2(slimeBallSpeed * walkSpeed * Time.fixedDeltaTime, 0f);

    }
}
