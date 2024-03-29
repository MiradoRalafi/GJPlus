﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static FMOD.Studio.EventInstance instanceMusic;
    public static FMOD.Studio.EventInstance instanceAmbi, instanceFoostep;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsPlaying(instanceMusic))
        {
            instanceMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
            instanceMusic.start();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void playFootstep()
    {
        instanceFoostep = FMODUnity.RuntimeManager.CreateInstance("event:/sfx/footstep");
        instanceFoostep.start();
    }

    public static void stopFootstep()
    {
        instanceFoostep.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    

    public static void PlayMusicSelector(int musicSelector)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicSelector", musicSelector);
    }

    public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    public static void PlayAmbiSelector(int ambiSelector)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AmbiSelector", ambiSelector);
    }

    public static void StoMusic()
    {
        instanceMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instanceMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        instanceMusic.start();
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
