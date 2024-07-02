using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardData
{
    public string score, bestScore;
    public string[] numbers;

    public BoardData(GameObject _score, GameObject _bestScore, GameObject[] _numbers)
    {
        score = _score.GetComponent<TMPro.TextMeshProUGUI>().text;
        bestScore = _bestScore.GetComponent<TMPro.TextMeshProUGUI>().text;

        numbers = new string[_numbers.Length];
        for(ushort i = 0; i < _numbers.Length; i++)
        {
            numbers[i] = _numbers[i].GetComponent<TMPro.TextMeshProUGUI>().text;
        }
    }
}
