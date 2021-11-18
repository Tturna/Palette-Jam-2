using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : StateMachineBehaviour
{
    public Transform playerPos;
    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        if(ValueManager.instance != null)
        {
            if(ValueManager.instance.debuggable)
            {
                ValueManager.instance.enemySpeed.text = speed.ToString();
            }
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ValueManager.instance != null)
        {
            if(ValueManager.instance.debuggable)
            {
                if(ValueManager.instance.valueManager.activeInHierarchy == true)
                {
                    if(!string.IsNullOrEmpty(ValueManager.instance.enemySpeed.text))
                    {
                        speed = int.Parse(ValueManager.instance.enemySpeed.text);
                    }
                }
            }
        }

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isFollowing", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
