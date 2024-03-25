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

    void generateThePlatforms()
    {
        Vector3 prevPosition = platform.transform.position;

        for(int i = 0; i < numberOfPlatforms; i++)
        {
            Vector3 position = new Vector3(prevPosition.x, prevPosition.y + offset, prevPosition.z);
            GameObject currentPlatform = Instantiate(platform, position, Quaternion.identity);

            if(i == firstKeyIndex || i == secondKeyIndex || i == thirdKeyIndex)
            {
                Vector3 keyPosition = new Vector3(position.x, position.y + 1, position.z);
                Instantiate(key, keyPosition, Quaternion.identity);
            }

            prevPosition = currentPlatform.transform.position;

            if(Mathf.Abs(prevPosition.x - prevPosition.x * -1) > 10)
            {
                prevPosition.x = prevPosition.x > 0 ? prevPosition.x - 3 : prevPosition.x + 3;
            } 
            else
            {
                prevPosition.x *= -1;
            }    

            prevPosition.y += offset;

            if(i % 6 == 0)
            {
                Vector3 currPosition = background.transform.position;
                Vector3 newPosition = new Vector3(currPosition.x, currPosition.y + bgOffset, currPosition.z);

                GameObject currentBackground = Instantiate(background, newPosition, Quaternion.identity);

                bgOffset += 20f;
            }
        }

        Vector3 lastPosition = new Vector3(prevPosition.x, prevPosition.y + offset, prevPosition.z);
        GameObject lastPlatform = Instantiate(platform, lastPosition, Quaternion.identity);
        finish.transform.position = new Vector3(lastPosition.x, lastPosition.y + 1.45f, lastPosition.z);
    }
}
