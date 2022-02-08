using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    public float maxTimeToGoOut;

    public List<string> triggerNames;
    public List<float> cumulativeTriggerProbabilities;

    public float timeToMove;
    CitizenState citizenState;
    TownManager townManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        townManager = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>();
        animator.ResetTrigger("BackHome");
        citizenState = animator.GetComponent<CitizenState>();

        if (citizenState.meetingPoint != -1 && citizenState.meetingSpot != -1)
        {
            townManager.GetMeetingPoint(citizenState.meetingPoint).FreeSpot(citizenState.meetingSpot);
        }
        citizenState.isSafe = true;
        
        timeToMove = Time.time + Random.Range(0, maxTimeToGoOut);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time > timeToMove) {
            float rand = Random.Range(0f, 0.999f);
            int id = 0;
            foreach(float proba in cumulativeTriggerProbabilities) {
                if (rand > proba)
                {
                    id++;
                }
            }
            string triggerName = triggerNames[id];
            animator.SetTrigger(triggerName);
        } 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
