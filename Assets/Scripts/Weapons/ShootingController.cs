using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ShootingController : MonoBehaviour
{

    bool aiming = false;
    Animator anims;
    public List<GunScript> guns = new List<GunScript>();
    public bool aimWithMouse = false;
    public float aimSpeed = 30f;
    public float fireRate = .5f;
    public float nextShot = 0.0f;
    public Transform shootT;
    public Transform singleHandShootT;
    public Transform rifleShootT;
    public int selectedWeaponIndex = 0;
    public RayCastScr raycastgun;

    public RuntimeAnimatorController defaultController;
    public AnimatorOverrideController rifleOverrideAnims;
    public GunDisplayScr gunDisplay;
    public PauseScript menu;

    public void PickupWeapon(int weaponIndex)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (i == weaponIndex)
            {
                guns[weaponIndex].Unlock();
            }
        }
    }

    void switchToWeaponIndex(int index)
    {
        if (index >= 0 && index < guns.Count)
        {
            if (guns[index].isUnlocked)
            {
                guns[selectedWeaponIndex].EquipGun(false);
                selectedWeaponIndex = index;
                guns[selectedWeaponIndex].EquipGun(true);

                if (selectedWeaponIndex == 2)
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
                index = guns.Count - 1;

            }

            foundGun = guns[index].isUnlocked;
        }

        switchToWeaponIndex(index);
    }

    public bool disabledShooting = false;

    void Start()
    {
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
    void Update()
    {
        if (disabledShooting) return;

        if (InputAim())
        {
            aiming = true;
            anims.SetBool("Aim", true);
            guns[selectedWeaponIndex].gameObject.SetActive(true);
            raycastgun.rayCastShot();
            Debug.Log(Input.mousePosition);

            if (InputShoot() && Time.time > nextShot)
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

            Quaternion dir = InputGetPlayerAimRotation();
            if (dir != Quaternion.identity)
            {
                transform.rotation = dir;
            }
        }
        else
        {
            aiming = false;
            anims.SetBool("Aim", false);
            guns[selectedWeaponIndex].gameObject.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.R) || Input.GetButton("Reload") && guns[selectedWeaponIndex].canReload && menu.GameIsPaused==false)
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

        if (InputCycleGunForward() || Input.GetButtonDown("GunScroll"))
        {
            switchToNextGun();
            gunDisplay.displayOn();
        }
        else if (InputCycleGunBackward())
        {
            switchToPreviousGun();
            gunDisplay.displayOn();
        }
    }



    #region Input

    bool InputAim()
    {
        return (Input.GetKey(KeyCode.Mouse1) || CrossPlatformInputManager.GetButton("Aim")) || Input.GetAxis("Aim") > 0f || false;
    }

    bool InputShoot()
    {
        return Input.GetKeyDown(KeyCode.Mouse0) || CrossPlatformInputManager.GetButton("Shoot") || Input.GetAxis("Shoot") > 0f || false; 
    }

    bool InputCycleGunForward()
    {
      

        return Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("GunScroll") > 75f && Input.GetAxis("GunScroll") > .64f || false; //put xbox controls here
    }

    bool InputCycleGunBackward()
    {
        return Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetAxis("GunScroll") < -.75f && Input.GetAxis("GunScroll") > .64f || false; //put xbox controls here
    }

    Quaternion InputGetPlayerAimRotation()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Aiming with mouse");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane p = new Plane(Vector3.up, shootT.transform.position);
            float dist;

            p.Raycast(ray, out dist);
            Vector3 intersectionPoint = ray.GetPoint(dist);
            Vector3 aimDir = intersectionPoint - transform.position;
            aimDir.y = 0;

            float yDiff = Vector3.SignedAngle(transform.forward, shootT.forward, Vector3.up);

            Quaternion targetRot = Quaternion.LookRotation(aimDir) * Quaternion.Euler(0f, -yDiff, 0f);
            return Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * aimSpeed);
        }
        else
        {
            Vector3 inputAimDir = new Vector3(-Input.GetAxis("xboxAimHorizontal"), 0, Input.GetAxis("xboxAimVertical"));
            if (inputAimDir.magnitude < 0.1) return Quaternion.identity;
            inputAimDir.Normalize();
            return Quaternion.LookRotation(inputAimDir);
        }
    }





    #endregion

}