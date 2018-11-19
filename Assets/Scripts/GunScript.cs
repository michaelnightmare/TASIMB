using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{

    int clipSize = 6;
    int bulletCount = 6;
    public bool canShoot = true;
    ShootingScript shootingScript;
    AudioSource PlayerSounds;
    public AudioClip shotClip;
    public AudioClip reloadClip;
    public int bulletIcons;
    public Image[] bullets;
    public Sprite bullet;


    void Start()
    {
        shootingScript = GetComponent<ShootingScript>();
        PlayerSounds = GetComponent<AudioSource>();
        Reload();
     
    }
    void Update()
    {
        if (bulletCount >= bulletIcons)
        {
            bulletCount = bulletIcons;
        }

        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < bulletCount)
            {
                bullets[i].sprite = bullet;
            }
            
            else
            {
                bullets[i].enabled = false;
            }
        }
 
      

    }
    public void Shoot()
    {

        if (bulletCount == 0)
        {
            //play click sound
        }
        else
        {
            shootingScript.Shoot();
            PlayerSounds.clip = shotClip;
            PlayerSounds.Play();
            bulletCount--;

            if (bulletCount <= 0)
            {
                canShoot = false;
            }
        }
    }

    public void Reload()
    {
        bulletCount = clipSize;
        PlayerSounds.clip = reloadClip;
        PlayerSounds.Play();
        canShoot = true;

        for (int i = 0; i < bullets.Length; i++)
        {
            if (i > bulletCount)
            {
                bullets[i].sprite = bullet;
            }

            else
            {
                bullets[i].enabled = true;
            }
        }

    }



}
