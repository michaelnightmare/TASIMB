using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUpScript : MonoBehaviour {

    public GunScript gun;
    public float speed;
    public ShotgunScr shotgun;
    public RifleScr rifle;
    public GameObject rifleObject;
    public GameObject shotgunObject;
    public GameObject player;
	// Use this for initialization
	void Start ()
    {
        if (gun == null)
        {
           
            if (shotgunObject)
            {
                shotgun.shotgunAvailable = true;
                Debug.Log("you got the shotgun");
                

           }
            if (rifleObject)
            {
                rifle.rifleAvailable = true;
                Debug.Log("you got the rifle");
                
            
            }

        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {

            gun.Unlock();
            Destroy(gameObject);
        }
    }
}
