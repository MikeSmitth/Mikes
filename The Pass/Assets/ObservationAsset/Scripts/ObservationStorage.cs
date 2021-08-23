using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ObservationStorage : MonoBehaviour
{
    bool[] shoePrint = new bool[20];
    bool[] woda = new bool[20];
    private Camera cam;


    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {      
        transform.LookAt((new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);
        Debug.Log("Up x:" + Input.mousePosition.x + " y: "+ Input.mousePosition.y);
    }
    public void observationUpdate(int i, string s)
    {
        shoePrint[i-1] = true;
        Debug.Log("Set: " + shoePrint[i-1]);
    }
    public bool observationDownload(int i)
    {
        return shoePrint[i];
    }
    
    
}
