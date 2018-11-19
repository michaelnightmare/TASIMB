using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletSpawn;
    public GameObject enemyBullet;


     public void Shoot()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }
    public void enemyShoot()
    {
        Instantiate(enemyBullet, bulletSpawn.position, bulletSpawn.rotation);
    }
}
