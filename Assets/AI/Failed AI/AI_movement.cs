using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_movement : MonoBehaviour {

    public Transform Player;
    public float EnemySpeed;
    public AIShootingScript Gun;
    Animator anim;
    public float Shoot = 0.0f;
    public float EnemyDistance;
    public float EnemyCloseness;
    public float EnemyAcceleration;
    public float enemyHealth = 2;
    public bool enemyAlive;
 
    public bool playingReloadAnim = false;
    public ObjectDestroyer clearBodies;


    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        Gun = GetComponentInChildren<AIShootingScript>();
        clearBodies = GetComponent<ObjectDestroyer>();
        enemyAlive = true;
        Player = GameObject.Find("Cowboy").transform;
      
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        
        if(Vector3.Distance(Player.position, this.transform.position) < EnemyDistance && enemyAlive== true)
        {
            Vector3 direction = Player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("OnGround", false);
            anim.SetFloat("Forward", EnemySpeed);

            if(direction.magnitude > EnemyCloseness)
            {
                anim.SetBool("Aim", false);

                EnemySpeed = EnemySpeed + Time.deltaTime / EnemyAcceleration;
                if (EnemySpeed > 0.05f)
                {
                    EnemySpeed = 0.05f;
                }
                this.transform.Translate(0, 0, EnemySpeed);
                anim.SetFloat("Forward", EnemySpeed);
                
            }
            else
            {
                anim.SetFloat("Forward", 0f);

                if (Gun.enemyBulletCount >0 )
                {
                    EnemySpeed = 0;
                    ShootPlayer();
                    anim.SetBool("Aim", true);
                    Gun.gameObject.SetActive(true);
                    if (Time.time > Shoot)
                    {
                        Gun.Shoot();
                        Shoot = Time.time + 1.0f;
                    }
                }
                else
                {
                    anim.SetBool("Aim", false);
                }
            }
        }
        else
        {
            anim.SetBool("OnGround", true);
            anim.SetBool("Aim", false);
            anim.SetFloat("Forward", EnemySpeed);
            EnemySpeed = 0;
          
        }
     

    }
    public void enemyTakeDamage()
    {
        
        enemyHealth--;
        anim.SetTrigger("enemyHurt");

        if (enemyHealth == 0)
        {
           
         
            Debug.Log("Dead");
            enemyDeath();
            
        }

        if (enemyAlive == false)
        {

            clearBodies.enabled = true;


        }


    }

    void ShootPlayer()
    {
        if (anim.GetBool("Aim") == false)
        {
            Shoot = Time.time + 1.0f;
        }
    }

    void enemyDeath()
    {
        
        enemyAlive = false;
        anim.SetBool("enemyAlive", false);
        GameManagerScr._instance.enemyCounterIncrease();

    }

}
