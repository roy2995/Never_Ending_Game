﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    [Header("Move Settings")]
    public Character_Control2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0;
    bool jump = false;

    [Header("Animator Settings")]
    public Animator player_animator;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //player_animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            //player_animator.SetBool("Jumping", true);
        }
    }

    public void OnLanding()
    {
        //player_animator.SetBool("Jumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        
    }
}
