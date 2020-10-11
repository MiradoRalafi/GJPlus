using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static FMOD.Studio.EventInstance instanceMusic;
    public static FMOD.Studio.EventInstance instanceAmbi;
    // Start is called before the first frame update
    void Start()
    {
        instanceMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        instanceMusic.start();
        PlayMusicSelector(0);
        //instanceAmbi = FMODUnity.RuntimeManager.CreateInstance("event:/Ambi");
        //instanceAmbi.start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlayMusicSelector(int musicSelector)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicSelector", musicSelector);
    }

    public static void PlayAmbiSelector(int ambiSelector)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AmbiSelector", ambiSelector);
    }

    public static void StopAmbi()
    {
        instanceAmbi.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //instanceAmbi = FMODUnity.RuntimeManager.CreateInstance("event:/Ambi");
        //instanceAmbi.start();
    }

    public static void PlayOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }


    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

}
