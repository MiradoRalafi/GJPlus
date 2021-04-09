using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject helpUI;
    public float timeBeforeStart = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if(helpUI)
            StartCoroutine(Load());
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
        {
            helpUI.SetActive(false);
           // Time.timeScale = 1f;
        }
        else
        {
            helpUI.SetActive(true);
           // Time.timeScale = 0f;
        }
    }
    IEnumerator Load()
    {
        Time.timeScale = 0f;
        helpUI.SetActive(true);
        yield return new WaitForSecondsRealtime(timeBeforeStart);
        print("ato");
        helpUI.SetActive(false);
        Time.timeScale = 1f;
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
