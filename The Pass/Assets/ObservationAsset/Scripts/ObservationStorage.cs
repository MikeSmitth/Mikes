using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ObservationStorage : MonoBehaviour
{

    //private Camera cam;

    //ustalamy ile wyborów mo¿e mieæ dany dowód
    bool[] observationArray = new bool[20];
    //Tworzymy s³ownik z dowodami/obserwacjami byœmy mogli je na bierz¹co dodawaæ i zmieniaæ je za poœrednictwem ich nazw. S³ownik sk³ada siê z nazwy dowodu i przypisanej do nazwy tablicy bool, która mówi nam czy kafelek jest odkryty czy nie.
    Dictionary<string, bool[]> observation = new Dictionary<string, bool[]>();

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
        
        //dodajemy dowód/obserwacje
        observation.Add("shoePrint", observationArray);

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
        //pobieramy tablice bool dowodu przed zmian¹ do buforu. 
        bool[] observationArrayBufor = observation[s];
        //zminiamy odpowiedni indeks w buforze
        observationArrayBufor[i-1] = true;
        //zastêpujemy poprzedni¹ tablice dowodu zmienionym buforem
        observation[s] = observationArrayBufor;


        //Debug.Log("Set: " + shoePrint[i-1]);

        /*
        foreach (bool observations in observation[s])
        {
            Debug.Log("observationUpdate: " + observations);
        }
        */

        //wypisujemy tablice w kosoli unity(funkcja poni¿ej)
        showObservationArray(s);
    }

    //analogicznie dzia³anie co powyrzej, tylko zwracamy wartoœc a nie nadpisujemy
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

    //wypisujemy uaktualnione dane w konsoli unity
    public void showObservationArray( string s)
    {
      foreach (bool observation in observation[s])
      {
          Debug.Log("observation: " + observation);
      }
      
    }

}
