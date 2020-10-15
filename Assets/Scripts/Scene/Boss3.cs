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
    bool halftime=false;
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
            if (!halftime && WaitTime < originalTime / 3)
            {
                halftime = true;
                AudioManager.PlayMusicSelector(5);
            }
            timeUI.fillAmount = WaitTime / originalTime;
        }
        else
        {
            SceneManager.LoadScene("StoryTelling 4");
        }
    }
}
