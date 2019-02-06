using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScr : MonoBehaviour
{
    public Transform spawn;

   
    public void rayCastShot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float shotDistance = 20;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            shotDistance = hit.distance;
        }

        Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
    }
   


}
