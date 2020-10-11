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

    public int faceDirection;

    protected void Start()
    {
        CalculateRaySpacings();
    }

    void Update()
    {
        Vector2 velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        animator.SetBool("isWalking", (velocity.x != 0 || velocity.y != 0) ? true : false);
        if (faceDirection != velocity.x && velocity.x != 0)
        {
            FlipHorizontally();
        }
        Move(velocity * MoveSpeed * Time.deltaTime);
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

    protected void FlipHorizontally()
    {
        animator.transform.Rotate(Vector2.up * 180);
        velocity.x = -velocity.x;
        faceDirection = -faceDirection;
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
