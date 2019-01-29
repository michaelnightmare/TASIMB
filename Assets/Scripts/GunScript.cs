using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{

    public int clipSize = 6;
    public int bulletCount = 6;
    public bool canShoot = true;
    ShootingScript shootingScript;
    AudioSource PlayerSounds;
    public AudioClip shotClip;
    public AudioClip reloadClip;
    public int bulletIcons;
    public Transform uiRoot;
    public Image[] bullets;
    public Sprite bullet;
    public bool isUnlocked= false;
    public bool canReload = false;
    bool initialized = false;

    void Start()
    {
        if (!initialized) Initialize();
        Reload();
     
    }

    void Initialize()
    {
        shootingScript = GetComponent<ShootingScript>();
        PlayerSounds = GetComponent<AudioSource>();
        initialized = true;
    }

    public void ToggleUI(bool enabled)
    {
        uiRoot.gameObject.SetActive(enabled);
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
                bullets[i].enabled = true;
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
                bullets[i].enabled = true;
            }

            else
            {
                bullets[i].enabled = true;
            }
        }

    }

    public void Unlock()
    {
        if (!initialized) Initialize();
        Reload();
        isUnlocked = true;

    }

}
