using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{

    bool aiming = false;
    Animator anims;
    public List<GunScript>  guns=new List<GunScript>();
    public bool aimWithMouse = false;
    public float aimSpeed = 10f;
    public float aimRotationOffset = 20f;
    public float fireRate = .5f;
    public float nextShot = 0.0f;
    public Transform shootT;
    public Transform singleHandShootT;
    public Transform rifleShootT;
    public int selectedWeaponIndex= 0;
    public RayCastScr raycastgun;

    public RuntimeAnimatorController defaultController;
    public AnimatorOverrideController rifleOverrideAnims;
    public GunDisplayScr gunDisplay;

 
   




    void switchToWeaponIndex(int index)
    {
        if(index >= 0 && index < guns.Count)
        {
            if (guns[index].isUnlocked)
            {
                guns[selectedWeaponIndex].EquipGun(false);
                selectedWeaponIndex = index;
                guns[selectedWeaponIndex].EquipGun(true);

                if(selectedWeaponIndex == 2)
                {
                    anims.runtimeAnimatorController = rifleOverrideAnims;
                    shootT = rifleShootT;
                }
                else
                {
                    anims.runtimeAnimatorController = defaultController;
                    shootT = singleHandShootT;
                }

            }
        }
    }

    void switchToNextGun()
    {
        int index = selectedWeaponIndex;

        bool foundGun = false;
        while (!foundGun)
        {
            index++;
            if (index > guns.Count - 1)
            {
                index = 0;
            }

            foundGun = guns[index].isUnlocked;
        }

        switchToWeaponIndex(index);
    }


    void switchToPreviousGun()
    {
        int index = selectedWeaponIndex;

        bool foundGun = false;
        while (!foundGun)
        {
            index--;
            if (index < 0)
            {
                index = guns.Count -1;
               
            }

            foundGun = guns[index].isUnlocked;
        }

        switchToWeaponIndex(index);
    }

    public bool disabledShooting = false;

    void Start () {
        anims = GetComponent<Animator>();
     
        guns[0].isUnlocked = true;
        guns[0].canReload = true;
     
        defaultController = anims.runtimeAnimatorController;
        singleHandShootT = shootT;

      

    }


    [ContextMenu("Enable Movement")]
    void enablePlayerMovement()
    {
        disabledShooting = false;
    }

    [ContextMenu("Disable Movement")]
    void disablePlayerMovement()
    {
        disabledShooting = true;
    }




    // Update is called once per frame
    void Update ()
    {
        if (disabledShooting) return;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            aiming = true;
            anims.SetBool("Aim", true);
            guns[selectedWeaponIndex].gameObject.SetActive(true);
            raycastgun.rayCastShot();


            if (Input.GetKeyDown(KeyCode.Mouse0)&& Time.time > nextShot)
            {

                if (guns[selectedWeaponIndex].canShoot)
                {
                    anims.SetTrigger("Shoot");
                    guns[selectedWeaponIndex].Shoot();
                   
                   


                    if (!guns[selectedWeaponIndex].isUnlocked)
                    {
                        
                        switchToPreviousGun();
                       
                    }
                    nextShot = Time.time + fireRate;
                }
      
                
            }


            if (aimWithMouse)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane p = new Plane(Vector3.up, shootT.position);
                float dist;
                p.Raycast(ray, out dist);
                Vector3 intersectionPoint = ray.GetPoint(dist);
                Vector3 aimDir = intersectionPoint - shootT.transform.position;
                aimDir.y = 0;
                float yDiff = Vector3.SignedAngle(transform.forward, shootT.forward, Vector3.up);
                Quaternion targetRot = Quaternion.LookRotation(aimDir) * Quaternion.Euler(0f, -yDiff, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * aimSpeed);



            }
        }
        else
        {
            aiming = false;
            anims.SetBool("Aim", false);
            guns[selectedWeaponIndex].gameObject.SetActive(false);
        

        }


        if (Input.GetKeyDown(KeyCode.R)&& guns[selectedWeaponIndex].canReload)
        {
            guns[selectedWeaponIndex].Reload();
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchToWeaponIndex(0);
            gunDisplay.displayOn();
           
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchToWeaponIndex(1);
            gunDisplay.displayOn();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchToWeaponIndex(2);
            gunDisplay.displayOn();
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            switchToNextGun();
            gunDisplay.displayOn();
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            switchToPreviousGun();
            gunDisplay.displayOn();
        }

    }


}
