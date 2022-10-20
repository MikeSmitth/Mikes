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
    CameraController cc;

    //Wa�ne!!! Zmiena sprawdzaj�ca czy badamy dow�d z ekwipunku, je�li tak to z regu�y nie mo�emy edytowa�. Jest ona sprawdzana w skrypcie ObservationButtons i edytowana w Observation.
    public bool fromEQ = false;

    public bool pickAble = false;

    void Start()
    {
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void Update()
    {
        //wychodzimy z trybu sprawdzania dowod�w, dodatkowo nie przyciskiem w grze
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse1))
        {
            buttonExit();
        }

    }

    //funkcja uruchamiaj�ca tryb badania obserwacji. 
    void OnMouseDown()
    {

        //sprawdzamy pod if czy dow�d jest w zasi�gu 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, cc.interactiveDistance) && hit.collider.tag == "Interactive")
        {
            //pozwalamy na edycje, je�li w tryb szukania dowod�w weszli�my ze sceny/ miejsca
            fromEQ = false;
            observationStudy();
        }
      
    }

    //Updatejtuje dane 
    public void observationName(Button number)
    {
        //Debug.Log("Up "+ Convert.ToInt32(number.name)+" "+ name);
        os.observationUpdate(Convert.ToInt32(number.name), name);
    }

    //Funkcja przycisku wychodz�cego z trybu obserwacji. Uruchamiana przyciskiem
    public void buttonExit()
    {
        //patrz na skrypt CameraController
        CameraController scopes = GameObject.Find("Main Camera").GetComponent<CameraController>();
        scopes.lookBack();
        //StartCoroutine(waiterAnimator());
        canvas.SetActive(false);

        //Zmiena sprawdzaj�ca czy przygl�dasz si� obiektowi. Blokuje np poruszanie si� postaci
        cc.isLook = false;
        //scopesAnimator.SetInteger("whatScope", 0);
        //os.showObservationArray(transform.root.name);


    }

    //Funkcja przycisku wchodz�cego w tryp przygl�dania sie. Znika UI z kafelkami. Uruchamiana przyciskiem
    public void buttonPreview()
    {
        panel.SetActive(!panel.activeSelf);
    }
    //badanie observacji
    public void observationStudy()
    {

       
        //scopesAnimator.enabled = true;

        canvas.SetActive(true);
        //wywo�ujemy funkcje setButtonOff() dla przycisk�w z tagami ObservationButton
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ObservationButton");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ObservationButton>().setButtonOff();
        }
        // os.showObservationArray(transform.root.name);
        //.SetInteger("whatScope", 1);
        //Zmiena sprawdzaj�ca czy przygl�dasz si� obiektowi. Blokuje np poruszanie si� postaci

        cc.isLook = true;

    }


    public void setInteractive()
    {
        // nie pozwalamy na edycje, je�li w tryb szukania dowod�w weszli�my z ekwipunku
        fromEQ=true;
    }

    /*
    IEnumerator waiterAnimator()
    {
        yield return new WaitForSeconds(2f);
        scopesAnimator.enabled = false;
        //tu zr�b znikanie pojawania si� colider�w
    }
    */

    //wy��czamy interaktywnio��/mo�liwo�� edycji obserwacji, np podczas podgl�dania jej w ekwipunku
}

      

    