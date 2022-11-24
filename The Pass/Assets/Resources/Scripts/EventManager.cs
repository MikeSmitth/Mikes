using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    
    public static EventManager current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }


    public event Action<int> onDoorwayEnter;
    public void DoorwayEnter(int id)
    {
        //Debug.Log("EVENT SYSTEM door:" + tag + " id: " + id);
        if (onDoorwayEnter != null)
        {
            onDoorwayEnter(id);
        }
    }


    public event Action<int> onDoorwayExit;
    public void DoorwayExit(int id)
    {
        if (onDoorwayExit != null)
        {
            onDoorwayExit(id);
        }
    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
