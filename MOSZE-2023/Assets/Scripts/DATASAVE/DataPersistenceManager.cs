using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake() 
    {
        if (instance != null) 
        {
            Debug.LogError("Nem található fájl, alapértékre inicializálás.");
        }
        instance = this;
    }

    private void Start() 
    {
        // Itt történik meg az objectek inicializálása
        this.dataHandler = new FileDataHandler(Application.dataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        Invoke ("LoadGame", 1f);
    }

    public void NewGame() 
    {   
        //Új játéknál létrehoz egy új GameData objectet.
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data handler Itt töltődnek be a fájlből az adatok
        this.gameData = dataHandler.Load();
        
        // Ha nincs adat, amit be lehetne tölteni, akkor alapértelmezettre állítja
        if (this.gameData == null) 
        {
            Debug.Log("Nincs adat");
            NewGame();
        }

        // Ez a ciklus végigmegy az objecteken és betölti az adatokat a többi scripthez
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scripts so they can update it Ez a ciklus végigmegy az objecteken és az adatokat elmenti a többi scripthez.
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.SaveData(ref gameData);
            Debug.Log(gameData.RoomDataList.Count);
        }

        //Itt elmenti a fájlba az adott data-t.
        dataHandler.Save(gameData);
    }

    // Itt csekkeljük, hogy mely objectek MonoBehaviorok és IDataPersistencek. Majd ezeket eltároljuk egy listában
    private List<IDataPersistence> FindAllDataPersistenceObjects() 
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}