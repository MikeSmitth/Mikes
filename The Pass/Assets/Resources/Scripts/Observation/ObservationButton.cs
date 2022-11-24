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


    DialogueMenager dm;


    //czas potrzebny do zbadania
    [Header("Time Needed")]
    [SerializeField] public float timeNeeded = 2;

    [Header("Observation or DialogueLine Needed To Show")]
    [SerializeField] string[] observationOrDialogueNames;
    [SerializeField] int[] observationOrDialogueIndexes;
    //[SerializeField] string[] dialogueLineNames;
    //[SerializeField] int[] dialogueLineIndexes;

    //GameObject[] arrows;





    void Awake()
    {
        
        //arrows = GameObject.FindGameObjectsWithTag("Arrow");
        // Debug.Log(name+" arrow: " + arrows.Length);
        os = GameObject.Find("Managers").GetComponent<ObservationStorage>();


        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
      
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
        if ((observationFromWhat() == false) && (Convert.ToInt32(name)) != 1)
        {
            //Debug.Log(" Ale zadzia³a³o dla: " +(Convert.ToInt32(name)) + " <- dowód z obserwacji ->(wy³¹czony) " + transform.root.name + " Poniewa¿ false==" + observationFromWhat() );
            //nie wy³¹czamy dowodów(za drugim wejœciu w badanie obserwacji, bo w innym przypadku znikaj¹) jeœli jest przegl¹dane z EQ 
            if (!fromEQ())
            { 
            //Debug.Log((Convert.ToInt32(name)) + " <- dowód z obserwacji ->(wy³¹czony) " + transform.root.name +" Poniewa¿ false=="+ observationFromWhat());
            gameObject.SetActive(false);
            }
        } 
        
        else if (observationFromWhat() == true)
        {
            //Debug.Log((Convert.ToInt32(name)) + " <- dowód z obserwacji ->(w³¹czony ON CLICK) " + transform.root.name  );
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


        //Pêtla sprawdzajaca czy i jeœli mamy inn¹ observacje lub dialogue line potrzerbne do pokazania tego dowodu
        int i = 0;
        bool allowToShow = true;
        if (observationOrDialogueNames.Length == observationOrDialogueIndexes.Length)
        {
            //Debug.Log("if");
            foreach (string name in observationOrDialogueNames)
            {

                //Debug.Log("allowToShow = true observation dla: " + this.name + " z obserwacji: " + transform.root.name+" JEŒLI: true=="+ os.observationDownload(observationOrDialogueIndexes[i], name) + " z: " + name + " o indexie: " + observationOrDialogueIndexes[i]);
                if (os.observation.ContainsKey(name) )
                {
                    if (os.observationDownload(observationOrDialogueIndexes[i], name) == true)
                    {
                        gameObject.SetActive(true);
                    }
                    //Debug.Log("os.observationDownload: "+ name+" o indexie "+i+" :  " + os.observationDownload(observationOrDialogueIndexes[i], name)+ " == false");
                    allowToShow = false;
                    //Debug.Log("Observation = " + allowToShow);

                }
                //Debug.Log("allowToShow = true dialogueLine dla: " + this.name + " z obserwacji: " + transform.root.name + " JEŒLI: true==" + dm.DialogueLineDownload(observationOrDialogueIndexes[i], name)+ " z: "+ name + " o indexie: "+ observationOrDialogueIndexes[i]);
                else if (dm.npcDialogueLine.ContainsKey(name))
                {
                    if(dm.DialogueLineDownload(observationOrDialogueIndexes[i], name) == true)
                    {
                        gameObject.SetActive(true);
                    }
                    //Debug.Log("dm.DialogueLineDownload: " + name + " o indexie " + i + " :  " + dm.DialogueLineDownload(observationOrDialogueIndexes[i], name) + " == false");                
                    allowToShow = false;
                    //Debug.Log("Dialogue = " + allowToShow);

                }
                //Debug.Log("dm.DialogueLineDownload: " + name + " o indexie " + observationOrDialogueIndexes[i] + " :  " + dm.DialogueLineDownload(observationOrDialogueIndexes[i], name) + " == false");
                i++;
            }
        }
        else
        {
            Debug.LogError(" Nie mo¿na odczytaæ oceswacji. D³ugoœæ listy obserwacji: " + observationOrDialogueNames.Length + " Nie odpowiada d³ugosci listy idexów: " + observationOrDialogueIndexes.Length);
        }

        // Debug.Log("11.5: " + arrows.Length);
        //hideArrows();


        //mo¿emy pokazywaæ dalej dowody jeœli nie przegl¹damy dowodów z eq
        //Debug.Log(allowToShow + " <- allow to show | " + this.name + " | fromEQ()->" + fromEQ());
        if (!fromEQ()&&allowToShow==true)
        {
            Debug.Log("Przycisk: " + name + " W³¹czony z obserwacji  " + transform.root.name);
            gameObject.SetActive(true);
        }
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
