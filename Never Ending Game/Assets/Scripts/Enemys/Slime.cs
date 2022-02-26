using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Private settings")]
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    [Header("Slime settings")]
    public Rigidbody2D rb;
    public float walkSpeed;
    public float range;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
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
    }

    void Patrol()
    {
        if (mustTurn)
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

    void Shoot()
    {

    }
}
