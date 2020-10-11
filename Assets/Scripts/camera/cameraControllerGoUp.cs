using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerGoUp : MonoBehaviour
{
    public float InitialWaitTime;
    public float Speed;
    //Commentaires
    void Update()
    {
        if (InitialWaitTime > 0)
        {
            InitialWaitTime -= Time.deltaTime;
            return;
        }
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }
}
