using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakamenaBoss : MonoBehaviour
{
    public Transform target;
    public float WaitTime = 2;
    float waitTimeLeft;
    Animator animator;

    public float MinDistance = 2.5f;
    bool performingAction = false;

    Vector2 targetWalk;

    public LayerMask PlayerMask;
    public Transform Hit;
    public float Radius = 2;

    bool grounded = true;
    int faceDirection = -1;

    public Collider2D JumpCheck;

    private void Start()
    {
        waitTimeLeft = WaitTime;
        animator = GetComponent<Animator>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
    }

    void Update()
    {
        if (!performingAction)
        {
            if (waitTimeLeft > 0)
            {
                waitTimeLeft -= Time.deltaTime;
            }
            else
            {
                TakeAction();
            }
        }

        if (grounded)
        {
            if ((target.transform.position.x < transform.position.x && faceDirection == 1) || (target.transform.position.x > transform.position.x && faceDirection == -1))
            {
                FlipHorizontal();
            }
        }
    }

    void FlipHorizontal()
    {
        faceDirection = -faceDirection;
        transform.Rotate(0, 180, 0);
    }

    void TakeAction()
    {
        performingAction = true;
        int rand = Random.Range(1, 3);
        switch (rand)
        {
            case (1):
                StartCoroutine("WalkAndAttack");
                break;
            case (2):
                StartCoroutine(Jump(Random.Range(1, 4)));
                break;
        }
    }

    IEnumerator WalkAndAttack()
    {
        targetWalk = target.transform.position;
        animator.SetTrigger("Walk");
        float distance = Vector2.Distance(target.position, transform.position);
        while (distance > MinDistance)
        {
            targetWalk = target.transform.position;
            distance = Vector2.Distance(target.position, transform.position);
            transform.position = Vector2.MoveTowards(transform.position, targetWalk, .03f);
            yield return null;
        }
        targetWalk = Vector2.zero;
        StartCoroutine("WaitAndAttack");
        //    else
        //{
        //    targetWalk = Vector2.zero;
        //    StartCoroutine("WaitAndAttack");
        //}
    }

    IEnumerator WaitAndAttack()
    {
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(.5f);
        Attack();
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        StartCoroutine("CheckHit");
        performingAction = false;
        waitTimeLeft = WaitTime;
    }

    IEnumerator CheckHit()
    {
        yield return new WaitForSeconds(.4f);
        print("Check hit");
        Collider2D collider = Physics2D.OverlapCircle(Hit.position, Radius, PlayerMask);
        print(collider);
        if(collider != null)
        {
            collider.GetComponent<PlayerController>().isDead = true;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(Hit.position, Radius);
    //}

    IEnumerator Jump(int number)
    {
        while(number > 0)
        {
            animator.SetTrigger("Jump");
            GetComponent<Rigidbody2D>().velocity = new Vector2(faceDirection * 10, 15);

            yield return new WaitForSeconds(1.1f);
            animator.SetTrigger("Idle");
            yield return new WaitForSeconds(.5f);
            number--;
        }
        performingAction = false;
        waitTimeLeft = WaitTime;
    }
}
