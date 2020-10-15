using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotUI : MonoBehaviour
{
    public GameObject[] spotLight;
    public Text nbSpotText;
    // Start is called before the first frame update
    void Start()
    {
        nbSpotText.text= spotLight.Length.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpot();


    }
    void CheckSpot()
    {
        int total = spotLight.Length;
        
        foreach (GameObject item in spotLight)
        {
            if (item.GetComponent<FireCamp>().Active)
                total -= 1;
        }
        nbSpotText.text = total.ToString();
    }
}
