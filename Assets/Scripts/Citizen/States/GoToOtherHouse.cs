using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToOtherHouse : StateMachineBehaviour
{
    public float speed = 1.8f;
    public float timeSpendInPhase = 30f;
    float endTime;
    Vector3 targetPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TownManager townManager = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>();
        CitizenState currCitizen = animator.gameObject.GetComponent<CitizenState>();
        int citizenHouseIndex = townManager.houses.IndexOf(currCitizen.home);
        int targetHouseIndex = Random.Range(0, townManager.houses.Count);
        Vector3 houseToVisitPos = townManager.houses[targetHouseIndex].transform.position;
        targetPos = new Vector3(houseToVisitPos.x, animator.transform.position.y, houseToVisitPos.z);

        endTime = Time.time + timeSpendInPhase;

        animator.GetComponent<NavMeshAgent>().speed = speed;
        animator.GetComponent<NavMeshAgent>().SetDestination(targetPos);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<CitizenState>().isSafe = false;
        if (Time.time > endTime)
        {
            animator.SetTrigger("Done");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("GoingToOtherHouse");
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
