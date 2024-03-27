using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Jump : MonoBehaviour
{
    bool isJumping = false;
    bool isOnGround = false;

    private AudioSource jumpSound;
    
    public Animator animator;

    void Start()
    {
        jumpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isJumping)
        {
            isJumping = Input.GetButtonDown("Jump") && isOnGround;
        }
        
        animator.SetBool("IsJumping", isJumping);
    }

    void FixedUpdate() 
    {
        transform.eulerAngles = new Vector3(0, 0, 0);

        if(isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            jumpSound.Play();
            isJumping = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collider2D)
    {
        Vector2 boxPosition = transform.position;
        RaycastHit2D[] raycastHits2D = Physics2D.BoxCastAll(boxPosition, new Vector2(1, 1), 0, new Vector2(0, 0));

        isOnGround = false;
        foreach(var item in raycastHits2D)
        {
            if(item.collider.gameObject.name != "Player")
            {
                isOnGround = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collider2D)
    {
        isOnGround = false;
    }
}
