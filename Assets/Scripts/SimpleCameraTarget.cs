using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraTarget : MonoBehaviour
{
    Transform defaultTarget;
    public Transform target;
    public float followSpeed = 2f;
    Vector3 offset;

    void Start()
    {
        offset =  transform.position - target.position;
        defaultTarget = target;
    }

    public void SetFollowTarget(Transform t)
    {
        target = t;
    }


    public void Reset()
    {
        target = defaultTarget;
    }

	// Update is called once per frame
	void Update ()
    {
        Vector3 dest = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * followSpeed);
	}
}
