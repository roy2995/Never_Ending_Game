using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("private settings")]
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    [Header("Slime settings")]
    public Rigidbody2D rb;
    public float walkSpeed;
    public float range;
    public float fireRate = 3;
    public float nextFireTime;
    public float timeBTWBalls;
    public float slimeBallSpeed;
    private float distToPlayer;
    public float healthPoints;
    public float maxHealthPoints = 10;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodycollider;
    public Transform player, ballPOS;
    public GameObject slimeBall;
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

        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < range && nextFireTime < Time.time)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            mustPatrol = false;
            rb.velocity = Vector2.zero;
            Shoot();
            //StartCoroutine(Shoot());
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
        //yield return new WaitForSeconds(timeBTWBalls);
        GameObject newSlimeBall = Instantiate(slimeBall, ballPOS.position, Quaternion.identity);

        newSlimeBall.GetComponent<Rigidbody>().velocity = new Vector2(slimeBallSpeed * walkSpeed * Time.fixedDeltaTime, 0f);
        nextFireTime = Time.time + fireRate;
        GameObject.Destroy(newSlimeBall.gameObject, 3f);


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void TakeHit(float damage)
    {
        healthPoints -= damage;
        if (healthPoints < 0)
        {
            Destroy(gameObject);
        }


    }
}
