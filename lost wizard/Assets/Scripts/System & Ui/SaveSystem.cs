using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(PlayerMovement player, string scene) {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.lwiz";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player,scene);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData loadPlayer()
    {

        string path = Application.persistentDataPath + "/save.lwiz";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;

        } else {
            Debug.LogError("save flie not found in " + path);
            return null;
        }


    }
    public static bool checkData() {

        string path = Application.persistentDataPath + "/save.lwiz";
        return File.Exists(path);
    
    }

    public static void borrarFile()
    {

        string path = Application.persistentDataPath + "/save.lwiz";
        File.Delete(path);
    }
}
