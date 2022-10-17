using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ObservationButton : MonoBehaviour
{
    //public int time=1;

    //public GameObject observationName;

    //tworzymy zminn¹, która ma przechowywaæ s³ownik/dane z obserwacjami o nazwie ObservationStorage
    ObservationStorage os;

    

    //GameObject[] arrows;

    void Awake()
    {
        //arrows = GameObject.FindGameObjectsWithTag("Arrow");
        // Debug.Log(name+" arrow: " + arrows.Length);
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
    }

    //funkcja ukrywaj¹ca kafelki obserwacji. Jest uruchamiana klikniêcem na obserwacje w Unity, lub na obserwacje z eq
    public void setButtonOff()
    {
        //Debug.Log(transform.root.name + " :name ");

        //wyszukujemy skrypt, podpiêty do kamery, przechowywuj¹cy dane o obserwacjach

        //pobieramy obserwacje PATRZ!!! FUNKCJA observationDownload SKRYPT ObservationStorage !!! nazwa kafelka odpowiada indeksowi dlatego zmianiamy ja na wartoœæ INT 
        //ten if jest potrzebny gdy¿ po klikniêciu na obserwacje pierwszy kafelek jest domyœlnie pokazany na ekranie(ale jeszcze nie odkryty, nie ustawiony na TRUE), a reszta zakryta
        if ((os.observationDownload((Convert.ToInt32(name)), transform.root.name)== false)&&(Convert.ToInt32(name)-1)!=0)
        {
            gameObject.SetActive(false);
        } 
        
        else if ((os.observationDownload((Convert.ToInt32(name)), transform.root.name) == true))
        {
            //Debug.Log(name+" clicked ");
            GetComponent<Button>().onClick.Invoke();
            GetComponent<Image>().color = new Color32(212, 212, 212, 255);

            //Funkcja pokazuj¹ca przypisane strza³ki. Poni¿ej 
            showArrows();
        }
        
        if (os.observationDownload((Convert.ToInt32(name)), transform.root.name) == false)
        {
            //Debug.Log(name + " hide ");
            //Funkcja ukrywaj¹ca przypisane strza³ki. Poni¿ej 
            hideArrows();
        }

        
        //funkcja wyko¿ystywana tylko po to by nie mo¿na by³o odkryæ wiêce ga³êzi w obserwacji z qe, ni¿ jedn¹ 
        /*
        if (tooMuch() && fromEQ())
        {
            tooMuchSet(false);
            Debug.Log("Kurwa ");
        }
        else if (!tooMuch())
        {
            Debug.Log("Maæ");
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            tooMuchSet(true);
        }
        */
        //os.showObservationArray(transform.root.name);
    }

    //funkcja pokazuj¹ca odkryte wczeœniej kafelki obserwacji. Jest uruchamina przyciskiem w Unity 
    public void setButton()
    {
       // Debug.Log("11.5: " + arrows.Length);
        //hideArrows();


        //mo¿emy pokazywaæ dalej dowody jeœli nie przegl¹damy dowodów z eq
        if(!fromEQ())
        gameObject.SetActive(true);
    }



    public void showArrows()
    {
         //Debug.Log("Show "+ name);
        foreach (Transform child in transform)
        {

            // Jeœli obiekt jest strza³k¹ i nie jest przegl¹dany dowód z EQ pokarz strza³ki 
            if (child.tag == "Arrow" && !fromEQ())
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

    //zwraca wartoœæ zmeinnej niepozwalaj¹cej na edycje obserwacji z ekwipnku
    bool fromEQ()
    {
        Observation myParent = transform.root.GetComponent<Observation>();
        return myParent.fromEQ;
       // Debug.Log("fromEQ: " + bufor);    
    }

    //zwraca wartoœæ zmeinnej wspó³pracuj¹cej z t¹ powyzej, pozwala ona na tylko jednokrotn¹ edycje obeswacji z poziomu ekwipunku
    bool tooMuch()
    {
        Observation myParent = transform.root.GetComponent<Observation>();
        return myParent.tooMuch;
        // Debug.Log("fromEQ: " + bufor);    
    }
    //zwraca wartoœæ zmeinnej wspó³pracuj¹cej z t¹ powyzej
    void tooMuchSet(bool tooMuchSet)
    {
        Observation myParent = transform.root.GetComponent<Observation>();
        myParent.tooMuch = tooMuchSet;
         Debug.Log("toomuch: " + tooMuch());    
    }
    // Update is called once per frame
}
