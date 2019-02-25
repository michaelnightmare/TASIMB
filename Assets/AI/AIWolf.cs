using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWolf : MonoBehaviour
{
    NavMeshAgent nma;
    public Transform target;
    Vector3 lastTargetPos;
    Animator anim;
    public ObjectDestroyer clearBodies;
    public GameObject spawnedItemRef;
    
    public Collider hitbox;
    Rigidbody mRB;
    Collider mCollider;
    public bool disableAI= false;

    bool canAttack = true;
    bool isCoolingDown = true;
    float CoolDownTimer = 0f;
    float distFromTarget;


    
    [Header("Wolf Settings")]
    public PlayerScript playerInteraction;
    public float wolfHealth = 2;
    public bool wolfAlive = true;
    public float AggroDist = 10f;
    public float StoppingDist = 5f;
    public float ShootAngleThreshold = 5f;
    public float turningSpeed = 2f;
    public float reloadTime = 4f;
    public float timeToAttack;
   

    [Header("Audio Settings")]
    public AudioSource PlayerSounds;
    public AudioClip shotClip;

    bool initialized = false;

    // Use this for initialization
    void Start ()
    {
       if(!initialized) Initialize();
       
    }



    void Initialize()
    {
        nma = GetComponent<NavMeshAgent>();
        
        if(target== null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        lastTargetPos = target.position;
        anim = GetComponent<Animator>();
   
        clearBodies = GetComponent<ObjectDestroyer>();
        wolfAlive = true;
        mRB = GetComponent<Rigidbody>();
        mCollider = GetComponentInChildren<Collider>();
        initialized = true;
    }

    void onEnable()
    {
        Initialize();
    }


    void wolfDeath()
    {

        wolfAlive = false;
        anim.SetBool("dead", true);
        Invoke("spawnDrop", 2);
        clearBodies.enabled = true;

        hitbox.enabled = false;
        mCollider.enabled = false;
        mRB.isKinematic = true;
        Debug.Log("boxcolliderdisabled");
    }

    public void wolfTakeDamage(float Damage)
    {
        anim.SetBool("walk", false);
        wolfHealth-= Damage;
        if (wolfHealth == 0)
        {
            Debug.Log("dead");
            wolfDeath();
        }
        else
        {
            
            anim.SetTrigger("wolfHurt");
        }

    }


    float AngleDiffFromPlayer()
    {
        return Vector3.Angle(transform.forward, target.position - transform.position);
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
        if (!initialized) Initialize();

        if (lastTargetPos != target.position)
        {
            NavMeshHit hit;

            if (NavMesh.SamplePosition(target.position, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                nma.SetDestination(hit.position);
                
            }
        }
    }


    void ResetCanAttack()
    {
        canAttack = true;
        isCoolingDown = true;
    }

    void Attack()
    {
       
        if (isCoolingDown)
        {
            PlayerSounds.PlayOneShot(shotClip);
            anim.SetBool("attack",true);
            playerInteraction.playerTakeDamage();
            isCoolingDown = false;
           
        }
        else 
        {
            canAttack = false;
            Debug.Log("working");
            anim.SetBool("attack", false);
            Invoke("ResetCanAttack", 3f);
        }

    }

    bool FullyAiming()
    {
        float dot = Vector3.Dot(transform.forward, Vector3.up);
        return Mathf.Abs(dot) < 0.1f;
    }

    void attackingPlayer()
    {
        if (nma.velocity.sqrMagnitude > 0.1f) return;


        Quaternion diff = Quaternion.Inverse(transform.rotation) * Quaternion.LookRotation(target.position - transform.position);
        Quaternion targetRot = transform.rotation * diff;

        //turn to face player
        //Vector3 aimDir = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turningSpeed);

        if (WithingShootAngleThreshold())
        {
           
            if (canAttack && FullyAiming()) Attack();
        }
       
    }

    public void spawnDrop()
    {
        GameObject itemTemp = Instantiate(spawnedItemRef, transform.position, Quaternion.identity) as GameObject;
        //steakTemp.GetComponent<SteakScr>().target = target.GetComponent<PlayerScript>();
        itemTemp.GetComponent<SteakScr>().playerInteraction = target.GetComponent<PlayerScript>();
    }






    public void EnableAi()
    {
        disableAI = false;
    }
    public void DisableAi()
    {
        disableAI = true;
    }

    public void enemyMoveToPoint(Vector3 eventTargetLocation)
    {
        if (!initialized) Initialize();
        NavMeshHit hit;

        if (NavMesh.SamplePosition(eventTargetLocation, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            nma.SetDestination(hit.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!wolfAlive) return;


        if (IsAwareOfTarget())
        {
            anim.SetBool("walk", true);

            ChasingPlayer();


            if (InRangeOfTarget())
            {

                anim.SetBool("walk", false);
                attackingPlayer();

            }
        }


        //UpdateAnims();
        lastTargetPos = target.position;
        if (nma.stoppingDistance != StoppingDist) nma.stoppingDistance = StoppingDist;
    }
}
