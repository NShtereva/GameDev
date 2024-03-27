using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class AttackBehavior : StateMachineBehaviour
{
    private Rigidbody2D rb;

    private Transform player;
    private Vector3 vectorToPlayer;
    
    private float speed = 2f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject playerGameObject = GameObject.FindWithTag("Player");

		if(playerGameObject == null) 
        {
			Debug.LogError("No GameObject with the \"Player\" tag found");
		} 
        else 
        {
			player = playerGameObject.transform;
		}

        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        vectorToPlayer = player.position - animator.transform.position;
        rb.velocity = new Vector2(vectorToPlayer.x * speed, rb.velocity.y);
	}
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = new Vector2(vectorToPlayer.x / speed, rb.velocity.y);
    }
}
