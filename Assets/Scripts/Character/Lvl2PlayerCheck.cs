using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2PlayerCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("waterCollider"))
        {
            print("YOU ARE DEAD");
           // Invoke("dropPlateform", 0.5f);
           // Destroy(gameObject, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
