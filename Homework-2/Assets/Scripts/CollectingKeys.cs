using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectingKeys : MonoBehaviour
{
    [SerializeField] GameObject key1;
    [SerializeField] GameObject key2;
    [SerializeField] GameObject key3;
    [SerializeField] GameObject winState;

    byte count;
    GameObject finish;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        finish = GameObject.Find("DoorClosed");
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
                case 3: { key3.SetActive(true); finish.SetActive(false); } break;
            }
        }

        if(collider2D.gameObject.tag == "Finish" && count == 3)
        {
            winState.SetActive(true);
            SceneManager.LoadScene(0);
        }
    }
}
