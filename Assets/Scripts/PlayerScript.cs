using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public int playerHealth = 3;
    public int healthIcons;
    public bool playerAlive;
    public Image[] stars;
    public Sprite star;
    public float bodyTimer;
    Animator anims;
    public float healthLossDelay = 1.0f;
    public float nextDelay = 0.0f;
    

    void Start()
    {
        playerAlive = true;
        anims = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {


        if (playerHealth >= healthIcons)
        {
            playerHealth = healthIcons;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            if (i < playerHealth)
            {
                stars[i].sprite = star;
            }
            else
            {
                stars[i].enabled = false;
            }
        }

        // bodyTimer += 1.0f * Time.deltaTime;
        if (playerHealth <= 0)
        {
            playerDead();
        }

      

    }

   

    public void playerTakeDamage()
    {
        if (Time.time > nextDelay)
        {
            playerHealth--;
            anims.SetTrigger("PlayerHurt");
            nextDelay = Time.time + healthLossDelay;
        }

    }
    public void playerGetHealth()
    {
        if (playerHealth < 5)
        {
            playerHealth++;
            for (int i = 0; i < stars.Length; i++)
            {
                if (i > playerHealth)
                {
                    stars[i].sprite = star;
                }
                else
                {
                    stars[i].enabled = true;
                }
            }
        }

    }

    void playerDead()
    {
        playerAlive = false;
        anims.SetBool("PlayerAlive", false);

   
        if (bodyTimer >= 10f && playerAlive == false)
        {
            Destroy(gameObject);
        }
    }

    

}
