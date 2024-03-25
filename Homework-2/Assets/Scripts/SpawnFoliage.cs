using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFoliage : MonoBehaviour
{
    [SerializeField] private GameObject spawnPointLeft;
    [SerializeField] private GameObject spawnPointRight;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFoliageForObject(spawnPointLeft);
        SpawnFoliageForObject(spawnPointRight);
    }

    void SpawnFoliageForObject(GameObject spawnPoint)
    {
        int childRange = spawnPoint.transform.childCount;

        int childId = Random.Range(0, childRange);
        spawnPoint.transform.GetChild(childId).gameObject.SetActive(true);
    }
}
