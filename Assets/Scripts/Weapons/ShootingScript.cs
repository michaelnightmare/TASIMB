using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletSpawn;
    public GameObject enemyBullet;


    public virtual void Shoot()
    {
        CastForEnemy(); // Cast ray first so we don't collide with the bullet.
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }
    public virtual void enemyShoot()
    {
    
            Instantiate(enemyBullet, bulletSpawn.position, bulletSpawn.rotation);
      
    }

    private void CastForEnemy()
    {
        //Make a ray, so we can set a bool for AI if they're in danger of getting shot up
        RaycastHit hit;
        int layerMask = 1 << 14; //this searches on the enemyCowboy layer only, more work will be needed if we have multiple enemy layers and want enemies to know there are incoming bullets.

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, layerMask))
        {
            //we hit something
            if (hit.collider.gameObject.GetComponent<AIEnemy>() != null)
            {
                hit.collider.gameObject.GetComponent<AIEnemy>().ShotAtByPlayer();
            }
        }

    }
}
