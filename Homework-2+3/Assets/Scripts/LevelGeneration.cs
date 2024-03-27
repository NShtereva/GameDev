using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int minNumberOfPlatforms = 10;
    [SerializeField] private int maxNumberOfPlatforms = 20;
    
    public GameObject platform;
    public GameObject background;
    public GameObject finish;
    public GameObject key;
    public GameObject enemy;
    public GameObject jumpingPad;

    private int numberOfPlatforms;
    private float offset = 2.2f, bgOffset = 20f;

    // Start is called before the first frame update
    void Start()
    {
        numberOfPlatforms = Random.Range(minNumberOfPlatforms, maxNumberOfPlatforms);
        generateThePlatforms();
    }

    private void generateThePlatforms()
    {
        Vector3 prevPosition = platform.transform.position;

        for(int i = 0; i < numberOfPlatforms; i++)
        {
            Vector3 position = new Vector3(prevPosition.x, prevPosition.y + offset, prevPosition.z);
            GameObject currentPlatform = Instantiate(platform, position, Quaternion.identity);

            prevPosition = currentPlatform.transform.position;
            correctPosition(ref prevPosition);

            generateNewBackground(i);
        }

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        int size = platforms.Length;

        generateGameObject(ref platforms, ref size, 3, key);
        generateGameObject(ref platforms, ref size, 2, jumpingPad);
        //generateGameObject(ref platforms, ref size, 2, enemy);

        lastPlatformGeneration(prevPosition);
    }

    private void correctPosition(ref Vector3 position)
    {
        if(Mathf.Abs(position.x - position.x * -1) > 10)
        {
            position.x = position.x > 0 ? position.x - 3 : position.x + 3;
        } 
        else
        {
            position.x *= -1;
        }    

        position.y += offset;
    }

    private void generateNewBackground(int index)
    {
        if(index % 5 == 0)
        {
            Vector3 currPosition = background.transform.position;
            Vector3 newPosition = new Vector3(currPosition.x, currPosition.y + bgOffset, currPosition.z);

            GameObject currentBackground = Instantiate(background, newPosition, Quaternion.identity);

            bgOffset += 20f;
        }
    }

    private void generateGameObject(ref GameObject[] platforms, ref int size, int numberOfObjects, GameObject gameObjectToAdd)
    {
        int counter = 0,
            from = 1, to = size / numberOfObjects;

        while(counter != numberOfObjects)
        {
            int index = Random.Range(from, to);
            if(index >= 0 && index <= numberOfPlatforms)
            {
                Vector3 position = platforms[index].transform.position;

                Vector3 newPosition = new Vector3(position.x, position.y + 1, position.z);
                Instantiate(gameObjectToAdd, newPosition, Quaternion.identity);

                counter++;

                from = to + 1;
                to += to;

                (platforms[index], platforms[size - 1]) = (platforms[size - 1], platforms[index]);
                size--;
            }
        }
    }

    private void lastPlatformGeneration(Vector3 position)
    {
        Vector3 lastPosition = new Vector3(position.x, position.y + offset, position.z);
        GameObject lastPlatform = Instantiate(platform, lastPosition, Quaternion.identity);
        finish.transform.position = new Vector3(lastPosition.x, lastPosition.y + 1.45f, lastPosition.z);
    }
}
