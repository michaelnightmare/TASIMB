using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public UnityEvent onEnterTriggerZone = new UnityEvent();
    bool fired = false;

    void OnTriggerEnter(Collider other)
    {
        if (fired) return;

        if (other.gameObject.layer == 9)
        {
            onEnterTriggerZone.Invoke();
            fired = true;
            Debug.Log("ROCKS ACTIVE");
        }

    }
}
