using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  

    // Use this for initialization
    void Start () {
        anims = GetComponent<Animator>();
        gun = GetComponentInChildren<GunScript>();
       
    }
	
	// Update is called once per frame
	void Update ()
    {
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
                Vector3 charScreenPos = Camera.main.WorldToScreenPoint(transform.position);
                Vector2 aimDir2D = charScreenPos - Input.mousePosition;


                Vector3 aimDir = new Vector3(aimDir2D.x, 0, aimDir2D.y);
                aimDir = Vector3.Scale(aimDir, new Vector3(1, 0, 1));
                aimDir.Normalize();

                Debug.DrawLine(transform.position, transform.position + aimDir * 2f);


                Quaternion targetRot = Quaternion.LookRotation(aimDir) * Quaternion.Euler(0,aimRotationOffset, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * aimSpeed);

            }
        }
        else
        {
            aiming = false;
            anims.SetBool("Aim", false);
            gun.gameObject.SetActive(false);

        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.Reload();
            
        }

    }
}
