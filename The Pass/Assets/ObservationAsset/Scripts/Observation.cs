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
    //public Animator scopesAnimator;
    int whatScope;
    ObservationStorage os;

    void Start()
    {
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
    }

    //funkcja uruchamiaj¹ca tryb badania obserwacji. 
    void OnMouseDown()
    {
        //scopesAnimator.enabled = true;
 
        canvas.SetActive(true);

        //wywo³ujemy funkcje setButtonOff() dla przycisków z tagami ObservationButton
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ObservationButton");       
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ObservationButton>().setButtonOff(); 
        }
       // os.showObservationArray(transform.root.name);
        //.SetInteger("whatScope", 1);
    }


    public void observationName(Button number)
    {
        //Debug.Log("Up "+ Convert.ToInt32(number.name)+" "+ name);
        os.observationUpdate(Convert.ToInt32(number.name), name);
    }

    //Funkcja przycisku wychodz¹cego z trybu obserwacji. Uruchamiana przyciskiem
    public void buttonExit()
    {
        //patrz na skrypt CameraController
        CameraController scopes = GameObject.Find("Main Camera").GetComponent<CameraController>();
        scopes.lookBack();
        //StartCoroutine(waiterAnimator());
        canvas.SetActive(false);
        //scopesAnimator.SetInteger("whatScope", 0);
        //os.showObservationArray(transform.root.name);
    }

    //Funkcja przycisku wchodz¹cego w tryp przygl¹dania sie. Znika UI z kafelkami. Uruchamiana przyciskiem
    public void buttonPreview()
    {
        panel.SetActive(!panel.activeSelf);
    }

    /*
    IEnumerator waiterAnimator()
    {
        yield return new WaitForSeconds(2f);
        scopesAnimator.enabled = false;
        //tu zrób znikanie pojawania siê coliderów
    }
    */
}

      

    