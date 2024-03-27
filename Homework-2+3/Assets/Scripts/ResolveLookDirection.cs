using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class ResolveLookDirection : MonoBehaviour
{
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb2d)
        {
            return;
        }

        float horizontalVelocity = rb2d.velocity.x;
        
        if (Abs(horizontalVelocity) > 0.01f)
        {
            float lookDirection = (horizontalVelocity > 0) ? 1 : -1;
            transform.localScale = new Vector3(lookDirection, 1, 1);
        }
    }
}
