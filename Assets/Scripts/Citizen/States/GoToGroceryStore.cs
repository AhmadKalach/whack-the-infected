using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToGroceryStore : StateMachineBehaviour
{
    public float speed = 1.8f;
    public float timeSpendInPhase = 10f;
    float endTime;
    CitizenState citizenState;
    HouseScript groceryStore;
    Vector3 targetPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        citizenState = animator.GetComponent<CitizenState>();
        TownManager townManager = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>();
        groceryStore = townManager.GetGroceryStore();
        Vector3 groceryPos = groceryStore.transform.position;

        endTime = Time.time + timeSpendInPhase;

        targetPos = new Vector3(groceryPos.x, animator.transform.position.y, groceryPos.z);
        animator.GetComponent<NavMeshAgent>().speed = speed;
        animator.GetComponent<NavMeshAgent>().SetDestination(targetPos);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (groceryStore.citizens.Count > groceryStore.maxNonResidents)
        {
            citizenState.isSafe = false;
        }
        else
        {
            citizenState.isSafe = true;
        }


        if (Time.time > endTime)
        {
            animator.SetTrigger("Done");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("GoToGrocery");
        animator.GetComponent<CitizenState>().isSafe = true;
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
