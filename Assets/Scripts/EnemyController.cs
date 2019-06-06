using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float ViewDistance = 1f;
    public float AlertTime = 10f;
    public float FieldOfView = 5f;
    private bool Alarmed = false;

    public Vector3 LastPlayerSighting = Vector3.positiveInfinity;


    private Animator sirenAnimator;

    private void Start()
    {
        sirenAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        checkSpottedPlayerWithAngle();
        var navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if(Alarmed)
        {
            navMeshAgent.SetDestination(LastPlayerSighting);
        }
    }

    IEnumerator AlarmWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        sirenAnimator.SetBool("IsSeeingPlayer", false);
        Alarmed = false;
    }


    private void checkSpottedPlayerWithAngle()
    {
        Debug.DrawRay(transform.position, transform.forward * ViewDistance, Color.red);
        var player = GameObject.FindGameObjectWithTag("Player");

        var hereToPlayer = player.transform.position - transform.position;
        //Debug.DrawRay(transform.position, hereToPlayer, Color.magenta);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, hereToPlayer, out hit, ViewDistance))
        {
            if(hit.transform.gameObject.tag == "Player")
            {
                if(Vector3.Distance(transform.position, player.transform.position) <= ViewDistance)
                {
                    if(Vector3.Angle(hereToPlayer, transform.forward) <= FieldOfView)
                        alarm(player.transform.position);
                }
            }
        }
    }


    private void alarm(Vector3 sightingPosition)
    {
        LastPlayerSighting = sightingPosition;
        Alarmed = true;
        if(sirenAnimator.GetBool("IsSeeingPlayer") == false)
        {
            sirenAnimator.SetBool("IsSeeingPlayer", true);
            StartCoroutine(AlarmWait(AlertTime));
        }
    }


    private void checkSpottedPlayerStraightForward()
    {
        RaycastHit hit;
        Ray visionCenterRay = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(visionCenterRay, out hit, ViewDistance))
        {
            if(hit.collider.tag == "Player")
            {
                alarm(hit.transform.position);
            }
        }
    }
}

