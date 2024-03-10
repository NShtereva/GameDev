using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingKeys : MonoBehaviour
{
    [SerializeField] GameObject key1;
    [SerializeField] GameObject key2;
    [SerializeField] GameObject key3;

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

            switch(count)
            {
                case 1: key1.SetActive(true); break;
                case 2: key2.SetActive(true); break;
                case 3: key3.SetActive(true); break;
            }
        }
    }
}
