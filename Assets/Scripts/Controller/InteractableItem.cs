using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public bool canOpen;
    public GameObject toOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (canOpen)
        {
            toOpen.SetActive(true);
        }    
    }
    public void closePanel()
    {
        toOpen.SetActive(false);
    }

}
