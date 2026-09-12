using System.IO;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public String bestPlayerName;
    public String playerName;
    public bool isGameActive;
    public float bestTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    [System.Serializable]
    public class Data
    {
        public String bestPlayer;
        public float bestTime;
    }

    public void SaveData()
    {
        Data data = new Data();
        data.bestPlayer = bestPlayerName;
        data.bestTime = bestTime;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);

            bestPlayerName = data.bestPlayer;
            bestTime = data.bestTime;
        }
    }


}
