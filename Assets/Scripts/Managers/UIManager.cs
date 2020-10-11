using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject deadScreen;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("character")!=null)
            player = GameObject.Find("character");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerController>().isDead)
            {
                if (deadScreen != null)
                {
                    deadScreen.SetActive(true);
                    StartCoroutine(LoadScene());
                }
            }
        }
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
