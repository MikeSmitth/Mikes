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
    [SerializeField] TextAsset currentNPCNotesJSON;
    Story currentStory;

    [Header("NPC Notes")]
    [SerializeField] TextMeshProUGUI notesText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject notesPanel;
    [SerializeField] Animator portraitAnimator;


    DialogueMenager dm;

    // Start is called before the first frame update
    void Awake()
    {
        //ButtonUpdate();
        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
       
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }
    private void Start()
    {
        gameObject.SetActive(false);
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

            }       
            //IF poniewa¿ nie chcemy pokazywaæ w QE dowodów których nie zaczeliœmy badaæ 

        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        dropdown.RefreshShownValue();
    }

    // Update is called once per frame
    public void toggleBottonShow()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        //consoleShowEvidence();

    }

    public void togglePanelShow()
    {
       
        if (notesPanel.activeSelf)
        {
            //Debug.Log("siemafalse");
            notesPanel.SetActive(false);
        }
        else
        {
            //Debug.Log("siemafalse");
            notesPanel.SetActive(true);
        }
        //consoleShowEvidence();

    }

    //pokazuje dowód z pamiêci ekwipunku, nie masz mo¿liwoœci edycji 
    public void showNPCNotes()
    {
        //Debug.Log("1");
        var dropdownName = transform.GetComponent<Dropdown>();
        //var dropdownNotes = GameObject.Find("DropdownNPCNotes").transform.GetComponent<TMP_Dropdown>();
        bool[] dialogueLineBoolArray;
        List<string> items = new List<string>();
       // Debug.Log("2");
        currentStory = new Story(currentNPCNotesJSON.text);

        string tagKey = "";
        string tagValue = "";

 

        nameText.text = "???";
        portraitAnimator.Play("Default");
        notesText.text = "";
        togglePanelShow();
        //dropdownNotes.options.Clear();


        int i= 1;
        //Debug.Log(dialogue.Key);
       
       
        //Debug.Log("3");
        if ( dropdownName.options.Count > 0 &&dm.npcDialogueLine.ContainsKey(dropdownName.options[dropdownName.value].text))
        {
            //Debug.Log("4");
            dialogueLineBoolArray = dm.DialogueLineArrayDownload(dropdownName.options[dropdownName.value].text);
        }
        else
        {
            Debug.LogWarning("DialogueLineArray = 0");
            return;
        }
       // Debug.Log("5");
        foreach (bool dialogueLineBool in dialogueLineBoolArray)
            {


            //Debug.Log("dialogueLineBool: " + dialogueLineBool + " o indexie "+ i);
            //Debug.Log("dialogueLineBool: " + dialogueLineBool);
            //Debug.Log(dropdownName.options[dropdownName.value].text + " dialogueLineBool true: " + dialogueLineBool);
            if (dialogueLineBool == true)
                {
                nameText.text = dropdownName.options[dropdownName.value].text;
                portraitAnimator.Play(dropdownName.options[dropdownName.value].text);

                currentStory.ResetState();


                while (currentStory.canContinue)
                {
                    currentStory.Continue();
                    
                    List<string> currentTags = currentStory.currentTags;


                    foreach (string tag in currentTags)
                    {
                       string[] splitTag = tag.Split(':');
                       //Debug.Log("tag " + tag);

                       if (splitTag.Length != 2)
                       {
                             Debug.LogError("Tag could not be appropriately parsed: " + tag);
                       }

                       tagKey = splitTag[0].Trim();
                       tagValue = splitTag[1].Trim();
                        Debug.Log("Tag: " + tagKey + " Value: " + tagValue + " Story: " + currentStory.currentText);
                        if (tagKey == dropdownName.options[dropdownName.value].text && tagValue == i.ToString())
                        {
                            notesText.text += i + "-true: " + currentStory.currentText;
                            //items.Add(i + "-true: " + currentStory.currentText);
                        }
                    }

                // tagKey != takenName || tagValue != takenValue
                
                    
                }

                //Wykrywanko czy s¹ tagi, jeœli nie to ContinueStory();, S³uzy do skipowania pustych linijek dialogowych
 

                //Debug.Log("dodano: " + i + ": true");


            }
            i++;
            }
        /*
        foreach (var item in items)
        {
            dropdownNotes.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
        dropdownNotes.RefreshShownValue();//IF poniewa¿ nie chcemy pokazywaæ w QE dowodów których nie zaczeliœmy badaæ  
        */
    }
}
