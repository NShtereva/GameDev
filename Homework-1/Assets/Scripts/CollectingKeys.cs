using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingKeys : MonoBehaviour
{
    byte count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Key")
        {
            count++;
            collider2D.gameObject.SetActive(false);
            print("number of keys: " + count);
        }
    }
}
