using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SaveDataManager : MonoSingleton<SaveDataManager>
{
  
    //public void SaveIntoJson()
    //{
    //    string userInventoryString = JsonUtility.ToJson(_inventoryData, true);
    //    System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", userInventoryString);
    //}

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadAllDataOnStart();
    }

    public void LoadAllDataOnStart()
    {
        GameManager.Instance.GetUserInventory().Load();
    }

    public void SaveAllData()
    {
        GameManager.Instance.GetUserInventory().Save();
    }

    public void WriteToFile(string filename, string json)
    {
        //todo - check first if it exists
        string path = GetFilePath(filename);
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        using (StreamWriter writer = new StreamWriter(file))
        {
            writer.Write(json);
        } ;
    }

    public string ReadFromFile(string filename)
    {
        string path = GetFilePath(filename);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            };
        }
        else
            Debug.Log("File not found: " + filename);
        return "";
       
    }

    private string GetFilePath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    //private void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //        SaveAllData();
    //}
}
