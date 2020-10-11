using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjectPause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.StoMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
