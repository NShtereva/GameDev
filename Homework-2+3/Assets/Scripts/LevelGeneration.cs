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

    private int numberOfPlatforms;
    private float offset = 2.2f, bgOffset = 20f;
    private int firstKeyIndex, secondKeyIndex, thirdKeyIndex;

    // Start is called before the first frame update
    void Start()
    {
        numberOfPlatforms = Random.Range(minNumberOfPlatforms, maxNumberOfPlatforms);

        int temp = numberOfPlatforms / 3;
        firstKeyIndex = Random.Range(0, temp);
        secondKeyIndex = Random.Range(temp + 1, 2 * temp);
        thirdKeyIndex = Random.Range(2 * temp + 1, numberOfPlatforms);

        generateThePlatforms();
    }

    private void generateThePlatforms()
    {
        Vector3 prevPosition = platform.transform.position;

        for(int i = 0; i < numberOfPlatforms; i++)
        {
            Vector3 position = new Vector3(prevPosition.x, prevPosition.y + offset, prevPosition.z);
            GameObject currentPlatform = Instantiate(platform, position, Quaternion.identity);

            generateNewKey(i, position);

            prevPosition = currentPlatform.transform.position;
            correctPosition(ref prevPosition);

            generateNewBackground(i);
        }

        lastPlatformGeneration(prevPosition);
    }

    private void generateNewKey(int index, Vector3 position)
    {
        if(index == firstKeyIndex || index == secondKeyIndex || index == thirdKeyIndex)
        {
            Vector3 keyPosition = new Vector3(position.x, position.y + 1, position.z);
            Instantiate(key, keyPosition, Quaternion.identity);
        }
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
        if(index % 6 == 0)
        {
            Vector3 currPosition = background.transform.position;
            Vector3 newPosition = new Vector3(currPosition.x, currPosition.y + bgOffset, currPosition.z);

            GameObject currentBackground = Instantiate(background, newPosition, Quaternion.identity);

            bgOffset += 20f;
        }
    }

    private void lastPlatformGeneration(Vector3 position)
    {
        Vector3 lastPosition = new Vector3(position.x, position.y + offset, position.z);
        GameObject lastPlatform = Instantiate(platform, lastPosition, Quaternion.identity);
        finish.transform.position = new Vector3(lastPosition.x, lastPosition.y + 1.45f, lastPosition.z);
    }
}
