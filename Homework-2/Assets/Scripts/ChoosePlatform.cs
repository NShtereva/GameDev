using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0, 100);
        if(num % 10 == 0)
        {
            platforms[2].SetActive(true);
        }
        else if(num % 2 == 0)
        {
            platforms[1].SetActive(true);
        }
        else
        {
            platforms[0].SetActive(true);
        }
    }
}
