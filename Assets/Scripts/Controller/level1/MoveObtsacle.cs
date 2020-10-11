using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObtsacle : MonoBehaviour
{
    public GameObject target;
    public GameObject[] listeBackground;
    public float speed=1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("" + collision.tag);
        if(collision.tag.Equals("Box"))
        {
            for (int i = 0; i < listeBackground.Length; i++)
            {
                listeBackground[i].GetComponent<ParallaxController>().enabled = false;
            }

            gameObject.SetActive(false);


        }
    }
}
