using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss3 : MonoBehaviour
{
    public float WaitTime = 3;
    private float originalTime;
    public Image timeUI;
    private void Start()
    {
        originalTime = WaitTime;
    }
    // Update is called once per frame
    void Update()
    {
        if(WaitTime > 0)
        {
            WaitTime -= Time.deltaTime;
            timeUI.fillAmount = WaitTime / originalTime;
        }
        else
        {
            SceneManager.LoadScene("StoryTelling 4");
        }
    }
}
