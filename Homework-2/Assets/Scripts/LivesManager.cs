using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject heart3;
    [SerializeField] GameObject loseState;

    public Animator animator;

    Rigidbody2D rb2d;
    byte count;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
    }

    void Update()
    {
        animator.SetBool("IsAttacked", false);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "OutOfMap" || collider2D.gameObject.tag == "Enemy")
        {
            count++;

            switch(count)
            {
                case 1: heart3.SetActive(false); break;
                case 2: heart2.SetActive(false); break;
                case 3: heart1.SetActive(false); break;
            }

            animator.SetBool("IsAttacked", true);
        }

        if(count == 3)
        {
            loseState.SetActive(true);
            SceneManager.LoadScene(0);
        }

        if(collider2D.gameObject.tag == "OutOfMap")
        {
            rb2d.transform.position = start.transform.position;
        }
    }
}
