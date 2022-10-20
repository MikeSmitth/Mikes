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
    Observation ob;

    //GameObject[] arrows;





    void Awake()
    {
        //arrows = GameObject.FindGameObjectsWithTag("Arrow");
        // Debug.Log(name+" arrow: " + arrows.Length);
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
      
    }

    bool fromEQ()
    {
        Observation ob = transform.root.GetComponent<Observation>();
        return ob.fromEQ;
        // Debug.Log("fromEQ: " + bufor);    
    }

    // BÊDZIE to funkcja zmieniaj¹ca z jakiej tablicy s¹ pobierane dane/ narazie nie jest urzywana, ale oszczêca liniki kodu 
    bool observationFromWhat()
    {
              
            return os.observationDownload((Convert.ToInt32(name)), transform.root.name);       
    }

    //funkcja ukrywaj¹ca kafelki obserwacji. Jest uruchamiana klikniêcem na obserwacje w Unity, lub na obserwacje z eq
    public void setButtonOff()
    {
        //Debug.Log(transform.root.name + " :name ");

        //pobieramy obserwacje PATRZ!!! FUNKCJA observationDownload SKRYPT ObservationStorage z funkcji boolowskiej powyrzej !!! nazwa kafelka odpowiada indeksowi dlatego zmianiamy ja na wartoœæ INT 
        //ten if jest potrzebny gdy¿ po klikniêciu na obserwacje pierwszy kafelek jest domyœlnie pokazany na ekranie(ale jeszcze nie odkryty, nie ustawiony na TRUE), a reszta zakryta
        if ((observationFromWhat() == false)&&(Convert.ToInt32(name)-1)!=0)
        {
                //nie wy³¹czamy dowodów jeœli jest przegl¹dane z EQ 
                if (!fromEQ())
                gameObject.SetActive(false);
        } 
        
        else if (observationFromWhat() == true)
        {
            //Debug.Log(name+" clicked ");
            GetComponent<Button>().onClick.Invoke();
            GetComponent<Image>().color = new Color32(212, 212, 212, 255);

            //Funkcja pokazuj¹ca przypisane strza³ki. Poni¿ej 

            showArrows();
        }
        
        if (observationFromWhat() == false)
        {
            //Debug.Log(name + " hide ");
            //Funkcja ukrywaj¹ca przypisane strza³ki. Poni¿ej 
            
             hideArrows();
        }

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
            if (child.tag == "Arrow" )
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



}
