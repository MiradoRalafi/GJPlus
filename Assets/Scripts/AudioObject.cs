using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public int selector;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayMusicSelector(selector);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
