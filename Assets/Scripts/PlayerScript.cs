using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float playerHealth = 3;
    public int healthIcons;
    public bool playerAlive;
    public Image[] stars;
    public Sprite star;
    public float bodyTimer;
    Animator anims;
    public float healthLossDelay = 1.0f;
    public float nextDelay = 0.0f; 
    public float hitTimerAmount = 2.0f; //this stores the amount of time we accrue after being hit
    public int gold;

    private float hitTimer = 0.0f; //this stores the last time we're hit

    void Start()
    {
        playerAlive = true;
        anims = GetComponent<Animator>();
        gold = 5;
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

        if (hitTimer >= 0.0f)
        {
            hitTimer -= Time.deltaTime;
        }

    }



    public void playerTakeDamage(float damage)
    {
        if (Time.time > nextDelay && hitTimer <= 0)
        {
            playerHealth -= damage;
            anims.SetTrigger("PlayerHurt");
            nextDelay = Time.time + healthLossDelay;
            hitTimer = hitTimerAmount;
        }

        else //if we've been hit recently, skip playing the PlayerHurt animation to make it less jarring for the player
        {
            playerHealth -= damage;
            nextDelay = Time.time + healthLossDelay;
        }

    }
    public void playerGetHealth(int healthAmount)
    {
        if (playerHealth < 10)
        {
            playerHealth += healthAmount;
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

        GameManagerScr._instance.YouLose();
   
        if (bodyTimer >= 10f && playerAlive == false)
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerHealth = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }

}
