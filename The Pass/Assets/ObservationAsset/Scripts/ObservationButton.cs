using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ObservationButton : MonoBehaviour
{
    public int time=1;
    ObservationStorage os;
    //public GameObject go;
    public void setButtonOff()
    {
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
        if ((os.observationDownload((Convert.ToInt32(name))-1)==false)&& (Convert.ToInt32(name)-1)!=0)
        {
            gameObject.SetActive(false);
        }
        else if ((os.observationDownload((Convert.ToInt32(name)) - 1) == true))
        {
            GetComponent<Button>().onClick.Invoke();
            GetComponent<Image>().color = new Color32(212, 212, 212, 255);
        }
    }
    public void setButton()
    { 
            gameObject.SetActive(true);
    }

    // Update is called once per frame
}
