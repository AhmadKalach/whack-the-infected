using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoingBackHome : StateMachineBehaviour
{
    public float speed = 2.6f;
    Vector3 targetPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 homePos = animator.gameObject.GetComponent<CitizenState>().home.transform.position;
        targetPos = new Vector3(homePos.x, animator.gameObject.transform.position.y, homePos.z);
        animator.GetComponent<NavMeshAgent>().speed = speed;
        animator.GetComponent<NavMeshAgent>().SetDestination(targetPos);
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.gameObject.transform.position, targetPos) < 0.3f)
        {
            animator.SetTrigger("BackHome");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Whacked");
        animator.ResetTrigger("Done");
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
