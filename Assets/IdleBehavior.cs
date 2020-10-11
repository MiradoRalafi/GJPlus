﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{

    public float timer;
    public float minTime;
    public float maxTime;
    public float startTimeBtwShots;
    float timeBtWShots = 0;
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (Vector2.Distance(player.position, rb.position) <= boss.attackRange)
        {
            animator.SetTrigger("attackC");
          //  timeBtWShots = startTimeBtwShots;
        }
        else
        {
            animator.SetTrigger("attackG");
         //   timeBtWShots -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attackC");
        animator.ResetTrigger("attackG");
        animator.ResetTrigger("idle");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}