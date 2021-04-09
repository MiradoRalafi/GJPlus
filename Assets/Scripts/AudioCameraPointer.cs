using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCameraPointer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        makeCamFollowPointer( );
    }

    public void makeCamFollowPointer( ){
        float speed = 20f;
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
    }
}
