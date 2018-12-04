using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    NavMeshAgent nma;
    public Transform target;
    Vector3 lastTargetPos;
    Animator anim;
    public ShootingScript gun;
    public ObjectDestroyer clearBodies;
    public Collider hitbox;
    public Collider mCollider;
    public Rigidbody mRB;



    bool isReloading;
    bool canShoot = true;
    int numBullets = 6;
    float reloadTimer = 0f;
    float distFromTarget;

    [Header("Enemy Settings")]
    public float enemyHealth = 2;
    public bool enemyAlive = true;
    public float AggroDist = 10f;
    public float StoppingDist = 5f;
    public float ShootAngleThreshold = 5f;
    public float turningSpeed = 2f;
    public float reloadTime = 4f;
    public float timeToShoot = 1f;
    public int clipSize = 6;

    [Header("Audio Settings")]
    public AudioSource PlayerSounds;
    public AudioClip shotClip;
    public AudioClip reloadClip;

    // Use this for initialization
    void Start ()
    {
        nma = GetComponent<NavMeshAgent>();
        lastTargetPos = target.position;
        anim = GetComponent<Animator>();
        nma.stoppingDistance = StoppingDist;
        clearBodies = GetComponent<ObjectDestroyer>();
        enemyAlive = true;
        mRB = GetComponent<Rigidbody>();
        mCollider = GetComponent<Collider>();
    }

    void enemyDeath()
    {

        enemyAlive = false;
        anim.SetBool("enemyAlive", false);
        GameManagerScr._instance.enemyCounterIncrease();
        clearBodies.enabled = true;
        hitbox.enabled = false;
        mRB.isKinematic = true;
        mCollider.enabled = false;

    }

    public void enemyTakeDamage()
    {

        enemyHealth--;
        if (enemyHealth == 0)
        {
            Debug.Log("Dead");
            enemyDeath();
        }
        else
        {
            anim.SetTrigger("enemyHurt");
        }






    }


    void UpdateAnims()
    {
        Vector3 s = nma.transform.InverseTransformDirection(nma.velocity).normalized;
        float speed = nma.velocity.magnitude / nma.speed;
        float turn = s.x;

        anim.SetFloat("Forward", speed);
        anim.SetFloat("Turn", turn);
    }

    float AngleDiffFromPlayer()
    {
        return Vector3.Angle(gun.bulletSpawn.forward, target.position - transform.position);
    }

    bool WithingShootAngleThreshold()
    {
        return AngleDiffFromPlayer() < ShootAngleThreshold;
    }

    bool InRangeOfTarget()
    {

        distFromTarget = Vector3.Distance(transform.position, target.position);



        if (distFromTarget < StoppingDist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsAwareOfTarget()
    {
        distFromTarget = Vector3.Distance(transform.position, target.position);
        


        if (distFromTarget < AggroDist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ChasingPlayer()
    {
        if (lastTargetPos != target.position)
        {
            NavMeshHit hit;

            if (NavMesh.SamplePosition(target.position, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                nma.SetDestination(hit.position);
                
            }
        }
    }

    void Reloading()
    {
        reloadTimer += Time.deltaTime;
        if(reloadTimer > reloadTime)
        {
            numBullets = clipSize;
            isReloading = false;
        }
    }

    void ResetCanShoot()
    {
        canShoot = true;
    }

    void Shoot()
    {
        gun.enemyShoot();
        numBullets--;
        PlayerSounds.PlayOneShot(shotClip);
        anim.SetTrigger("Shoot");

        if(numBullets == 0)
        {
            isReloading = true;
            PlayerSounds.PlayOneShot(reloadClip);
        }
        else
        {
            canShoot = false;
            Invoke("ResetCanShoot", timeToShoot);
        }

    }

    bool FullyAiming()
    {
        float dot = Vector3.Dot(gun.transform.forward, Vector3.up);
        return Mathf.Abs(dot) < 0.1f;
    }

    void ShootingPlayer()
    {
        if (nma.velocity.sqrMagnitude > 0.1f) return;


        Quaternion diff = Quaternion.Inverse(gun.bulletSpawn.rotation) * Quaternion.LookRotation(target.position - transform.position);
        Quaternion targetRot = transform.rotation * diff;

        //turn to face player
        //Vector3 aimDir = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turningSpeed);


        //if within shooting angle threshold, try and shoot
        if (WithingShootAngleThreshold())
        {
            anim.SetBool("Aim", true);
            if (canShoot && FullyAiming()) Shoot();
        }
        else
        {
            anim.SetBool("Aim", false);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (!enemyAlive) return;

        if (isReloading)
        {
            Reloading();
            //Debug.Log("Reloading");
        }
        else if (IsAwareOfTarget())
        {
            anim.SetBool("Aim", false);
            ChasingPlayer();
            
            //Debug.Log("isChasingPlayer");
            if (InRangeOfTarget())
            {
                
                ShootingPlayer();
                //Debug.Log("ShootingPlayer");
            }
        }
        
        


        UpdateAnims();
        lastTargetPos = target.position;
        if (nma.stoppingDistance != StoppingDist) nma.stoppingDistance = StoppingDist;
	}
}
