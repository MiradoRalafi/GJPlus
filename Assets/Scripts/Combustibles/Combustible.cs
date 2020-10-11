using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combustible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TopDownController controller = collision.GetComponent<TopDownController>();
        if(controller != null)
        {
            controller.SetSpotlightRadius(60);
            gameObject.SetActive(false);
        }
    }
}
