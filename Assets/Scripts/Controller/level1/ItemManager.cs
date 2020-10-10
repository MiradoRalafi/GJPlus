using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] listeDiamant;
    public GameObject decharge;
    public GameObject part2;
    private int cmpt;
    public float delay = 1f;
    public bool isFull = false;
    // Start is called before the first frame update
    void Start()
    {
        cmpt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckList();
        if (isFull)
            StartCoroutine(TransitionPART2());
           
    }
    IEnumerator TransitionPART2()
    {
        
        yield return new WaitForSeconds(delay);
        part2.SetActive(true);
        decharge.SetActive(false);
    }
   void CheckList()
    {
        int taille = listeDiamant.Length;
        for (int i = 0; i < listeDiamant.Length; i++)
        {
            if (!listeDiamant[i].activeSelf && listeDiamant[i].GetComponent<Diamant>().isClicked) 
                taille--;

        }
        if (taille == 0)
            isFull = true;
    }
}
