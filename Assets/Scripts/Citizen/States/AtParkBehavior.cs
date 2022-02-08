using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtParkBehavior : StateMachineBehaviour
{
    public float minDistanceDelta = 1f;
    public int currSpot;
    public float timeSpendInPhase = 30f;

    float endTime;
    MeetingPoint meetingPoint;
    Vector3 targetPos;
    CitizenState citizenState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        citizenState = animator.gameObject.GetComponent<CitizenState>();
        meetingPoint = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>().GetMeetingPoint(citizenState.meetingPoint);
        meetingPoint.AddCitizen(citizenState);

        int personToLookAt = -1;
        int thisSpot = animator.gameObject.GetComponent<CitizenState>().meetingSpot;
        while (personToLookAt == -1 || personToLookAt == thisSpot)
        {
            personToLookAt = Random.Range(0, meetingPoint.spots.Count);
        }

        endTime = Time.time + timeSpendInPhase;

        Vector3 parkPos = meetingPoint.spots[personToLookAt].position;
        targetPos = new Vector3(parkPos.x, animator.gameObject.transform.position.y, parkPos.z);
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (meetingPoint.citizens.Count > meetingPoint.maxCitizensAllowed)
        {
            citizenState.isSafe = false;
        }
        else
        {
            citizenState.isSafe = true;
        }

        animator.gameObject.transform.LookAt(targetPos);
        if (Time.time > endTime)
        {
            animator.SetTrigger("Done");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        meetingPoint.RemoveCitizen(citizenState);
        meetingPoint.FreeSpot(currSpot);
        animator.ResetTrigger("AtMeetingPoint");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
