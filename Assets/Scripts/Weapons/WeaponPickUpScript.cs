using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUpScript : MonoBehaviour {

    public GunScript gun;
    public float speed;
    public GameObject shotgun;
    public GameObject rifle; 
	// Use this for initialization
	void Start ()
    {
        if (gun == null)
        {
           
                
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
