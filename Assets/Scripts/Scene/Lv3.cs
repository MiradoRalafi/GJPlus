using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lv3 : MonoBehaviour
{
    public static int light = 0;

    private void Update()
    {
        if(light == 4)
        {
            StartCoroutine("Load");
        }
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Boss Fight 3");
    }
}
