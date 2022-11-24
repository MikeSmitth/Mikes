using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ObservationStorage : MonoBehaviour, IDataPresistence
{

    //private Camera cam;
    [Header("Observations List")]
    [SerializeField] string[] observationList;
    [SerializeField] int observationsSize=20;
    DialogueMenager dm;


    //Tworzymy s³ownik z dowodami/obserwacjami byœmy mogli je na bierz¹co dodawaæ i zmieniaæ je za poœrednictwem ich nazw. S³ownik sk³ada siê z nazwy dowodu i przypisanej do nazwy tablicy bool, która mówi nam czy kafelek jest odkryty czy nie.
    public Dictionary<string, bool[]> observation = new Dictionary<string, bool[]>();


    //public SerializabeleDictionary<string, bool> observationChangedToSave;

    private void Awake()
    {
        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
        //ustalamy ile wyborów mo¿e mieæ dany dowód. 20 u nas
        //dodajemy dowód/obserwacje
        foreach (string observationName in observationList)
        {
            observation.Add(observationName, new bool[observationsSize]);
        }
    }


    public void SaveData(ref GameData data)
    {
        //observationChangedToSave = new SerializabeleDictionary<string, bool>();
       
        foreach (string observationName in observationList)
        {     
            
                for (int i = 1; i <= observationsSize; i++)
                {
                //Debug.Log("Ile?: " data.observationToSave.ContainsKey(i.ToString() + ":" + observationName) +" : "+ data.observationToSave.ContainsValue(observationDownload(i, observationName);
                if (data.observationToSave.ContainsKey(i.ToString() + ":" + observationName))
                {
                    data.observationToSave.Remove(i.ToString() + ":" + observationName);
                }
                    //Debug.Log("Ile?: " + i.ToString() + ":" + observationName);
                    data.observationToSave.Add(i.ToString() + ":" + observationName, observationDownload(i, observationName));
                }
        }

        
        //data.observationToSave = this.observation;
       
    }

    
    //Wczytywanko dancyh z pliku do dictionary
    public void LoadData(GameData data)
    {
        foreach (string observationName in observationList)
        {
            GameObject obserationToDestroy = GameObject.Find(observationName);
            for (int i = 1; i <= observationsSize; i++)
            {
                //Debug.Log("observationToSave Kay: " + i.ToString() + ":" + observationName + " observation in Data: "+data.observationToSave[i.ToString() + ":" + observationName]);
                if (data.observationToSave[i.ToString() + ":" + observationName]==true)
                {
                    observationUpdate(i, observationName);

                    //niszczonko podnaszalnych obiektów
                    if(i==1 && obserationToDestroy.GetComponent<Observation>().pickable)
                    {
                        Destroy(obserationToDestroy);
                    }
                }
            }

        }
    }
    
    void Start()
    {
        //List<ObservationList> observations = new List<ObservationList>();
        //observations.Add(new ObservationList("shoePrint", observation));
        //ObservationList result = observations.Find(x => x.name.Contains("shoePrint"));
        //Debug.Log(observations);
        //Debug.Log(observations.Exists(x => x.observationsArray[5] == false) +" result: "+ result+ " Find: "+ observations.Find(x => x.observationsArray[5] == false));
        /*
        Dictionary<string, bool[]> observations = new Dictionary<string, bool[]>();      
        observations.Add("shoePrintt", new bool[20]);
        Debug.Log(observations["shoePrintt", ]); //prints out 200
        */
        // cam = GetComponent<Camera>();


        
        //observation.Add("V3Print", new bool[20]);

        //observationUpdate(3, "shoePrint");
    }
    void Update()
    {      
       // transform.LookAt((new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);
       // Debug.Log("Up x:" + Input.mousePosition.x + " y: "+ Input.mousePosition.y);
    }

    //zminiamy dane dowody/obserwacje, czyli czy dany kafelek zosta³ przez nas odkryty
    public void observationUpdate(int i, string s)
    {
        if (i <= 0)
        {
            Debug.LogError("Poda³eœ za ma³y index, minimum 1");
            return;
        }
        else if (!observation.ContainsKey(s))
        {
            Debug.LogError("Nie ma takiego klucza w observation: " + s);
            return;
        }

        

        //pobieramy tablice bool dowodu przed zmian¹ do buforu. 
        bool[] observationArrayBufor = observation[s];
        //zminiamy odpowiedni indeks w buforze
        observationArrayBufor[i-1] = true;
        //zastêpujemy poprzedni¹ tablice dowodu zmienionym buforem
        observation[s] = observationArrayBufor;


        //Aktualizujemy tablice dziennik dialogue line za posrednictwen observacji, jeœli oczywiœcie funkcja coœ takigo wykryje 
        //Jest wywo³ywana observation storage za ka¿dym razem gdy observacje s¹ aktualizowane KOLEJNOŒÆ WA¯NA, PO AKTUALIZACJI OBSERWACJI
        dm.DialogueLineUpdateFromObservation();


        //Debug.Log("Set: " + shoePrint[i-1]);

        /*
        foreach (bool observations in observation[s])
        {
            Debug.Log("observationUpdate: " + observations);
        }
        */

        //wypisujemy tablice w kosoli unity(funkcja poni¿ej)
        //showObservationArray(s);
        //Debug.Log(s + ": " + observationArrayBufor[i - 1]);


        /*
        int a = 0;
        foreach (bool observation in observation["shoePrint"])
        {
            a++;
            Debug.Log("shoePrint" + ": " + a + " " + observation);
        }

        //observationArrayBufor[3] = true;
        // observation["V1Print"] = observationArrayBufor;
        int b = 0;
        foreach (bool observation in observation["V1Print"])
        {
            b++;
            Debug.Log("V1Print" + ": " + b + " " + observation);
        }
        observationArrayBufor = observation["V1Print"];
        Debug.Log("V1Print" + " 4 Kurwa  : " + observationArrayBufor[3]);
        Debug.Log("V2Print" + " 4 Kurwa  : " + observationArrayBufor[3]);

        int c = 0;
        foreach (bool observation in observation["V2Print"])
        {
            c++;
            Debug.Log("V2Print" + ": " + c + " " + observation);
        }
        */
    }

    //analogicznie dzia³anie co powyrzej, tylko zwracamy wartoœc a nie nadpisujemy
    public bool observationDownload(int i, string s)
    {
        //Debug.Log("int i: "+i);
        if (i == 0)
        {
            Debug.LogError("Poda³eœ za ma³y index, minimum 1");
            return false;
        }
        else if (!observation.ContainsKey(s))
        {
            Debug.LogError("Nie ma takiego klucza w observation: " + s);
            return false;
        }

        bool[] observationArrayBufor = new bool[20];
        observationArrayBufor = observation[s];
        /*
        foreach(bool observations in observation[s])
        {
            Debug.Log("observationsDownlad: " + observations);
        }
        */
        //if(observations.Exists(x => x.name == "s")
        //return false;
        return observationArrayBufor[i - 1];
    }

    public bool[] observationArrayDownload(string s)
    {
        bool[] observationArrayBufor = new bool[20];
        observationArrayBufor = observation[s];
        /*
        foreach(bool observations in observation[s])
        {
            Debug.Log("observationsDownlad: " + observations);
        }
        */
        //if(observations.Exists(x => x.name == "s")
        //return false;
        return observationArrayBufor;
    }

    //wypisujemy uaktualnione dane w konsoli unity
    public void showObservationArray(string s)
    {
        int i = 0;
        foreach (bool observation in observation[s])
        {
            i++;
            Debug.Log(s+": "+i + " " + observation);
        }

    }

}
