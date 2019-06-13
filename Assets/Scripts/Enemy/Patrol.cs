using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    public Waypoint[] Waypoints;
    public bool Patrolling = true;
    public int WaypointIndex = 0;


    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.autoBraking = false;
    }

    void Update()
    {
        //if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 1f)
        //{
        //    WaypointIndex = (WaypointIndex + 1) % Waypoints.Length;
        //    navMeshAgent.SetDestination(Waypoints[WaypointIndex].transform.position);

        //}
        StartCoroutine(PatrolCoroutine());
    }

    IEnumerator PatrolCoroutine()
    {
        if(navMeshAgent.remainingDistance < 1f)
        {
            yield return new WaitForSeconds(Waypoints[WaypointIndex].WaitTime);
            WaypointIndex = (WaypointIndex + 1) % Waypoints.Length;
            navMeshAgent.SetDestination(Waypoints[WaypointIndex].transform.position);
        }


    }


}
