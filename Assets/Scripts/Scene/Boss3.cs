using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss3 : MonoBehaviour
{
    public float WaitTime = 3;

    // Update is called once per frame
    void Update()
    {
        if(WaitTime > 0)
        {
            WaitTime -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("StoryTelling 4");
        }
    }
}
