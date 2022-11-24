using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        //Urzywamy Path.Combine poniewa¿ ró¿ne OS'y maj¹ ró¿ne separatory œcie¿ek "/"
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //³adowanko serialized danych z pliku 
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath,FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); 
                    }
                }
                //deserializowanko danych z Json pliku do danych c#
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load from data to file" + fullPath + "|n" + e);
            }
        }
        return loadedData;

    }
    public void Save(GameData data)
    {
        //Urzywamy Path.Combine poniewa¿ ró¿ne OS'y maj¹ ró¿ne separatory œcie¿ek "/"
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //utwórzy katalog, w którym plik zostanie zapisany, jeœli jeszcze nie istnieje
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serializowanie obiektów danych gry C#
            string dataToStore = JsonUtility.ToJson(data, true);


            //zapisanei serializowanych danych do pliku
            using(FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file" + fullPath + "|n" + e);
        }
    }
}
