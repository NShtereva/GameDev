using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomElements : MonoBehaviour
{
    [SerializeField] private GameObject[] points;

    // Start is called before the first frame update
    void Start()
    {
        int size = points.Length;
        for(int i = 0; i < size; i++)
        {
            SelectRandomElementsForObject(points[i]);
        }
    }

    void SelectRandomElementsForObject(GameObject point)
    {
        int childRange = point.transform.childCount;
        int childId = Random.Range(0, childRange);
        point.transform.GetChild(childId).gameObject.SetActive(true);
    }
}
