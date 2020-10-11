using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("character"))
        {
            collision.GetComponent<PlayerController>().isDead = true;
        }
    }
}
