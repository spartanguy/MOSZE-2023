using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "moszejatek";

    // FileDataHandler konstruktor
    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption) 
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load() 
    {
        // A Path.Combine-al minden operációs rendszer file pathjét be lehet kérni, a fullPath ezt tárolja el
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath)) //Csekkeljuk, hogy a fullpathen található-e fájl.
        {
            try 
            {
                //Ez tölti be a szerializált adatokat a fájlból
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Ez az if ág fogja titkosítani/visszafordítani az adatokat
                if (useEncryption) 
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //Itt történik meg az adatok deszérializálása.
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            //Ha hibába ütközik a rendszer, akkor kidobja az alábbi errort
            catch (Exception e) 
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data) 
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try 
        {
            //Ha nem létezik Directory, akkor létrehoz egyet, amibe a fájlt majd belehelyezzük
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // JSON-ba serializáljuk a GameData objectet adatait.
            string dataToStore = JsonUtility.ToJson(data, true);

            // Itt lesz végrehajtva a fájl titkosítása
            if (useEncryption) 
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // A szerializált adatokat itt írjuk ki a fájlba
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Error a fájl mentésekor: " + fullPath + "\n" + e);
        }
    }

    // Ez a függvény hajtja végre a fájl titkosítását.
    private string EncryptDecrypt(string data) 
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++) 
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
    //Ez a függvény fogja visszaadni a mentésfájl teljes elérhetőségi vonalát.
    public string GetFullPath(){
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log(fullPath);
        return fullPath;
    }
}