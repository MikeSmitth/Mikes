using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPresistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private FileDataHandler dataHandler;

    private GameData gameData;
    //private GameData;


    //bêdziemy sprawdzali czy poprawnie jest zalinkowane IDataPresestence
    private List<IDataPresistence> dataPresistenceObjects;
    public DataPresistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one DataPresistenceManager");
        }
        instance = this;
    }
    private void Start()
    {
        //Application.persistentDataPath oddaje standardowe miejsce na magazywnowanie danych w unity
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPresistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
        dataHandler.Save(gameData);
    }   
    
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("No dataa was found. Initializing data to defailts.");
            NewGame();
        }


        foreach (IDataPresistence dataPersistenceObj in dataPresistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        //Debug.Log("Loaded death count = " + gameData.inGameTimeToSave);
    }

    public void SaveGame()
    {
        foreach (IDataPresistence dataPersistenceObj in dataPresistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }


        //Debug.Log("Saved death count = " + gameData.inGameTimeToSave);
        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
       // SaveGame();
    }

    //metoda sprawdzaj¹ca pod³aczenie  IDataPresestence
    private List<IDataPresistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPresistence> dataPresistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPresistence>();

        return new List<IDataPresistence>(dataPresistenceObjects);
    }
}
