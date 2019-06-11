using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    public Waypoint[] Waypoints;
    public bool Patrolling = true;
    public int CurrentWaypointIndex = 0;


    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //navMeshAgent.autoBraking = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            CurrentWaypointIndex = (CurrentWaypointIndex + 1) % Waypoints.Length;
            navMeshAgent.SetDestination(Waypoints[CurrentWaypointIndex].transform.position);
            StartCoroutine(WaitSeconds(Waypoints[CurrentWaypointIndex].WaitTime));
        }
        
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
