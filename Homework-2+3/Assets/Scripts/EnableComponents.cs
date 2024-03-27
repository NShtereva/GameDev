using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponents : MonoBehaviour
{
    [SerializeField] GameObject heart2;

    // Update is called once per frame
    void Update()
    {
        if(!heart2.activeSelf)
        {
            gameObject.GetComponent<VignetteEffect>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<VignetteEffect>().enabled = false;
        }
    }
}
