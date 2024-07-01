using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] board;
    [SerializeField] private GameObject[] numbers;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject bestScore;

    uint[] values;

    uint sum;

    const ushort SIZE = 4;

    // Start is called before the first frame update
    void Start()
    {
        values = new uint[SIZE * SIZE];

        var rnd = new System.Random();
        values[rnd.Next(SIZE) * SIZE + rnd.Next(SIZE)] = 2;
        values[rnd.Next(SIZE) * SIZE + rnd.Next(SIZE)] = 2;

        sum = 0;

        //ChangeColors();
        ConvertToString();      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            ConvertToNumbers();

            MoveUp();
            AddNewNode();

            //ChangeColors();
            ConvertToString();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            ConvertToNumbers();

            MoveDown();
            AddNewNode();

            //ChangeColors();
            ConvertToString();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ConvertToNumbers();

            MoveLeft();
            AddNewNode();

            //ChangeColors();
            ConvertToString();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ConvertToNumbers();

            MoveRight();
            AddNewNode();

            //ChangeColors();
            ConvertToString();
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

    void ConvertToString()
    {
        for(uint i = 0; i < numbers.Length; i++)
        {
            if(values[i] == 0)
            {
                numbers[i].GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
            else
            {
                numbers[i].GetComponent<TMPro.TextMeshProUGUI>().text = values[i].ToString();
            }
        }

        score.GetComponent<TMPro.TextMeshProUGUI>().text = sum.ToString();
    
        if(sum > uint.Parse(bestScore.GetComponent<TMPro.TextMeshProUGUI>().text))
        {
            bestScore.GetComponent<TMPro.TextMeshProUGUI>().text = score.GetComponent<TMPro.TextMeshProUGUI>().text;
        }
    }

    // [i][j] -> [k] : k = i * SIZE + j

    void MoveUp()
    {
        for(ushort j = 0; j < SIZE; j++)
        {
            // remove zeros
            ushort write = 0;
            for(ushort read = 0; read < SIZE; read++)
            {
                if(values[read * SIZE + j] != 0)
                {
                    values[write * SIZE + j] = values[read * SIZE + j];
                    write++;
                }
            }

            while(write < SIZE)
            {
                values[write * SIZE + j] = 0;
                write++;
            }

            // sum the same values
            for(int i = SIZE - 1; i > 0; i--)
            {
                if(values[(i-1) * SIZE + j] == values[i * SIZE + j] && values[(i-1) * SIZE + j] != 0)
                {
                    values[(i-1) * SIZE + j] *= 2;
                    values[i * SIZE + j] = 0;

                    sum += values[(i-1) * SIZE + j];

                    i--;
                }
            }

            // remove zeros
            write = 0;
            for(ushort read = 0; read < SIZE; read++)
            {
                if(values[read * SIZE + j] != 0)
                {
                    values[write * SIZE + j] = values[read * SIZE + j];
                    write++;
                }
            }

            while(write < SIZE)
            {
                values[write * SIZE + j] = 0;
                write++;
            }
        }
    }

    void MoveDown()
    {
        for(ushort j = 0; j < SIZE; j++)
        {
            // remove zeros
            int write = SIZE - 1;
            for(int read = SIZE - 1; read >= 0; read--)
            {
                if(values[read * SIZE + j] != 0)
                {
                    values[write * SIZE + j] = values[read * SIZE + j];
                    write--;
                }
            }

            while(write >= 0)
            {
                values[write * SIZE + j] = 0;
                write--;
            }

            // sum the same values
            for(ushort i = 0; i < SIZE - 1; i++)
            {
                if(values[(i+1) * SIZE + j] == values[i * SIZE + j] && values[i * SIZE + j] != 0)
                {
                    values[(i+1) * SIZE + j] *= 2;
                    values[i * SIZE + j] = 0;

                    sum += values[(i+1) * SIZE + j];

                    i++;
                }
            }

            // remove zeros
            write = SIZE - 1;
            for(int read = SIZE - 1; read >= 0; read--)
            {
                if(values[read * SIZE + j] != 0)
                {
                    values[write * SIZE + j] = values[read * SIZE + j];
                    write--;
                }
            }

            while(write >= 0)
            {
                values[write * SIZE + j] = 0;
                write--;
            }
        }
    }

    void MoveLeft()
    {
        for(ushort i = 0; i < SIZE; i++)
        {
            // remove zeros
            ushort write = 0;
            for(ushort read = 0; read < SIZE; read++)
            {
                if(values[i * SIZE + read] != 0)
                {
                    values[i * SIZE + write] = values[i * SIZE + read];
                    write++;
                }
            }

            while(write < SIZE)
            {
                values[i * SIZE + write] = 0;
                write++;
            }

            // sum the same values
            for(int j = SIZE - 1; j > 0; j--)
            {
                if(values[i * SIZE + (j-1)] == values[i * SIZE + j] && values[i * SIZE + j] != 0)
                {
                    values[i * SIZE + (j-1)] *= 2;
                    values[i * SIZE + j] = 0;

                    sum += values[i * SIZE + (j-1)];

                    j--;
                }
            }

            // remove zeros
            write = 0;
            for(ushort read = 0; read < SIZE; read++)
            {
                if(values[i * SIZE + read] != 0)
                {
                    values[i * SIZE + write] = values[i * SIZE + read];
                    write++;
                }
            }

            while(write < SIZE)
            {
                values[i * SIZE + write] = 0;
                write++;
            }
        }
    }

    void MoveRight()
    {
        for(ushort i = 0; i < SIZE; i++)
        {
            // remove zeros
            int write = SIZE - 1;
            for(int read = SIZE - 1; read >= 0; read--)
            {
                if(values[i * SIZE + read] != 0)
                {
                    values[i * SIZE + write] = values[i * SIZE + read];
                    write--;
                }
            }

            while(write >= 0)
            {
                values[i * SIZE + write] = 0;
                write--;
            }

            // sum the same values
            for(ushort j = 0; j < SIZE - 1; j++)
            {
                if(values[i * SIZE + (j+1)] == values[i * SIZE + j] && values[i * SIZE + j] != 0)
                {
                    values[i * SIZE + (j+1)] *= 2;
                    values[i * SIZE + j] = 0;

                    sum += values[i * SIZE + (j+1)];

                    j++;
                }
            }

            // remove zeros
            write = SIZE - 1;
            for(int read = SIZE - 1; read >= 0; read--)
            {
                if(values[i * SIZE + read] != 0)
                {
                    values[i * SIZE + write] = values[i * SIZE + read];
                    write--;
                }
            }

            while(write >= 0)
            {
                values[i * SIZE + write] = 0;
                write--;
            }
        }
    }

    void AddNewNode()
    {
        const ushort MAX_ITER = 100;
        
        var rnd = new System.Random();
        ushort row = (ushort)rnd.Next(SIZE), col = (ushort)rnd.Next(SIZE), count = 0;

        while(count < MAX_ITER && values[row * SIZE + col] != 0)
        {
            row = (ushort)rnd.Next(SIZE);
            col = (ushort)rnd.Next(SIZE);
            count++;
        }

        if(count != MAX_ITER)
        {
            if(rnd.Next(MAX_ITER) % 2 == 0)
            {
                values[row * SIZE + col] = 2;
            }
            else
            {
                values[row * SIZE + col] = 4;
            }
        }
    }

    void ChangeColors()
    {
        for(ushort i = 0; i < board.Length; i++)
        {
            switch(values[i])
            {
                case    0: board[i].GetComponent<Renderer>().material.color = new Color(197, 175, 175); break;
                case    2: board[i].GetComponent<Renderer>().material.color = new Color(238, 228, 218); break;
                case    4: board[i].GetComponent<Renderer>().material.color = new Color(237, 224, 200); break;
                case    8: board[i].GetComponent<Renderer>().material.color = new Color(242, 177, 121); break;
                case   16: board[i].GetComponent<Renderer>().material.color = new Color(245, 149, 99); break;
                case   32: board[i].GetComponent<Renderer>().material.color = new Color(246, 124, 95); break;
                case   64: board[i].GetComponent<Renderer>().material.color = new Color(246,  94, 59); break;
                case  128: board[i].GetComponent<Renderer>().material.color = new Color(237, 207, 114); break;
                case  256: board[i].GetComponent<Renderer>().material.color = new Color(237, 204, 97); break;
                case  512: board[i].GetComponent<Renderer>().material.color = new Color(237, 200, 80); break;
                case 1024: board[i].GetComponent<Renderer>().material.color = new Color(237, 197, 63); break;
                case 2048: board[i].GetComponent<Renderer>().material.color = new Color(237, 194, 46); break;

                default: board[i].GetComponent<Renderer>().material.color = new Color(0, 0, 0); break;
            }

            if(values[i] == 2 || values[i] == 4)
            {
                numbers[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(128, 96, 96);
            }
            else
            {
                numbers[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
            }
        }
    }
}
