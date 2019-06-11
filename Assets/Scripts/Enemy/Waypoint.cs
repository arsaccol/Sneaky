using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public float WaitTime = 3f; // In seconds


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawIcon(transform.position, "MapPinIcon", allowScaling: true);
        
    }
}
