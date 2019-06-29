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
    public AudioClip outOfAmmo;
    public int bulletIcons;
    public Transform uiRoot;
    public Image[] bullets;
    public Sprite bullet;
    public bool isUnlocked= false;
    public bool canReload = false;
    bool initialized = false;
    public GameObject muzzleFlash;
    public float holdTime;
    public float gunHoldTime;


    public GameObject GunIcon;
    public GameObject Highlight;
    public GunDisplayScr gunDisplay;

    void Start()
    {
        if (!initialized) Initialize();
        Reload();
        holdTime = .10f;
        gunHoldTime = .5f;
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

      
        if (bulletCount >= 0)
        {
          
            shootingScript.Shoot();
            PlayerSounds.PlayOneShot(shotClip);
            bulletCount--;
            muzzleFlare();
            if (bulletCount < 0)
            {
               
                canShoot = false;
                Debug.Log("click");
                PlayerSounds.PlayOneShot(outOfAmmo);

                if (!canReload)
                {

                    StartCoroutine(gunHold());
                }
                
            }
        }
        else
        {
            Debug.Log("click");
            PlayerSounds.PlayOneShot(outOfAmmo);
        }

    }
    public void EquipGun(bool equipped)
    {
        if (equipped)
        {
            gameObject.SetActive(true);
            ToggleUI(true);
            Highlight.SetActive(true);
            
            
        }
        else
        {
            gameObject.SetActive(false);
            ToggleUI(false);
            Highlight.SetActive(false);
           
            
           
        }
    }
    public void Reload()
    {
        bulletCount = clipSize;
        PlayerSounds.PlayOneShot(reloadClip);
        //Debug.Log(PlayerSounds.isPlaying + " " + PlayerSounds.clip);
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

    public void Lock()
    {
        
        isUnlocked = false;
       
        GunIcon.SetActive(false);

       
       
    }

    public void Unlock()
    {
        if (!initialized) Initialize();
        Reload();
        isUnlocked = true;
        GunIcon.SetActive(true);
        gunDisplay.displayOn();
    }
    IEnumerator flashFlare()
    {
        //don't do anything for hold time seconds
        yield return new WaitForSeconds(holdTime);

        muzzleFlash.SetActive(false);
        yield break;
    }

    IEnumerator gunHold()
    {
        yield return new WaitForSeconds(gunHoldTime);
        Lock();
        yield break;
    }

    public void muzzleFlare()
    {
        muzzleFlash.SetActive(true);
        muzzleFlash.transform.position = shootingScript.bulletSpawn.position;
        muzzleFlash.transform.rotation = Quaternion.LookRotation(shootingScript.bulletSpawn.forward, Vector3.up);
        muzzleFlash.transform.localScale = new Vector3(Random.Range(0.6f, 1.5f), 0, Random.Range(0.8f, 1.3f));
      

        StartCoroutine(flashFlare());
    }

}
