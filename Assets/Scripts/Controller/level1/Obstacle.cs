using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 80 * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Collide");
        if (collision.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().isDead=true;
        }
    }
}
