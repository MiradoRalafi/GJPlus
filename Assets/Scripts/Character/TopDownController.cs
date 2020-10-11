using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    public float MoveSpeed;
    public Animator animator;
    // Essential attributes
    public Collider2D mainCollider;
    //[HideInInspector]
    public Vector2 velocity;
    protected ColliderEdges colliderEdges;
    protected const float skinWidth = .015f;
    private Vector2 velocityOld;

    // Detection raycast attributes
    [Range(2, 100)]
    public float horizontalRayCount = 2;
    [Range(2, 100)]
    public float verticalRayCount = 2;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    // Collision attributes
    public LayerMask environmentLayer;
    protected CollisionInfo collisionInfo;

    private string currentMoveAxis;

    Light spotLight;
    public float NormalLightAngle = 30;
    public float LightShrinkCountdown = 4.5f;
    float timeBeforeLightShrink;
    public float MinDistance = 15;
    public bool isDead;

    protected void Start()
    {
        spotLight = GetComponentInChildren<Light>();
        timeBeforeLightShrink = 8;
        CalculateRaySpacings();
    }

    void Update()
    {
        if (timeBeforeLightShrink > 0)
        {
            timeBeforeLightShrink -= Time.deltaTime;
            if(timeBeforeLightShrink <= 0)
            {
                StartCoroutine(StartSpotLightChange(spotLight.spotAngle - 15));
                timeBeforeLightShrink = LightShrinkCountdown;
            }
        }
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        animator.SetBool("LeftWalking", false);
        animator.SetBool("RightWalking", false);
        animator.SetBool("DownWalking", false);
        animator.SetBool("UpWalking", false);
        if (velocity.x != 0)
        {
            print(velocity.x == 1);
            animator.SetBool("LeftWalking", velocity.x == -1);
            animator.SetBool("RightWalking", velocity.x == 1);
        }
        else
        {
            animator.SetBool("DownWalking", velocity.y == -1);
            animator.SetBool("UpWalking", velocity.y == 1);
        }
        Move(velocity * MoveSpeed * Time.deltaTime);
        velocityOld = velocity;
    }

    public float GetSpotlightRadius()
    {
        return spotLight.spotAngle;
    }

    public void SetSpotlightRadius(float angle)
    {
        StartCoroutine(StartSpotLightChange(angle));
        timeBeforeLightShrink += 2f;
    }

    IEnumerator StartSpotLightChange(float angle)
    {
        if(spotLight.spotAngle > angle)
        {
            while (spotLight.spotAngle > angle)
            {
                yield return new WaitForSeconds(.05f);
                spotLight.spotAngle--;
            }
        }
        if (spotLight.spotAngle < angle)
        {
            while (spotLight.spotAngle < angle)
            {
                yield return new WaitForSeconds(.05f);
                spotLight.spotAngle++;
            }
        }
        spotLight.spotAngle = angle;
        if(angle == 0)
        {
            spotLight.gameObject.SetActive(false);
        }
        timeBeforeLightShrink = LightShrinkCountdown;
    }

    /// <summary>
    /// Manages all movements of the object.
    /// </summary>
    /// <param name="velocity">Reference to object's velocity attribute.</param>
    public void Move(Vector2 velocity)
    {
        collisionInfo.Reset();
        UpdateColliderEdges();

        CheckHorizontalCollision(ref velocity);
        CheckVerticalCollision(ref velocity);
        transform.Translate(velocity);
    }

    /// <summary>
    /// Checks if the object collides with the environment horizontally and adjusts its velocity accordingly. 
    /// Also checks if object should climb a new slope or not.
    /// </summary>
    /// <param name="velocity">Reference to object's velocity attribute.</param>
    private void CheckHorizontalCollision(ref Vector2 velocity)
    {
        float direction = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        Vector2 origin = (direction == -1) ? colliderEdges.bottomLeft : colliderEdges.bottomRight;
        Vector2 spacingDirection = Vector2.up;

        // Fires horizontal rays for obstacle checking
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = origin + spacingDirection * (i * horizontalRaySpacing);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, rayLength, environmentLayer);

            Debug.DrawRay(rayOrigin, Vector2.right * direction * 5, Color.green);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * direction;
                rayLength = hit.distance;
                collisionInfo.right = direction == 1;
                collisionInfo.left = direction == -1;
            }
        }
    }

    /// <summary>
    /// Checks if the object collides with the environment vertically and adjusts its velocity accordingly.
    /// </summary>
    /// <param name="velocity">Reference to object's velocity attribute.</param>
    private void CheckVerticalCollision(ref Vector2 velocity)
    {
        float direction = (velocity.y <= 0) ? -1 : 1;
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        Vector2 origin = (velocity.y <= 0) ? colliderEdges.bottomLeft : colliderEdges.topLeft;

        // Fires vertical rays for ground/ceiling checking
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = origin + Vector2.right * (i * verticalRaySpacing + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * direction, rayLength, environmentLayer);

            Debug.DrawRay(rayOrigin, Vector2.up * direction * rayLength, Color.red);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * direction;
                rayLength = hit.distance;
                collisionInfo.top = direction == 1;
                collisionInfo.bottom = direction == -1;
            }
        }
    }

    /// <summary>
    /// Calculates spacing distance between detection raycasts.
    /// </summary>
    private void CalculateRaySpacings()
    {
        Bounds bounds = mainCollider.bounds;
        bounds.Expand(skinWidth * -2);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    /// <summary>
    /// Updates the coordinates of collider's top left, top right, bottom left and bottom right edges.
    /// </summary>
    private void UpdateColliderEdges()
    {
        Bounds bounds = mainCollider.bounds;
        bounds.Expand(skinWidth * -2);
        colliderEdges.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        colliderEdges.topRight = new Vector2(bounds.max.x, bounds.max.y);
        colliderEdges.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        colliderEdges.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }
}

public struct ColliderEdges
{
    public Vector2 topLeft, topRight;
    public Vector2 bottomLeft, bottomRight;
}

public struct CollisionInfo
{
    public bool top, bottom;
    public bool left, right;

    public bool climbingSlope;
    public bool descendingSlope;

    public int faceDirection;
    public float slopeAngle, slopeAngleOld;

    public void Reset()
    {
        top = bottom = false;
        left = right = false;
        climbingSlope = false;
        descendingSlope = false;
        slopeAngleOld = slopeAngle;
        slopeAngle = 0;
    }
}
