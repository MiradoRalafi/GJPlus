using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCamp : MonoBehaviour
{
    public Light SpotLight;
    public FireLight FireLight;
    public bool Active = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TopDownController controller = collision.GetComponent<TopDownController>();
        if (controller != null)
        {
            controller.SetSpotlightRadius(60);
            ActivateSpotLight();
        }
    }

    private void ActivateSpotLight()
    {
        SpotLight.gameObject.SetActive(true);
        FireLight.gameObject.SetActive(true);
        Active = true;
        StartCoroutine(StartSpotLightChange(75));
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