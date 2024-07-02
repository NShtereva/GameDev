using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataRecordManager : MonoBehaviour
{
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject bestScore;
    [SerializeField] private GameObject[] numbers;

    const ushort SIZE = 4;

    // Update is called once per frame
    void Update()
    {
        SaveBoard();
    }

    public void SaveBoard()
    {
        SaveSystem.SaveBoard(score, bestScore, numbers);
    }

    void Clear()
    {
        score.GetComponent<TMPro.TextMeshProUGUI>().text = "0";

        for(uint i = 0; i < numbers.Length; i++)
        {
            numbers[i].GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }

        var rnd = new System.Random();
        numbers[rnd.Next(SIZE) * SIZE + rnd.Next(SIZE)].GetComponent<TMPro.TextMeshProUGUI>().text = (rnd.Next(SIZE) % 2 == 0 ? 2u : 4u).ToString();
        numbers[rnd.Next(SIZE) * SIZE + rnd.Next(SIZE)].GetComponent<TMPro.TextMeshProUGUI>().text = (rnd.Next(SIZE) % 2 == 0 ? 2u : 4u).ToString();
    }

    public void Restart()
    {
        Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
