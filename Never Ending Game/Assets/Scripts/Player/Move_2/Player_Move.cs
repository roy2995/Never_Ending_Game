using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [Header("Objects references")]
    public Rigidbody2D physics;
    public GameObject player;

    [Header("Player settings")]
    [Range(0, 20)] public float moveSpeed;
    [Range(0, 20)] public float jumpForce;

    [Header("Wall Jump Settings")]
    [Range(0, 1)] public float walljumpTime;
    [Range(0, 1)] public float wallslideSpeed;
    [Range(0, 1)] public float wallDistance;
    private bool iswallSliding = false;
    public RaycastHit2D wallchekhit;
    float jumpTime;

    [Header("Grounded")]
    [SerializeField] Transform groundchek;
    [SerializeField] LayerMask groundLayer;
    [Range(0, 1)] public float groundRadius;
    bool isGrounded = false;

    //private
    float mx = 0;
    bool isFacingRigth = true;

    private void Update()
    {
        if(isGrounded && Input.GetButtonDown("Jump") || iswallSliding && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        mx = Input.GetAxis("Horizontal");

        if (mx < 0)
        {
            isFacingRigth = false;
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else
        {
            isFacingRigth = true;
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        physics.velocity = new Vector2(mx * moveSpeed, physics.velocity.y);
        bool touchinground = Physics2D.OverlapCircle(groundchek.position, groundRadius, groundLayer);
        if (touchinground)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Wall Jump
        if (isFacingRigth)
        {
            wallchekhit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);
        }
        else
        {
            wallchekhit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);
        }
       

        if(wallchekhit && !isGrounded && mx != 0)
        {
            iswallSliding = true;
            //jumpTime = Time.time + walljumpTime;
        }
        else 
        {
            iswallSliding = false;
  
        }

        if (iswallSliding)
        {
            physics.velocity = new Vector2(physics.velocity.x, Mathf.Clamp(physics.velocity.y, wallslideSpeed,float.MaxValue));
        }
    }

    void Jump()
    {
        physics.velocity = new Vector2(physics.velocity.x, jumpForce);
    }

}
