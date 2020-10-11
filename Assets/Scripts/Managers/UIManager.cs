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
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller != null && controller.isDead)
            {
                if (deadScreen != null)
                {
                    deadScreen.SetActive(true);
                    StartCoroutine(LoadScene());
                }
            }
            else
            {
                TopDownController tController = player.GetComponent<TopDownController>();
                if (tController != null && tController.isDead)
                {
                    if (deadScreen != null)
                    {
                        deadScreen.SetActive(true);
                        StartCoroutine(LoadScene());
                    }
                }
            }
        }
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1Part2");
    }
}
