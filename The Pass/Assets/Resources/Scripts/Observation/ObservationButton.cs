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

    // B�DZIE to funkcja zmieniaj�ca z jakiej tablicy s� pobierane dane/ narazie nie jest urzywana, ale oszcz�ca liniki kodu 
    bool observationFromWhat()
    {
              
            return os.observationDownload((Convert.ToInt32(name)), transform.root.name);       
    }

    //funkcja ukrywaj�ca kafelki obserwacji. Jest uruchamiana klikni�cem na obserwacje w Unity, lub na obserwacje z eq
    public void setButtonOff()
    {






        //Debug.Log(transform.root.name + " :name ");

        //pobieramy obserwacje PATRZ!!! FUNKCJA observationDownload SKRYPT ObservationStorage z funkcji boolowskiej powyrzej !!! nazwa kafelka odpowiada indeksowi dlatego zmianiamy ja na warto�� INT 
        //ten if jest potrzebny gdy� po klikni�ciu na obserwacje pierwszy kafelek jest domy�lnie pokazany na ekranie(ale jeszcze nie odkryty, nie ustawiony na TRUE), a reszta zakryta
        if ((observationFromWhat() == false) && (Convert.ToInt32(name)) != 1)
        {
            //Debug.Log(" Ale zadzia�a�o dla: " +(Convert.ToInt32(name)) + " <- dow�d z obserwacji ->(wy��czony) " + transform.root.name + " Poniewa� false==" + observationFromWhat() );
            //nie wy��czamy dowod�w(za drugim wej�ciu w badanie obserwacji, bo w innym przypadku znikaj�) je�li jest przegl�dane z EQ 
            if (!fromEQ())
            { 
            //Debug.Log((Convert.ToInt32(name)) + " <- dow�d z obserwacji ->(wy��czony) " + transform.root.name +" Poniewa� false=="+ observationFromWhat());
            gameObject.SetActive(false);
            }
        } 
        
        else if (observationFromWhat() == true)
        {
            //Debug.Log((Convert.ToInt32(name)) + " <- dow�d z obserwacji ->(w��czony ON CLICK) " + transform.root.name  );
            //Debug.Log(name+" clicked ");
            GetComponent<Button>().onClick.Invoke();
            GetComponent<Image>().color = new Color32(212, 212, 212, 255);

            //Funkcja pokazuj�ca przypisane strza�ki. Poni�ej 

            showArrows();
        }
        

        if (observationFromWhat() == false)
        {
            //Debug.Log(name + " hide ");
            //Funkcja ukrywaj�ca przypisane strza�ki. Poni�ej 
            
             hideArrows();
        }





    }

    //funkcja pokazuj�ca odkryte wcze�niej kafelki obserwacji. Jest uruchamina przyciskiem w Unity 
    public void setButton()
    {


        //P�tla sprawdzajaca czy i je�li mamy inn� observacje lub dialogue line potrzerbne do pokazania tego dowodu
        int i = 0;
        bool allowToShow = true;
        if (observationOrDialogueNames.Length == observationOrDialogueIndexes.Length)
        {
            //Debug.Log("if");
            foreach (string name in observationOrDialogueNames)
            {

                //Debug.Log("allowToShow = true observation dla: " + this.name + " z obserwacji: " + transform.root.name+" JE�LI: true=="+ os.observationDownload(observationOrDialogueIndexes[i], name) + " z: " + name + " o indexie: " + observationOrDialogueIndexes[i]);
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
                //Debug.Log("allowToShow = true dialogueLine dla: " + this.name + " z obserwacji: " + transform.root.name + " JE�LI: true==" + dm.DialogueLineDownload(observationOrDialogueIndexes[i], name)+ " z: "+ name + " o indexie: "+ observationOrDialogueIndexes[i]);
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
            Debug.LogError(" Nie mo�na odczyta� oceswacji. D�ugo�� listy obserwacji: " + observationOrDialogueNames.Length + " Nie odpowiada d�ugosci listy idex�w: " + observationOrDialogueIndexes.Length);
        }

        // Debug.Log("11.5: " + arrows.Length);
        //hideArrows();


        //mo�emy pokazywa� dalej dowody je�li nie przegl�damy dowod�w z eq
        //Debug.Log(allowToShow + " <- allow to show | " + this.name + " | fromEQ()->" + fromEQ());
        if (!fromEQ()&&allowToShow==true)
        {
            Debug.Log("Przycisk: " + name + " W��czony z obserwacji  " + transform.root.name);
            gameObject.SetActive(true);
        }
    }



    public void showArrows()
    {
         //Debug.Log("Show "+ name);
        foreach (Transform child in transform)
        {

            // Je�li obiekt jest strza�k� i nie jest przegl�dany dow�d z EQ pokarz strza�ki 
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
