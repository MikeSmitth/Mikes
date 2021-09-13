using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ObservationStorage : MonoBehaviour
{

    //private Camera cam;
    bool[] observationArray = new bool[20];
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
        observation.Add("shoePrint", observationArray);
        //observationUpdate(3, "shoePrint");
    }
    void Update()
    {      
       // transform.LookAt((new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);
       // Debug.Log("Up x:" + Input.mousePosition.x + " y: "+ Input.mousePosition.y);
    }
    public void observationUpdate(int i, string s)
    {
        bool[] observationArrayBufor = observation[s];
        observationArrayBufor[i-1] = true;       
        observation[s] = observationArrayBufor;
        //Debug.Log("Set: " + shoePrint[i-1]);
        
        /*
        foreach (bool observations in observation[s])
        {
            Debug.Log("observationUpdate: " + observations);
        }
        */
        //showObservationArray(s);
    }
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
    public void showObservationArray( string s)
    {
      foreach (bool observation in observation[s])
      {
          Debug.Log("observation: " + observation);
      }
      
    }

}
