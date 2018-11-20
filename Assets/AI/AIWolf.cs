using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWolf : MonoBehaviour {

    public Transform Player;
    public PlayerScript playerInteraction;
    public float EnemySpeed;
    Animator anim;
    public float EnemyDistance;
    public float EnemyCloseness;
    float attackRate = 2.0f;
    float nextAttack = 0.0f;
    public float wolfHealth = 1;
    public bool wolfAlive;
    public ObjectDestroyer clearBodies;
    public GameObject steakRef; 

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        clearBodies = GetComponent<ObjectDestroyer>();
        wolfAlive = true; 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Vector3.Distance(Player.position, this.transform.position) < EnemyDistance && wolfAlive)
        {
            Vector3 direction = Player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("idle", false);
            anim.SetBool("walk", true);

            if(direction.magnitude > EnemyCloseness)
            {
                this.transform.Translate(0, 0, EnemySpeed= 0.05f);
                anim.SetBool("walk", true);
              
            }
    
            else
            {
              

                anim.SetBool("walk", false);
                 EnemySpeed = 0;
                anim.SetBool("attack", true);
                if (Time.time > nextAttack)
                {

                    damagePlayer();
                    nextAttack = Time.time + attackRate;
                }
              
               
            }
        }
        else
        {
            anim.SetBool("idle", true);
         
            anim.SetBool("walk", false);
            EnemySpeed = 0;
          
        }
        if (wolfHealth <=0)
        {
            wolfDeath();
        }

        if(wolfAlive==false)
        {
            
            clearBodies.enabled = true;
            Invoke("spawnSteak", 5);
      

        }
		
	}
    void damagePlayer()
    {
        playerInteraction.playerTakeDamage();
        
    }
   public void wolfTakeDamage()
    {
        wolfHealth--;
    }
    void wolfDeath()
    {
        wolfAlive = false;
        anim.SetBool("dead", true);
        EnemySpeed = 0;
        
        
    }
    public void spawnSteak()
    {
        GameObject steakTemp = Instantiate(steakRef, transform.position, Quaternion.identity) as GameObject;
        //steakTemp.GetComponent<SteakScr>().Player = playerRef.GetComponent<PlayerScript>();
        //steakTemp.GetComponent<SteakScr>().playerInteraction = PlayerRef.GetComponent<PlayerScript>();
    }
}
