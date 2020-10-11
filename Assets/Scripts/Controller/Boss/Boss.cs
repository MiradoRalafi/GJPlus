using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float attackRange;
    public int speed;
    public int enrageSpeed;
    public int damage;
    public Slider healthBar;
    public float pv;
    public bool isInvulnerable = false;
    public bool isEnraged = false;
    public LayerMask attackMask;
    private float maxHealth;
    private Animator anim;
    public bool isDead = false;
    public SpriteRenderer[] bodyPart;
    public Color hurtColor;
    public string deadSound;
   // private AudioManager audioManager;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
       // audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        maxHealth = pv;
    }
    void Update()
    {
        healthBar.value = (float)pv / maxHealth;
        print("HealthBar value :" + healthBar.value);


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void ReceiveDamage(int damage)
    {
        if (isInvulnerable)
            return;

        if (pv - damage >= 0)
        {
            StartCoroutine(Flash());
            pv -= damage;
            // StartCoroutine(Flash());
            if (pv - damage <= (maxHealth / 2))
            {
                print("Ato");
                anim.SetBool("isEnraged", true);
                isEnraged = true;
            }
        }
        else
        {
            anim.SetTrigger("dead");
            DestroySelf();
        }
    }
    public void DetectionPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, attackMask);

        if (hit != null)

        {
            hit.GetComponent<PlayerController>().ReceiveDamage(damage);
        }
        else
        {
            print("Null ici");

        }
    }
    IEnumerator Flash()
    {
        for (int i = 0; i < bodyPart.Length; i++)
        {
            bodyPart[i].color = hurtColor;
        }
        yield return new WaitForSeconds(0.05F);
        for (int i = 0; i < bodyPart.Length; i++)
        {
            bodyPart[i].color = Color.white;
        }
    }
    public void DestroySelf()
    {
      //  audioManager.Play(deadSound);
        isDead = true;
        Destroy(gameObject.GetComponent<Collider2D>());
        healthBar.gameObject.SetActive(false);

        //Destroy(gameObject);
    }
}
