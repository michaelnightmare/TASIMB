using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [Header("Generic Destroyer")]
    public float destoryTime;


    [Header("Particle Destroyer")]
    public bool isParticleSystem = false;

    ParticleSystem particles;

	// Use this for initialization
	void Start ()
    {
        if (isParticleSystem)
        {
            particles = GetComponent<ParticleSystem>();
        }
        else
        {
            Invoke("DestroySelf", destoryTime);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isParticleSystem)
        {
            if (!particles.IsAlive())
            {
                DestroySelf();
            }
        }
        else
        {

        }
	}

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
