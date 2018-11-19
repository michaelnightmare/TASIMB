using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraTarget : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;
    Vector3 offset;

    void Start()
    {
        offset =  transform.position - target.position;
    }

	// Update is called once per frame
	void Update ()
    {
        Vector3 dest = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * followSpeed);
	}
}
