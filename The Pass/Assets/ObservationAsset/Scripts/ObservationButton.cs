using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObservationButton : MonoBehaviour
{
    public int time=1;
    ObservationStorage os;
    //public GameObject go;
    public void setButtonOff()
    {
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
        if ((os.observationDownload((Convert.ToInt32(name))-1)==false))
        {
            gameObject.SetActive(false);
        }
        else
        {

        }
    }
    public void setButton()
    { 
            gameObject.SetActive(true);
    }

    // Update is called once per frame
}
