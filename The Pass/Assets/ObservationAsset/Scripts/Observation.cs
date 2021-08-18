using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Observation : MonoBehaviour
{
    public GameObject panel;
    ObservationStorage os;
  
    void Start()
    {
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
    }

    void OnMouseDown()
    {
        panel.SetActive(true);

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ObservationButton");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ObservationButton>().setButtonOff(); ;
        }  
    }

    public void observationName(Button number)
    {
        Debug.Log("Up "+ Convert.ToInt32(number.name)+" "+ name);
        os.observationUpdate(Convert.ToInt32(number.name), name);
    }

    public void buttonExit() 
    {
        panel.SetActive(false);
    }

}

      

    