using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Observation : MonoBehaviour
{
    public GameObject canvas;
    public GameObject panel;
    public Animator scopesAnimator;
    int whatScope;
    ObservationStorage os;

    void Start()
    {
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
    }

    void OnMouseDown()
    {
        scopesAnimator.enabled = true;
        canvas.SetActive(true);
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ObservationButton");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ObservationButton>().setButtonOff(); ;
        }

        scopesAnimator.SetInteger("whatScope", 1);
    }

    public void observationName(Button number)
    {
        //Debug.Log("Up "+ Convert.ToInt32(number.name)+" "+ name);
        os.observationUpdate(Convert.ToInt32(number.name), name);
    }

    public void buttonExit()
    {
        scopesAnimator.enabled = false;
        canvas.SetActive(false);
        scopesAnimator.SetInteger("whatScope", 0);
    }
    public void buttonPreview()
    {
        panel.SetActive(!panel.activeSelf);
    }
}

      

    