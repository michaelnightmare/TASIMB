using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIBase : MonoBehaviour
{
    protected NavMeshAgent nma;
    public Transform target;
    protected Vector3 lastTargetPos;
    protected Animator anim;
    public ObjectDestroyer clearBodies;

    public Collider hitbox;
    protected Rigidbody mRB;
    protected Collider mCollider;
    public bool disableAI = false;

    public float health = 2;
    public bool isAlive = true;
    public float aggroDist = 10f;
    public float stoppingDist = 5f;
    public float shootAngleThreshold = 5f;
    public float turningSpeed = 2f;
    public float reloadTime = 4f;
    public float timeToAttack;

    public bool initialized = false;

    public bool canAttack = true;
    public float distFromTarget;

    [Header("Audio Settings")]
    public AudioSource PlayerSounds;
    public AudioClip shotClip;

    public UnityEvent OnEnemyDeath = new UnityEvent();


    protected virtual void Initialize()
    {
        nma = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        lastTargetPos = target.position;
        anim = GetComponent<Animator>();

        clearBodies = GetComponent<ObjectDestroyer>();
        isAlive = true;
        mRB = GetComponent<Rigidbody>();
        mCollider = GetComponentInChildren<Collider>();
        initialized = true;
    }

    void onEnable()
    {
        Initialize();
    }

    public virtual void Death()
    {
        OnEnemyDeath.Invoke();
        isAlive = false;
        anim.SetBool("dead", true);
        clearBodies.enabled = true;
        hitbox.enabled = false;
        mCollider.enabled = false;
        mRB.isKinematic = true;
        nma.SetDestination(transform.position);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("dead");
            Death();
            
        }
      
    }

    // Use this for initialization
    void Start ()
    {
        if (!initialized)
            Initialize();
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
        NavMeshHit hit;

        if (NavMesh.SamplePosition(eventTargetLocation, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            nma.SetDestination(hit.position);
        }
    }

    public virtual float AngleDiffFromPlayer()
    {
        return Vector3.Angle(transform.forward, target.position - transform.position);
    }

    public bool WithinShootAngleThreshold()
    {
        return AngleDiffFromPlayer() < shootAngleThreshold;
    }

    public bool InRangeOfTarget()
    {

        distFromTarget = Vector3.Distance(transform.position, target.position);



        if (distFromTarget < stoppingDist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsAwareOfTarget()
    {
        distFromTarget = Vector3.Distance(transform.position, target.position);

        if (distFromTarget < aggroDist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChasingPlayer()
    {
        if (lastTargetPos != target.position)
        {
            NavMeshHit hit;

            if (NavMesh.SamplePosition(target.position, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                float distFromPlayer = Vector3.Distance(target.position, transform.position);
                if (distFromPlayer > stoppingDist)
                {
                    nma.SetDestination(hit.position);
                }
                else
                {
                    nma.SetDestination(transform.position);
                }
            }
        }
    }

    public bool FullyAiming()
    {
        float dot = Vector3.Dot(transform.forward, Vector3.up);
        return Mathf.Abs(dot) < 0.1f;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
