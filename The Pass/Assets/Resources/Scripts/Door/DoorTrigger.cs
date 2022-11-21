using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public int doorId  = 0;
    CameraController cc;
    bool isopen = false;
    // Start is called before the first frame update
    private void Awake()
    {
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (cc.hitTag().tag == "DoorTrigger"&&isopen==false)
        {
            isopen = true;
            //Debug.Log("TRIGGER door:" + tag + " id: " + doorId);
            EventManager.current.DoorwayEnter(doorId);
        }
        else
        {
            isopen = false;
            //Debug.Log("TRIGGER door:" + tag + " id: " + doorId);
            EventManager.current.DoorwayExit(doorId);
        }
    }
}
