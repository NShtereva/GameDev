using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winState;
    [SerializeField] private GameObject lossState;
    [SerializeField] private GameObject[] numbers;

    const ushort SIZE = 4;
    const ushort WIN_VALUE = 2048;

    uint[] values;

    bool win, loss;

    // Start is called before the first frame update
    void Start()
    {
        values = new uint[SIZE * SIZE];

        win = false;
        loss = false;
    }

    // Update is called once per frame
    void Update()
    {
        ConvertToNumbers();

        CheckForWin();
        if(win)
        {
            winState.SetActive(true);
        }

        CheckForLoss();
        if(loss)
        {
            winState.SetActive(false);
            lossState.SetActive(true);
        }

        winState.SetActive(false);
    }

    void CheckForWin()
    {
        for(ushort i = 0; i < values.Length && !win; i++)
        {
            if(values[i] == WIN_VALUE)
            {
                win = true;
            }
        }
    }

    void CheckForLoss()
    {
        ushort count = 0;
        bool sameValues = false;

        for(ushort i = 0; i < SIZE && !sameValues; i++)
        {
            for(ushort j = 0; j < SIZE && !sameValues; j++)
            {
                if(values[i * SIZE + j] != 0)
                {
                    count++;
                }

                if((j - 1 >= 0 && values[i * SIZE + j] == values[i * SIZE + (j-1)])   ||    // values[i][j] == values[i][j-1]
                   (i - 1 >= 0 && values[i * SIZE + j] == values[(i-1) * SIZE + j])   ||    // values[i][j] == values[i-1][j]
                   (j + 1 < SIZE && values[i * SIZE + j] == values[i * SIZE + (j+1)]) ||    // values[i][j] == values[i][j+1]
                   (i + 1 < SIZE && values[i * SIZE + j] == values[(i+1) * SIZE + j]))      // values[i][j] == values[i+1][j]
                {
                    sameValues = true;
                }
            }
        }

        if(!sameValues && count == values.Length)
        {
            loss = true;
        }
    }

    void ConvertToNumbers()
    {
        for(uint i = 0; i < numbers.Length; i++)
        {
            if(numbers[i].GetComponent<TMPro.TextMeshProUGUI>().text == "")
            {
                values[i] = 0;
            }
            else
            {
                values[i] = uint.Parse(numbers[i].GetComponent<TMPro.TextMeshProUGUI>().text);
            }
        }
    }
}
