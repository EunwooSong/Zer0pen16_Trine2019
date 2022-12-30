using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    //Player
    private static string MonthCtrl()
    {
        string month = "";

        switch (DateTime.Now.ToString("MM"))
        {
            case "01": month = " 1월/JAN "; break;
            case "02": month = " 2월/FAB "; break;
            case "03": month = " 3월/MAR "; break;
            case "04": month = " 4월/APR "; break;
            case "05": month = " 5월/MAY "; break;
            case "06": month = " 6월/JUN "; break;
            case "07": month = " 7월/JUL "; break;
            case "08": month = " 8월/AUG "; break;
            case "09": month = " 9월/SEP "; break;
            case "10": month = " 10월/OCT "; break;
            case "11": month = " 11월/NOV "; break;
            case "12": month = " 12월/DEC "; break;
        }

        return month;
    }

    public static void Save(PlayerCtrl player, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        PlayerData hisData = PlayerDataLoad(fileName);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        

        data.dateOfLssued = hisData.dateOfLssued;

        data.dateOfLast = "" + DateTime.Now.ToString("dd");
        data.dateOfLast += MonthCtrl();
        data.dateOfLast += DateTime.Now.ToString("yyyy");

        data.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        //Save
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void CreateProfile(PlayerCtrl player, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        data.dateOfLssued = "" + DateTime.Now.ToString("dd");
        data.dateOfLssued += MonthCtrl();
        data.dateOfLssued += DateTime.Now.ToString("yyyy");

        data.dateOfLast = "" + DateTime.Now.ToString("dd");
        data.dateOfLast += MonthCtrl();
        data.dateOfLast += DateTime.Now.ToString("yyyy");

        data.sceneName = "Stage_1";

        Debug.Log("Creat Profile Succeeded! - " + data.dateOfLssued);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Setting Data
    public static void Save(MainSceneManager msm, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingData data = new SettingData(msm);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Map Lock Data
    public static void Save(MapLockData mlData, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        MapLockData data = new MapLockData(mlData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //NPC Data 
    public static void Save(NPCData npcData, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        NPCData data = new NPCData(npcData);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Save NPC Data Done!");
    }

    public static PlayerData PlayerDataLoad(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
        
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
        
            return data;
        }

        else
        {
            Debug.LogError("Player Save file not found! - " + path);
            return null;
        }
    }

    public static SettingData SettingDataLoad(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingData data = formatter.Deserialize(stream) as SettingData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Setting Save file not found! - " + path);
            return null;
        }
    }

    public static MapLockData MapLockDataLoad(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapLockData data = formatter.Deserialize(stream) as MapLockData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Map Lock Data file not found! - " + path);
            return null;
        }
    }

    public static NPCData NPCDataLoad(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            NPCData data = formatter.Deserialize(stream) as NPCData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("NPC Data file not found! - " + path);
            return null;
        }
    }
}
