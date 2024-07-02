using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveBoard(GameObject _score, GameObject _bestScore, GameObject[] _numbers)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "board.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        BoardData data = new BoardData(_score, _bestScore, _numbers);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static BoardData LoadBoard()
    {
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "board.dat";
        if(!File.Exists(path))
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        BoardData data = formatter.Deserialize(stream) as BoardData;
        stream.Close();

        return data;
    }
}
