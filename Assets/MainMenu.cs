﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("StoryTelling 1");
    }

    public void ExitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
