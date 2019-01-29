using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{

    bool aiming = false;
    Animator anims;
    public GunScript gun;
    public bool aimWithMouse = false;
    public float aimSpeed = 10f;
    public float aimRotationOffset = 20f;
    public float fireRate = .5f;
    public float nextShot = 0.0f;
    public Transform shootT;

    public bool disabledShooting = false;
    //public Image reticle;
    //RectTransform reticleRec;
    //RectTransform parentRec;
    // Use this for initialization
    void Start () {
        anims = GetComponent<Animator>();
        gun = GetComponentInChildren<GunScript>();
        //reticleRec = reticle.GetComponent<RectTransform>();
        //parentRec = reticle.transform.parent.GetComponent<RectTransform>();
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = false;
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
            gun.gameObject.SetActive(true);
            


            if (Input.GetKeyDown(KeyCode.Mouse0)&& Time.time > nextShot)
            {

                if (gun.canShoot)
                {
                    anims.SetTrigger("Shoot");
                    gun.Shoot();
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
            gun.gameObject.SetActive(false);
            //reticle.enabled = false;

        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.Reload();
            
        }

    }
}
