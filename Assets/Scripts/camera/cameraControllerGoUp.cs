using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControllerGoUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(0, 1 ,-2);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0, 1 * Time.deltaTime,0);

    }
}
