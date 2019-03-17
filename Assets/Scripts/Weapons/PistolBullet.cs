using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{ 
    Rigidbody rb;
    public float BulletSpeed;
    public LayerMask genericHitEffectMask;
    public GameObject genericHitEffectPrefab;
    public LayerMask bloodHitEffectMask;
    public GameObject bloodHitEffectPrefab;

    public float Damage=1;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(0,0,BulletSpeed,ForceMode.Impulse);
	}
	
    void OnCollisionEnter(Collision col)
    {
        
        if( col.collider.gameObject.layer == 11)
        {
            DoGenericHitFX(col.contacts[0]);
        }
        if (col.collider.gameObject.layer == 12)
        {
            DoBloodHitFX(col.contacts[0]);
         
        }
        if (col.collider.gameObject.layer == 13)
        {
            Debug.Log(col.gameObject);
            col.gameObject.GetComponentInParent<AIWolf>().wolfTakeDamage(Damage);
            DoBloodHitFX(col.contacts[0]);

        }
        if (col.collider.gameObject.layer == 14)
        {
            col.gameObject.GetComponentInParent<AIEnemy>().enemyTakeDamage(Damage);
            DoBloodHitFX(col.contacts[0]);

        }

        DestroyBullet();
    }



    void DoGenericHitFX(ContactPoint p)
    {
        Vector3 normal = p.normal;

        Vector3 pos = p.point;

        Instantiate(genericHitEffectPrefab, pos, Quaternion.LookRotation(normal));
    }


    void DoBloodHitFX(ContactPoint p)
    {
        Vector3 normal = p.normal;

        Vector3 pos = p.point;

        Instantiate(bloodHitEffectPrefab, pos, Quaternion.LookRotation(normal));
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }  
}
