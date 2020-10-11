using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlateformController : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.name.Equals("character"))
        {
            Invoke("dropPlateform", 0.5f);
            Destroy(gameObject, 2f);
        }
    }

    void dropPlateform()
    {
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
