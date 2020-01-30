using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScriptShotgun : ShootingScript {

    public int numBullets = 3;
    public float spreadAngle = 20f;

     public override void Shoot()
    {
        Quaternion spawnRotation = bulletSpawn.rotation;

        for(int i = 0; i < numBullets; i++)
        {
            float angle = Random.Range(0f, spreadAngle);
            angle *= Random.Range(0, 2) * 2 - 1;


            Quaternion rot = spawnRotation * Quaternion.Euler(0f, angle, 0f);
            Instantiate(bullet, bulletSpawn.position, rot);
        }


    }
    public override void enemyShoot()
    {
        Quaternion spawnRotation = bulletSpawn.rotation;

        for (int i = 0; i < numBullets; i++)
        {
            float angle = Random.Range(0f, spreadAngle);
            angle *= Random.Range(0, 2) * 2 - 1;


            Quaternion rot = spawnRotation * Quaternion.Euler(0f, angle, 0f);
            Instantiate(bullet, bulletSpawn.position, rot);
        }
    }
}
