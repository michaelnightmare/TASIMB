using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraTarget : MonoBehaviour
{
    Transform defaultTarget;
    public Transform target;
    public float followSpeed = 2f;
    Vector3 offset;

    public BoxCollider boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera mainCamera;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        offset =  transform.position - target.position;
        defaultTarget = target;

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;

        mainCamera = GetComponentInChildren<Camera>();
        halfHeight = mainCamera.orthographicSize; ;
        halfWidth = halfHeight * Screen.width / Screen.height;
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

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedZ = Mathf.Clamp(transform.position.z, minBounds.z + halfHeight, maxBounds.z - halfHeight);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
