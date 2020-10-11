using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakamenaLabyrinth : MonoBehaviour
{
    public TopDownController Target;

    public float Epsilon = .05f;

    public LayerMask mask;

    public List<Transform> camps;

    public int FaceDirection = 1;

    public bool lockTarget;

    private void Update()
    {
        if (!lockTarget)
        {
            foreach (Transform camp in camps)
            {
                if (camp.gameObject.activeSelf)
                {
                    float MinDistance = 3.45f;
                    float distance = Vector2.Distance(transform.position, camp.transform.position);
                    if (distance < MinDistance - Epsilon)
                    {
                        Vector2 director = new Vector2(camp.transform.position.x - transform.position.x, camp.transform.position.y - transform.position.y);
                        director.Normalize();
                        transform.Translate(director * 2 * Time.deltaTime);
                    }
                }
                if ((Target.transform.position.x < transform.position.x && FaceDirection == 1) || (Target.transform.position.x > transform.position.x && FaceDirection == -1))
                {
                    FlipHorizontal();
                }
            }
        }
        else
        {
            float distance = Vector2.Distance(transform.position, Target.transform.position);
            if (distance <= .2f && Target.GetSpotlightRadius() == 1)
            {
                Target.GetComponent<TopDownController>().isDead = true;
            }
        }
    }

    void FlipHorizontal()
    {
        FaceDirection = -FaceDirection;
        transform.Rotate(0, 180, 0);
    }
}
