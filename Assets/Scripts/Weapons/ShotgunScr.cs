using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScr : MonoBehaviour
{
    public float speed = 10f;
    public WeaponSwitching weapons;
    public bool shotgunAvailable = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            
            Debug.Log("Picked Up Shotgun");
            Destroy(gameObject);
            shotgunAvailable = true; 
         
        }
    }
}
