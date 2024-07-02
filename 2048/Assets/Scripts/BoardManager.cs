using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

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
        sum = 0;

        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "board.dat";
        if(File.Exists(path))
        {
            LoadBoard();
            ConvertToNumbers();
        } 
        else
        {
            var rnd = new System.Random();
            values[rnd.Next(SIZE) * SIZE + rnd.Next(SIZE)] = (rnd.Next(SIZE) % 2 == 0 ? 2u : 4u);
            values[rnd.Next(SIZE) * SIZE + rnd.Next(SIZE)] = (rnd.Next(SIZE) % 2 == 0 ? 2u : 4u);
        }        

        ChangeColors();
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

            ChangeColors();
            ConvertToString();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            ConvertToNumbers();

            MoveDown();
            AddNewNode();

            ChangeColors();
            ConvertToString();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ConvertToNumbers();

            MoveLeft();
            AddNewNode();

            ChangeColors();
            ConvertToString();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ConvertToNumbers();

            MoveRight();
            AddNewNode();

            ChangeColors();
            ConvertToString();
        }
    }

    public void LoadBoard()
    {
        BoardData data = SaveSystem.LoadBoard();
        if(data == null)
        {
            return;
        }

        score.GetComponent<TMPro.TextMeshProUGUI>().text = data.score;
        bestScore.GetComponent<TMPro.TextMeshProUGUI>().text = data.bestScore;

        for(ushort i = 0; i < numbers.Length; i++)
        {
            numbers[i].GetComponent<TMPro.TextMeshProUGUI>().text = data.numbers[i];
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

        sum = uint.Parse(score.GetComponent<TMPro.TextMeshProUGUI>().text);
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
            values[row * SIZE + col] = (rnd.Next(MAX_ITER) % 2 == 0 ? 2u : 4u);
        }
    }

    void ChangeColors()
    {
        for(ushort i = 0; i < board.Length; i++)
        {
            switch(values[i])
            {
                case    0: board[i].GetComponent<Renderer>().material.color = new Color(0.773f, 0.686f, 0.686f, 0.1f); break;
                case    2: board[i].GetComponent<Renderer>().material.color = new Color(0.933f, 0.894f, 0.855f, 0.5f); break;
                case    4: board[i].GetComponent<Renderer>().material.color = new Color(0.929f, 0.878f, 0.784f, 0.5f); break;
                case    8: board[i].GetComponent<Renderer>().material.color = new Color(0.949f, 0.694f, 0.475f, 0.5f); break;
                case   16: board[i].GetComponent<Renderer>().material.color = new Color(0.961f, 0.584f, 0.388f, 0.5f); break;
                case   32: board[i].GetComponent<Renderer>().material.color = new Color(0.965f, 0.486f, 0.373f, 0.5f); break;
                case   64: board[i].GetComponent<Renderer>().material.color = new Color(0.965f, 0.369f, 0.231f, 0.5f); break;
                case  128: board[i].GetComponent<Renderer>().material.color = new Color(0.929f, 0.812f, 0.447f, 0.5f); break;
                case  256: board[i].GetComponent<Renderer>().material.color = new Color(0.929f, 0.800f, 0.380f, 0.5f); break;
                case  512: board[i].GetComponent<Renderer>().material.color = new Color(0.929f, 0.784f, 0.314f, 0.5f); break;
                case 1024: board[i].GetComponent<Renderer>().material.color = new Color(0.929f, 0.773f, 0.247f, 0.5f); break;
                case 2048: board[i].GetComponent<Renderer>().material.color = new Color(0.929f, 0.761f, 0.180f, 0.5f); break;

                default: board[i].GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 1f); break;
            }

            numbers[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}
