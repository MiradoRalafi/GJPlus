using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCamp : MonoBehaviour
{
    public Light SpotLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TopDownController controller = collision.GetComponent<TopDownController>();
        if (controller != null)
        {
            ActivateSpotLight();
        }
    }

    private void ActivateSpotLight()
    {
        SpotLight.gameObject.SetActive(true);
        StartCoroutine(StartSpotLightChange(90));
    }

    IEnumerator StartSpotLightChange(float angle)
    {
        while (SpotLight.spotAngle < angle)
        {
            yield return new WaitForSeconds(.025f);
            SpotLight.spotAngle++;
        }
        SpotLight.spotAngle = angle;
    }
}