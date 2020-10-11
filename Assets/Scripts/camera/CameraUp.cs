using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUp : MonoBehaviour
{
    public float InitialWaitTime;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
