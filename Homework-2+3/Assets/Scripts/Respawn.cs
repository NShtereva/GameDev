using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject heart3;
    [SerializeField] GameObject key1;
    [SerializeField] GameObject key2;
    [SerializeField] GameObject key3;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!heart1.activeSelf)
        {
            rb2d.transform.position = start.transform.position;

            key1.SetActive(false);
            key2.SetActive(false);
            key3.SetActive(false);

            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
    }
}
