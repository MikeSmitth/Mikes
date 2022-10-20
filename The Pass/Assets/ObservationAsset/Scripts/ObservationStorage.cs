using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ObservationStorage : MonoBehaviour
{

    //private Camera cam;


    

    //Tworzymy s�ownik z dowodami/obserwacjami by�my mogli je na bierz�co dodawa� i zmienia� je za po�rednictwem ich nazw. S�ownik sk�ada si� z nazwy dowodu i przypisanej do nazwy tablicy bool, kt�ra m�wi nam czy kafelek jest odkryty czy nie.
    public Dictionary<string, bool[]> observation = new Dictionary<string, bool[]>();

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


        //ustalamy ile wybor�w mo�e mie� dany dow�d. 20 u nas
        //dodajemy dow�d/obserwacje
        observation.Add("shoePrint", new bool[20]);
        observation.Add("V1Print", new bool[20]);
        observation.Add("V2Print", new bool[20]);
        //observation.Add("V3Print", new bool[20]);

        //observationUpdate(3, "shoePrint");
    }
    void Update()
    {      
       // transform.LookAt((new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);
       // Debug.Log("Up x:" + Input.mousePosition.x + " y: "+ Input.mousePosition.y);
    }

    //zminiamy dane dowody/obserwacje, czyli czy dany kafelek zosta� przez nas odkryty
    public void observationUpdate(int i, string s)
    {
        //pobieramy tablice bool dowodu przed zmian� do buforu. 
        bool[] observationArrayBufor = observation[s];
        //zminiamy odpowiedni indeks w buforze
        observationArrayBufor[i-1] = true;
        //zast�pujemy poprzedni� tablice dowodu zmienionym buforem
        observation[s] = observationArrayBufor;


        //Debug.Log("Set: " + shoePrint[i-1]);

        /*
        foreach (bool observations in observation[s])
        {
            Debug.Log("observationUpdate: " + observations);
        }
        */

        //wypisujemy tablice w kosoli unity(funkcja poni�ej)
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

    //analogicznie dzia�anie co powyrzej, tylko zwracamy warto�c a nie nadpisujemy
    public bool observationDownload(int i, string s)
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
