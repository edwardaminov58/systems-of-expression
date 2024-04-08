using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_float : StateMachineBehaviour
{
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}
    //IEnumerator moveForward(float inTime)
    //{
    //    Vector3 currentPosition = transform.position;
    //    Vector3 toPosition = currentPosition + transform.forward * ForwardDist;
    //    for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
    //    {
    //        transform.position = Vector3.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
    //        //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
    //        //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
    //        yield return null;
    //    }
    //    transform.position = toPosition;
    //    //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
    //    moving = false;
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
