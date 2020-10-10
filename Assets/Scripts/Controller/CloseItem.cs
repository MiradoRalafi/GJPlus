using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseItem : MonoBehaviour
{
    public GameObject toClose;
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

        toClose.SetActive(false);
        toOpen.SetActive(true);
           
    }
}
