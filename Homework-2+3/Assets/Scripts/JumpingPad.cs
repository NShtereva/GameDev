using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            collider2D.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        }
    }
}
