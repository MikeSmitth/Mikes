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
    [SerializeField] GameObject NPCUIPanel;
    [SerializeField] GameObject BackButton;
    [SerializeField] Animator portraitAnimator;
    [SerializeField] Animator scrollAnimator;
    [SerializeField] AudioClip PaperOpenSound;
    [SerializeField] AudioClip PaperClouseSound;

    List<string> items;
    DialogueMenager dm;
    CameraController cc;
    SoundsManager sm;
    // Start is called before the first frame update
    void Awake()
    {
        //ButtonUpdate();
        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        sm = GameObject.Find("Managers").GetComponent<SoundsManager>();
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }
    private void Start()
    {
        items = new List<string>();
        //gameObject.SetActive(false);
    }
   
    private void Update()
    {    
    }

    public void NamesUpdate()
    {
        //var dropdown = transform.GetComponent<Dropdown>();
        // dropdown.options.Clear();
        bool[] dialogueLineBoolArray;


        foreach (KeyValuePair<string, bool[]> dialogue in dm.npcDialogueLine)
        {
            // Debug.Log(dialogue.Key);
            dialogueLineBoolArray = dm.DialogueLineArrayDownload(dialogue.Key);
            foreach (bool dialogueLineBool in dialogueLineBoolArray)
            {

                // Debug.Log("dialogueLineBool: " + dialogueLineBool);
                if (dialogueLineBool == true)
                {
                    // Debug.Log("dodano: " + dialogue.Key);
                    items.Add(dialogue.Key);
                    break;
                }

            }


        }

        //foreach (var item in items)
        //{
        // dropdown.options.Add(new Dropdown.OptionData() { text = item });
        // }
        // dropdown.RefreshShownValue();
    }

    void NotesUpdate()
    {
        if(!dm.npcDialogueLine.ContainsKey(nameText.text))
        {
             Debug.LogWarning("Nie ma takiego klucza w dialogach");
            return;
        }

        notesText.text = "\n";
        var dropdownName = transform.GetComponent<Dropdown>();
        //var dropdownNotes = GameObject.Find("DropdownNPCNotes").transform.GetComponent<TMP_Dropdown>();
        bool[] dialogueLineBoolArray;

        // Debug.Log("2");
        currentStory = new Story(currentNPCNotesJSON.text);

        string tagKey = "";
        string tagValue = "";

        //dropdownNotes.options.Clear();
        int i = 1;
        //Debug.Log(dialogue.Key);

        dialogueLineBoolArray = dm.DialogueLineArrayDownload(nameText.text);



        //Debug.Log("5");
        foreach (bool dialogueLineBool in dialogueLineBoolArray)
        {
            if (dialogueLineBool == true)
            {

                currentStory.ResetState();

                while (currentStory.canContinue)
                {
                    //Debug.Log("6");
                    currentStory.Continue();

                    List<string> currentTags = currentStory.currentTags;

                    foreach (string tag in currentTags)
                    {
                        string[] splitTag = tag.Split(':');

                        if (splitTag.Length != 2)
                        {
                            Debug.LogError("Tag could not be appropriately parsed: " + tag);
                        }

                        tagKey = splitTag[0].Trim();
                        tagValue = splitTag[1].Trim();

                        if (tagKey == nameText.text && tagValue == i.ToString())
                        {

                            //Debug.Log(currentStory.currentText);

                            notesText.text += currentStory.currentText;

                        }
                    }
                }
            }
            i++;
        }
    }







    public void NextName()
    {
        if (items.Count == 0)
            return;

        int nameIndex = 0;

        nameIndex = (items.IndexOf(nameText.text));
        //Debug.Log("NextName index :" + (nameIndex) + " items.Count: " + items.Count);
        //Debug.Log("1 :" + items[0] + " 2: " + items[1]);
        if ((nameIndex + 1) >= 0 && (nameIndex + 1) <= (items.Count-1))
        {
            // Debug.Log("NextName 2 index :" + (nameIndex + 1));
            nameText.text = items[nameIndex + 1];
            portraitAnimator.Play(nameText.text);
            //Debug.Log("niezawijanko :" + (nameIndex + 1) + " Index: " + nameIndex + 1);
        }
        
        //S³u¿y do "zawijania" tekstu gdy jest koniec listy to wracamy na pocz¹tek
        else if(nameIndex + 1 == items.Count)
        {
           
            nameText.text = items[0];
            //Debug.Log("NextName ZAWIJANKO index :" + (items[0]));
            portraitAnimator.Play(nameText.text);
        }
     
    }
    public void PreviousName()
    {
        if (items.Count == 0)
            return;

        int nameIndex = 0;

        nameIndex = (items.IndexOf(nameText.text));

        // Debug.Log("NextName 1 index :" + (nameIndex - 1) + " items.Count: " + items.Count);
        if ((nameIndex - 1) >= 0 && (nameIndex - 1) <= (items.Count-1))
        {
            //Debug.Log("NextName 2 index :" + (nameIndex - 1));
            nameText.text = items[nameIndex - 1];
            portraitAnimator.Play(nameText.text);
            //Debug.Log("NextName 3 index :" + (nameIndex - 1));
        }

        //S³u¿y do "zawijania" tekstu gdy jest koniec listy to wracamy na pocz¹tek
        else if ((nameIndex - 1) == -1)
        {
            nameText.text = items[items.Count-1];
            portraitAnimator.Play(nameText.text);
        }
    }



    public void togglePanelShow()
    {


        if (notesPanel.activeSelf)
        {

            cc.isLook = false;
            cc.boxCollider(false);
            //Debug.Log("siemafalse");
            notesPanel.SetActive(false);
        }
        else
        {        
            cc.isLook = true;
            cc.boxCollider(true);
            //Debug.Log("siemafalse");
            notesPanel.SetActive(true);
            // Debug.Log("0");
            //ScrollBackAnimation();
            NPCUIPanel.SetActive(true);
        }
        //consoleShowEvidence();

    }


    public void ScrollAnimation()
    {
        NotesUpdate();
        //Debug.Log(scrollAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        if (scrollAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "ScrollOpen")
        {
            sm.PlaySoundClip(PaperOpenSound);
            scrollAnimator.SetTrigger("Scroll");
            NPCUIPanel.SetActive(false);
        }


    }
    public void ScrollBackAnimation()
    {

        if (scrollAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Scroll")
        {
           // Debug.Log("1");
            sm.PlaySoundClip(PaperClouseSound);
            scrollAnimator.SetTrigger("Scroll");
            NPCUIPanel.SetActive(true);
            setNPCUIPanel();
        }
        //Debug.Log("2");
    }
    //pokazuje dowód z pamiêci ekwipunku, nie masz mo¿liwoœci edycji 


    void setNPCUIPanel()
    {
        int nameIndex = 0;
        NamesUpdate();

        if (items.Count > 0)
        {
            if (items.Contains(nameText.text))
            {
                nameIndex = (items.IndexOf(nameText.text));
            }

            nameText.text = items[nameIndex];
            portraitAnimator.Play(nameText.text);
        }
        else
        {
            nameText.text = "Noone";
            portraitAnimator.Play("Default");
        }

    }



    public void showNPCNotes()
    {
        togglePanelShow();
        setNPCUIPanel();
    }
}
