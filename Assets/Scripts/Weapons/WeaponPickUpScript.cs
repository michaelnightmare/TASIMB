using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUpScript : MonoBehaviour
{

    public float speed;
    public int weaponIndex = 0;

    // Use this for initialization
    void Start ()
    {
  
    }

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            ShootingController gunController = other.gameObject.GetComponent<ShootingController>();
            gunController.PickupWeapon(weaponIndex);
            Destroy(gameObject);
        }
    }
}
