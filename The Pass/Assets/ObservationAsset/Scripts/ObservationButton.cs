using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ObservationButton : MonoBehaviour
{
    //public int time=1;

    //public GameObject observationName;

    //tworzymy zminn�, kt�ra ma przechowywa� s�ownik/dane z obserwacjami o nazwie ObservationStorage
    ObservationStorage os;

    //GameObject[] arrows;
   
    void Start()
    {
        //arrows = GameObject.FindGameObjectsWithTag("Arrow");
        // Debug.Log(name+" arrow: " + arrows.Length);
        
       
        
    }

    //funkcja ukrywaj�ca kafelki obserwacji. Jest uruchamiana klikni�cem na obserwacje w Unity
    public void setButtonOff()
    {
        //Debug.Log(transform.root.name + " :name ");

        //wyszukujemy skrypt, podpi�ty do kamery, przechowywuj�cy dane o obserwacjach
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();

        //pobieramy obserwacje PATRZ!!! FUNKCJA observationDownload SKRYPT ObservationStorage !!! nazwa kafelka odpowiada indeksowi dlatego zmianiamy ja na warto�� INT 
        //ten if jest potrzebny gdy� po klikni�ciu na obserwacje pierwszy kafelek jest domy�lnie pokazany na ekranie(ale jeszcze nie odkryty, nie ustawiony na TRUE), a reszta zakryta
        if ((os.observationDownload((Convert.ToInt32(name)), transform.root.name)== false)&&(Convert.ToInt32(name)-1)!=0)
        {
            gameObject.SetActive(false);
        } 
        
        else if ((os.observationDownload((Convert.ToInt32(name)), transform.root.name) == true))
        {
            //Debug.Log(name+" clicked ");
            GetComponent<Button>().onClick.Invoke();
            GetComponent<Image>().color = new Color32(212, 212, 212, 255);

            //Funkcja pokazuj�ca przypisane strza�ki. Poni�ej 
            showArrows();
        }
        
        if (os.observationDownload((Convert.ToInt32(name)), transform.root.name) == false)
        {
            //Debug.Log(name + " hide ");
            //Funkcja ukrywaj�ca przypisane strza�ki. Poni�ej 
            hideArrows();
        }

        //os.showObservationArray(transform.root.name);
    }

    //funkcja pokazuj�ca odkryte wcze�niej kafelki obserwacji. Jest uruchamina przyciskiem w Unity 
    public void setButton()
    {
       // Debug.Log("11.5: " + arrows.Length);
        //hideArrows();
        gameObject.SetActive(true);
    }



    public void showArrows()
    {
         //Debug.Log("Show "+ name);
        foreach (Transform child in transform)
        {
            if (child.tag == "Arrow")
                //Debug.Log("arrow " + child.name);
            child.gameObject.SetActive(true);
        }
    }
    public void hideArrows()
    {
        //Debug.Log("1.5: " + arrows.Length);
        foreach (Transform child in transform)
        {
            if (child.tag == "Arrow")
                child.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
}
