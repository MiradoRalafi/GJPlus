using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioLoader : MonoBehaviour
{
    public string SceneName;
    void Start()
    {
        //LoadBank();
    }

    // Update is called once per frame
    void Update()
    {
        //LoadBank();
    }

    void LoadBank()
    {
        if (FMODUnity.RuntimeManager.HasBanksLoaded)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        }
    }
}
