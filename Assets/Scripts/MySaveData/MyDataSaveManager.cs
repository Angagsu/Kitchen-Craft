using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using Newtonsoft.Json;

public class MyDataSaveManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    public static MyDataSaveManager Instance { get; private set; }

    private MyGameData gameData;
    private List<ISavable> savableObjects;
    private MyDataHandler dataHandler;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more then one DataPersistenceManager");
        }

        Instance = this;
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath + fileName);
        dataHandler = new MyDataHandler(Application.persistentDataPath, fileName, useEncryption);
        savableObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new MyGameData();
    }

    public void SaveGame()
    {
        // TODO - pass the data to other scripts so they can update it
        foreach (ISavable savableObj in savableObjects)
        {
            savableObj.SaveData(ref gameData);
        }

        // TODO - save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        // load any data from a file using the data handler
        this.gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // TODO - push the loaded data to all other scripts that need it
        foreach (ISavable savableObj in savableObjects)
        {
            savableObj.LoadData(gameData);
        }

    }

    public void Delete()
    {
        dataHandler.Delete();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISavable> FindAllDataPersistenceObjects()
    {
        IEnumerable<ISavable> savableObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<ISavable>();

        return new List<ISavable>(savableObjects);
    }
}
