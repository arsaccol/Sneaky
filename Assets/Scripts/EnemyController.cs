using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float ViewDistance = 1f;
    public float AlertTime = 10f;
    public float FieldOfView = 5f;


    private Animator sirenAnimator;

    private void Start()
    {
        sirenAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * ViewDistance, new Color(0xca / 255f, 0x2c / 255f, 0x92 / 255f));

        //checkSpottedPlayerStraightForward();
        checkSpottedPlayerWithAngle();
    }

    IEnumerator AlarmWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        sirenAnimator.SetBool("IsSeeingPlayer", false);
    }


    private void checkSpottedPlayerWithAngle()
    {
        Debug.DrawRay(transform.position, transform.forward * ViewDistance, Color.red);
        var player = GameObject.FindGameObjectWithTag("Player");

        var hereToPlayer = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, hereToPlayer, Color.magenta);

        if(Vector3.Distance(transform.position, player.transform.position) <= ViewDistance)
        {
            if(Vector3.Angle(hereToPlayer, transform.forward) <= FieldOfView)
                alarm();
        }
    }


    private void alarm()
    {
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
                alarm();
            }
        }
    }
}

