using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    public GameObject knife;
    Rigidbody rb;
    public LayerMask bloodHitEffectMask;
    public GameObject bloodHitEffectPrefab;
    public LayerMask genericHitEffectMask;
    public GameObject genericHitEffectPrefab;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision col)
    {

        if (col.collider.gameObject.layer == 11)
        {
            DoGenericHitFX(col.contacts[0]);
        }

        if (col.collider.gameObject.layer == 12)
        {
            DoBloodHitFX(col.contacts[0]);
        }
     
    }

    void DoBloodHitFX(ContactPoint p)
    {
        Vector3 normal = p.normal;

        Vector3 pos = p.point;

        Instantiate(bloodHitEffectPrefab, pos, Quaternion.LookRotation(normal));
    }

    void DoGenericHitFX(ContactPoint p)
    {
        Vector3 normal = p.normal;

        Vector3 pos = p.point;

        Instantiate(genericHitEffectPrefab, pos, Quaternion.LookRotation(normal));
    }
}
