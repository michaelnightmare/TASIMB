using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootingScript : MonoBehaviour {

    int enemyClipSize = 6;
    public int enemyBulletCount = 6;
    public bool canShoot = true;
    ShootingScript shootingScript;
    AudioSource PlayerSounds;
    public AudioClip shotClip;
    public AudioClip reloadClip;
    public GameObject bullet;
    public Transform bulletSpawn;
    public float fireRate = 3.0f;
    public float nextShot = 0.0f;
    public float reloadTime = 5.0f;
    public float nextClip = 0.0F;
    public AI_movement enemyRef;



    // Use this for initialization
    void Start ()
    {
        shootingScript = GetComponent<ShootingScript>();
        PlayerSounds = GetComponent<AudioSource>();
    
        

    }
	
	// Update is called once per frame
	void Update ()
    {

   

        if (enemyBulletCount == 0 )
        {
            if (Time.time > nextClip)
            {
                Reload();
            }
        }
	}

    public void Shoot()
    {
        if (!canShoot) return;

        if (Time.time > nextShot && enemyRef.enemyAlive == true)
        {
            shootingScript.enemyShoot();
            PlayerSounds.clip = shotClip;
            PlayerSounds.Play();
            enemyBulletCount--;
            nextShot = Time.time + fireRate;
        }

        if (enemyBulletCount == 0)
        {
            nextClip = Time.time + reloadTime;
            canShoot = false;
           
        }
        
    }

    public void Reload()
    {
        enemyBulletCount = enemyClipSize;
        PlayerSounds.clip = reloadClip;
        PlayerSounds.Play();
      
        
        canShoot = true;
    }
  
}
