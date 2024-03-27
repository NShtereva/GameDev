using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptPlayer : MonoBehaviour
{
    private float wantedDistanceToPlayer = 2f;

    private Transform player;
    private Rigidbody2D rb;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGameObject = GameObject.FindWithTag("Player");
		if (playerGameObject == null) 
        {
			Debug.LogError("No GameObject with the \"Player\" tag found");
		} 
        else 
        {
			player = playerGameObject.transform;
		}

		rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorToPlayer = player.position - animator.transform.position;
		float distanceToPlayer = vectorToPlayer.magnitude;

		if (distanceToPlayer <= wantedDistanceToPlayer) 
        {
			animator.SetBool("IsInterceptedPlayer", true);
		} 
        else 
        {
			animator.SetBool("IsInterceptedPlayer", false);
		}
    }
}
