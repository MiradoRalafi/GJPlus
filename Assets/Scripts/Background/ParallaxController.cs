using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float Speed;
    public float Offset;

    void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
        if(transform.localPosition.x < -Offset)
        {
            transform.localPosition = Vector2.right * Offset;
        }
    }
}
