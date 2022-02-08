using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoingToParkBehavior : StateMachineBehaviour
{
    public float speed = 1.8f;
    MeetingPoint meetingPoint;
    int currSpot;
    Vector3 targetPos;
    TownManager townManager;
    CitizenState citizenState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        townManager = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>();
        int randomMeetingPointIndex = Random.Range(0, townManager.meetingPoints.Length);
        meetingPoint = townManager.GetMeetingPoint(randomMeetingPointIndex);
        currSpot = meetingPoint.AllocateSpot();
        citizenState = animator.gameObject.GetComponent<CitizenState>();

        if (currSpot == -1)
        {
            animator.SetTrigger("BackToIdle");
        }
        else
        {
            citizenState.meetingSpot = currSpot;
            citizenState.meetingPoint = randomMeetingPointIndex;
            Vector3 spotPos = meetingPoint.spots[currSpot].position;
            targetPos = new Vector3(spotPos.x, animator.gameObject.transform.position.y, spotPos.z);
            animator.GetComponent<NavMeshAgent>().speed = speed;
            animator.GetComponent<NavMeshAgent>().SetDestination(targetPos);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.gameObject.transform.position, targetPos) < 0.3f)
        {
            animator.SetTrigger("AtMeetingPoint");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("GoingToMeetingPoint");
        citizenState.isSafe = true;
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
