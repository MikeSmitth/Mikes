using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NpcNotes : MonoBehaviour
{

    //[Header("Observation or DialogueLine Needed To Show")]
    //[SerializeField] string[] observationOrDialogueNames;
    //[SerializeField] int[] observationOrDialogueIndexes;


    [Header("Ink JSON NPCNotes")]
    [SerializeField] Story currentNPCNotes;

    DialogueMenager dm;

    // Start is called before the first frame update
    void Awake()
    {
        //ButtonUpdate();
        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    public void ButtonUpdate()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        bool[] dialogueLineBoolArray;
        List<string> items = new List<string>();

        foreach (KeyValuePair<string, bool[]> dialogue in dm.npcDialogueLine)
        {
            //Debug.Log(dialogue.Key);
            dialogueLineBoolArray = dm.DialogueLineArrayDownload(dialogue.Key);
            foreach (bool dialogueLineBool in dialogueLineBoolArray)
            {
               
                //Debug.Log("dialogueLineBool: " + dialogueLineBool);
                if (dialogueLineBool == true)
                {
                   //Debug.Log("dodano: " + dialogue.Key);
                    items.Add(dialogue.Key);
                    break;
                }

            }               //IF poniewa� nie chcemy pokazywa� w QE dowod�w kt�rych nie zaczeli�my bada� 
                
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        dropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void toggleBottonShow()
    {
        if (gameObject.active)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        //consoleShowEvidence();

    }



    //pokazuje dow�d z pami�ci ekwipunku, nie masz mo�liwo�ci edyzji 
    public void consoleShowEvidenceNoEdit()
    {


        /*
        var dropdown = transform.GetComponent<Dropdown>();

        //Debug.Log(dropdown.options[dropdown.value].text);
        //os.showObservationArray(dropdown.options[dropdown.value].text);




        //Debug.Log(dropdown.options[dropdown.value].text +" to "+ Resources.Load("Observations/V2Print.prefab"));


        //Jesli obiekt jest ju� na scenie, to nie tworzymy nowego 

        if (!GameObject.Find(dropdown.options[dropdown.value].text))
        {
            //tworzymy obiekt z folderu resources 
            GameObject.Instantiate((UnityEngine.Object)Resources.Load("Observations/" + dropdown.options[dropdown.value].text));
            //GameObject.Find(dropdown.options[dropdown.value].text).transform.SetPositionAndRotation = new Vector3();
            // GameObject.Find(dropdown.options[dropdown.value].text).transform.SetPositionAndRotation(cc.transform.position + cc.transform.forward * 2f, cc.transform.rotation);

            //ostawiamy obiekt blisko kamery
            GameObject.Find(dropdown.options[dropdown.value].text).transform.position = (cc.transform.position + cc.transform.forward * 1.5f);
            //obracamy sam model obiektu w kierunku kamery 
            GameObject.Find(dropdown.options[dropdown.value].text + "/Item").transform.rotation = (cc.transform.rotation);
        }
        //Instantiate(Resources.Load("Observations/V2Print.prefab"), transform.position + transform.forward * 2f, transform.rotation);


        //aktywujemy skrypt ob.observationStudy(); dla danego dowodu, wiemy kt�rego poniewa� znamy jego nazwe dropdown.options[dropdown.value].text
        Observation ob;
        ob = GameObject.Find(dropdown.options[dropdown.value].text).GetComponent<Observation>();

        //studiowanie obserwacji
        ob.observationStudy();

        //spogl�danie na obserwacje
        //if, bo nie obracamy sie w kierunku dowody kt�rego nie podnie�li�my, bo nie jest "podnaszalny" to w else, patrzymy przed siebie
        if (ob.pickable)
        {
            cc.lookAt(GameObject.Find(dropdown.options[dropdown.value].text).transform.position);
        }
        else
        {

            cc.lookAt(cc.transform.position + cc.transform.forward * 1.5f);
        }

        //jest to funkcja wy��czaj�ca edycje dowodu z eq
        ob.setInteractive();
        */
    }
        
}