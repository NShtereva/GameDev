using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private float horizontal;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        horizontal = 0;
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("Speed", Abs(rb.velocity.x));
    }
}
