using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2PlayerCheck : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("waterCollider"))
        {
            print("YOU ARE DEAD");
            StartCoroutine(Dead());
           // Invoke("dropPlateform", 0.5f);
           // Destroy(gameObject, 2f);
        }
        if (col.gameObject.name.Equals("chechWin"))
        {
            print("YOU Win");

            StartCoroutine(Win());
            // Invoke("dropPlateform", 0.5f);
            // Destroy(gameObject, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Win()
    {

        //panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("StoryTelling 3");
    }
  

    IEnumerator Dead()
    {
        
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
