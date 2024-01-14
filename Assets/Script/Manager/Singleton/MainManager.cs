using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public string playerName; // player Name Data
    public int m_Points; // Point Data
    public int scenePoints;

    public Vector3 playerPosition; // Player Posisiton Data

    public int sceneIndex; // Scene Index Data
    public int selectedCharacter; //character

    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        SaveData data = new SaveData(); //use Save Data Class
        data.namePlayer = playerName; // save name
        data.position = playerPosition; // save position
        data.point = m_Points; // save point
        data.character = selectedCharacter; //save character
        data.ScenePoint = scenePoints;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            data.savedScene = SceneManager.GetActiveScene().buildIndex + 2;
        }
        else
        {
            data.savedScene = SceneManager.GetActiveScene().buildIndex;
        }

        string json = JsonUtility.ToJson(data);

        string path = GetSaveFilePath();
        File.WriteAllText(path, json);
    }

    //Load Function
    public void LoadData()
    {
        string path = GetSaveFilePath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.namePlayer;
            playerPosition = data.position;
            m_Points = data.point;
            sceneIndex = data.savedScene;
            selectedCharacter = data.character;
            scenePoints = data.ScenePoint;
        }
    }

    private string GetSaveFilePath()
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Get Document path
        string gameFolder = Path.Combine(documentsPath, "NoobMaster69"); //add path to NoobMaster69 Folder

        // Create the game folder if it doesn't exist
        if (!Directory.Exists(gameFolder))
        {
            Directory.CreateDirectory(gameFolder);
        }

        // Append the file name to the game folder path
        return Path.Combine(gameFolder, "Savefile.json");
    }
}

[Serializable]
public class SaveData
{
    public string namePlayer;
    public Vector3 position;
    public int point;
    public int savedScene;
    public int character;
    public int ScenePoint;

    // add the variable that want to save
    //ex:
    // public string Playername //to save playername
}
