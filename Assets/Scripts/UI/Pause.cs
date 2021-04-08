using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject helpUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseUI)
            CheckInput();
    }
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
            }
        }
    }
    public void OpenHelp()
    {
        if (helpUI.activeSelf)
            helpUI.SetActive(false);
        else
            helpUI.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }
    public void GOTOMenu()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("MainMenu");
    }
}
