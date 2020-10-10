using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX;
    private bool isHurting = false, isDead=false;
    private bool facingRight = true;
   // public Vector3 respawnPoint;
    private bool isGround = false;
    private bool isWalk = false;
    private bool isJumping = false;
    public int jumpForce = 2200;
    public int moveSpeed = 5;
    // public int runSpeed = 60;
    public bool isTopDown=false;
    public Transform groundDetection;
    public LayerMask mask;
    public float dist=0f;
    private float lastDirection = 1;

    //Stat
    public int pv = 3;
    //private bool isRunning = false;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTopDown)
            rb.isKinematic = true;
        else
            rb.isKinematic = false;
        CheckDead();

        if (!isDead)
        {
            if (isTopDown)
                MoveTopDown();
            else
            {
                Move();
                CheckGround();
            }

            CheckWhereToFace();
            SetAnimationState();
        }
        else
            anim.SetTrigger("isDead");//Eto no anoloana anle variable anle animationDead --Chains
    }

    void Move()
    {
        float speed = moveSpeed;
        float direction = Input.GetAxisRaw("Horizontal");
        isWalk = direction != 0;
        if (direction != 0)
        {
            lastDirection = direction;
        }


        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {

            rb.AddForce(new Vector2(0, jumpForce));
            isGround = false;
            isJumping = true;
        }
    }
    void MoveTopDown()
    {
        float speed = moveSpeed;
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        if (directionX != 0 || directionY != 0)
            isWalk = true;
        else
            isWalk = false;
        if (directionX != 0)
        {
            lastDirection = directionX;
        }


        rb.velocity = new Vector2(directionX * speed, directionY * speed);
    }
    void SetAnimationState()
    {
        //Ato mgerer anle animation Jump, Walk
        anim.SetBool("isJumping", isJumping && !isGround);
        anim.SetBool("isFalling", !isGround);
        anim.SetBool("isFalling", isJumping && !isGround);
        anim.SetBool("isWalking", isGround && isWalk);
    }

    void CheckWhereToFace()
    {
        int face = lastDirection > 0 ? 1 : -1;
        Vector3 ls = transform.localScale;
        transform.localScale = new Vector3(face * Mathf.Abs(ls[0]), ls[1], ls[2]);
    }
    void CheckGround()
    {
        Debug.DrawRay(groundDetection.position, Vector2.down,Color.green);
        RaycastHit2D hit = Physics2D.Raycast(groundDetection.position, Vector2.down, dist, mask);
        if (hit.collider)
        {
            if (isJumping)
            {
                isJumping = false;
            }

            isGround = true;
        }
        else
        {

            isGround = false;
        }
    }
    void CheckDead()
    {
        if (pv == 0)
            isDead = true;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
    }
    void OnCollisionStay2D(Collision2D col)
    {

       
    }
    void OnCollisionExit2D(Collision2D col)
    {
    }
}
