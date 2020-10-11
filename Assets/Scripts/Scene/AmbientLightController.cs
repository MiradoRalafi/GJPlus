using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLightController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            RenderSettings.ambientLight = new Color(RenderSettings.ambientLight.r + 5, RenderSettings.ambientLight.g + 5, RenderSettings.ambientLight.b + 5);
            print($"{RenderSettings.ambientLight.r}, { RenderSettings.ambientLight.g}, { RenderSettings.ambientLight.b}");
        }
    }
}
