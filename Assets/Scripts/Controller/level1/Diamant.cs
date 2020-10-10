using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamant : MonoBehaviour
{
    public bool isClicked=false;
    public float delay=1f;
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
        isClicked = true;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
