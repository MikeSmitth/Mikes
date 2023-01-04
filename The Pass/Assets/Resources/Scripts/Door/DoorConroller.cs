using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorConroller : MonoBehaviour
{


    public int doorId= 0;

    Animator DoorAnimator;

    
    // Start is called before the first frame update
    void Start()
    {
        DoorAnimator = this.GetComponent<Animator>();
        EventManager.current.onDoorwayEnter += OnDoorwayOpen;
        EventManager.current.onDoorwayExit += OnDoorwayClose;
    }


    void OnDoorwayOpen(int id)
    {

        //Debug.Log("OPEN door:" + tag + " id: " + id);
        if (id == this.doorId)
        {
            DoorAnimator.SetBool("open", true);
        }
    }
    void OnDoorwayClose(int id)
    {

        //Debug.Log("OPEN door:" + tag + " id: " + id);
        if (id == this.doorId)
        {
            DoorAnimator.SetBool("open", false);
        }
    }
    // Update is called once per frame


    private void OnDestroy()
    {
        EventManager.current.onDoorwayEnter -= OnDoorwayOpen;
        EventManager.current.onDoorwayExit -= OnDoorwayClose;
    }
}
