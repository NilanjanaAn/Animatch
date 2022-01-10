using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad // save and load game data using binary format to prevent external alteration
{
    public static void SaveData()
    {
        Debug.Log("saving");
        BinaryFormatter bform = new BinaryFormatter();
        string path = Application.persistentDataPath + "/match.dat";
        FileStream file = new FileStream(path, FileMode.Create);
        LevelData data = new LevelData();
        bform.Serialize(file, data); // convert to binary and write in file
        file.Close();
    }

    public static LevelData LoadData()
    {
        
        string path = Application.persistentDataPath + "/match.dat";

        if(File.Exists(path))
        {
            Debug.Log("loading");
            BinaryFormatter bform = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            LevelData data = bform.Deserialize(file) as LevelData; // read and convert back from binary to LevelData type
            file.Close();
            return data;
        }
        else
        {
            Debug.Log("Not found " + path);
            return null;
        }
    }
}
