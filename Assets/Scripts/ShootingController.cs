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
    //public Image reticle;
    //RectTransform reticleRec;
    //RectTransform parentRec;
    // Use this for initialization
    void Start () {
        anims = GetComponent<Animator>();
        guns[0].isUnlocked = true;
        guns[0].canReload = true;
        //reticleRec = reticle.GetComponent<RectTransform>();
        //parentRec = reticle.transform.parent.GetComponent<RectTransform>();
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = false;
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
                //reticle.enabled = true;
                /*
                bool hitEnemy = false;
                RaycastHit hit;
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(mouseRay, out hit, Mathf.Infinity, LayerMask.GetMask("enemyCowboy")))
                {
                    hitEnemy = true;
                }

               
                if (false)
                {
                    aimDir = hit.collider.transform.position - transform.position;
                    aimDir.Scale(new Vector3(1f, 0, 1f));
                }
                */



                /*
                Vector3 aimDir = Vector3.zero;
                Vector3 charScreenPos = Camera.main.WorldToScreenPoint(transform.position);
                Vector2 aimDir2D = Input.mousePosition - charScreenPos;
                aimDir = new Vector3(aimDir2D.x, 0, aimDir2D.y);
                aimDir.Normalize();
                */

                Vector3 gunDir = shootT.transform.forward;
                Vector2 gunDir2d = new Vector2(gunDir.x, gunDir.z);

                Vector2 mousePos = Input.mousePosition;
                Vector2 gunPos = Camera.main.WorldToScreenPoint(shootT.transform.position);
                Vector2 aimDir2d = gunPos - mousePos;

                float diff = Vector2.SignedAngle(aimDir2d, gunDir2d);
                Quaternion targetRot = transform.rotation * Quaternion.Euler(0f, diff, 0);

                //Vector3 reticlePos3d = shootT.transform.position + shootT.transform.forward * aimDir2d.magnitude /20f;
               // Vector2 pos = Camera.main.WorldToScreenPoint(reticlePos3d);
                //reticleRec.position = pos;

                /*
                Debug.DrawLine(transform.position, transform.position + aimDir * 2f);

                Quaternion diff = Quaternion.Inverse(Quaternion.LookRotation(shootT.forward)) * Quaternion.LookRotation(-aimDir);
                Quaternion targetRot = transform.rotation * diff;
                */
                //Quaternion targetRot = Quaternion.LookRotation(aimDir) * Quaternion.Euler(0,aimRotationOffset, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * aimSpeed);

            }
        }
        else
        {
            aiming = false;
            anims.SetBool("Aim", false);
            guns[selectedWeaponIndex].gameObject.SetActive(false);
            //reticle.enabled = false;

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
