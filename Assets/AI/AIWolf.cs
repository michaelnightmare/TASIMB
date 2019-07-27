using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIWolf : AIBase
{
    public GameObject spawnedItemRef;

    bool isCoolingDown = true;
    float CoolDownTimer = 0f;
    
    [Header("Wolf Settings")]
    public PlayerScript playerInteraction;

    public override void Death()
    {
        base.Death();
        Invoke("spawnDrop", 2);
    }

    public override void TakeDamage(float damage)
    {
        anim.SetBool("walk", false);

        base.TakeDamage(damage);

        if (isAlive)
        {
            anim.SetTrigger("wolfHurt");
        }

    }

    /*void ChasingPlayer()
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
    }*/


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
            playerInteraction.playerTakeDamage(-1);
            isCoolingDown = false;
           
        }
        else 
        {
            canAttack = false;
            Debug.Log("working");
            anim.SetBool("attack", false);
            Invoke("ResetCanAttack", timeToAttack);
        }

    }

    void attackingPlayer()
    {
        if (nma.velocity.sqrMagnitude > 0.1f) return;


        Quaternion diff = Quaternion.Inverse(transform.rotation) * Quaternion.LookRotation(target.position - transform.position);
        Quaternion targetRot = transform.rotation * diff;

        //turn to face player
        //Vector3 aimDir = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turningSpeed);

        if (WithinShootAngleThreshold())
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

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;

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

        lastTargetPos = target.position;
        if (nma.stoppingDistance != stoppingDist)
            nma.stoppingDistance = stoppingDist;
    }
}
