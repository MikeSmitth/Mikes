using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// dzia�a tylko w edutorze unity itak �rednio jest tego urzywa� w kodzie https://youtu.be/HP1EYVwAhRg?t=222
//using Ink.UnityIntegration;


public class DialogueMenager : MonoBehaviour, IDataPresistence
{
    [Header("Menu")]
    [SerializeField] GameObject menu;

    [Header("Dialogue UI")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI displayNameText;
    [SerializeField] Animator portraitAnimator;
    [SerializeField] Animator choicesAnimator;
    private Animator layoutAnimator;
    Story currentStory;

    [Header("Choices UI")]
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;

    [Header("Load Globals JSON")]
    [SerializeField] TextAsset loadGloabalJSON;

    [Header("Observations List")]
    [SerializeField] string[] dialogueLineList;
    [SerializeField] int dialogueLineSize = 40;


    public bool dialoguePlaying { get; private set; }

    static DialogueMenager instance;

    const string SPEAKER_Tag = "speaker";
    const string PORTRAIT_Tag = "portrait";
    const string LAYOUT_Tag = "layout";
    const string TIME_Tag = "time";
    const string OBSERVATION_Tag = "observation";
    const string SKIP_Tag = "skip";

    GlobalManager gm;
    ObservationStorage os;
    BottonToggleShow bts;
    CameraController cc;
    ButtonManager bm;


    [Header("Observation Updating Story Line")]
    [Header("Observation Name")]
    [SerializeField] string[] observationNames;
    [SerializeField] int[] observationIndexes;

    [Header("Updated Dialogue Name")]
    [SerializeField] string[] dialogueNames;
    [SerializeField] int[] dialogueIndexes;


    //Zmienne do odtwarzania d�wi�ku
    bool allowToPlay;
    bool fromLoad;



    public Dictionary<string, bool[]> npcDialogueLine = new Dictionary<string, bool[]>();  


    //zmiena przechwywuj�ca informacje czy zauktualizowa� mo�na npcDialogueLine
    public bool changeDialogueLine { get; private set; } = false;

    private void Awake()
    {
               

        foreach (string observationName in dialogueLineList)
        {
            npcDialogueLine.Add(observationName, new bool[dialogueLineSize]);
        }



        if (instance != null)
        {
            Debug.LogWarning("More than one Dialogue Menager");
        }
        instance = this;

        //gm = GameObject.Find("Managers").GetComponent<GlobalManager>();
        bm = GameObject.Find("Managers").GetComponent<ButtonManager>();
        gm = GameObject.Find("Managers").GetComponent<GlobalManager>();
        os = GameObject.Find("Managers").GetComponent<ObservationStorage>();
        bts = GameObject.Find("Dropdown").GetComponent<BottonToggleShow>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        gm.GlobalManagerLoadGlobalJson(loadGloabalJSON);
    }

    
    public void SaveData(ref GameData data)
    {
        //observationChangedToSave = new SerializabeleDictionary<string, bool>();

        foreach (string name in dialogueLineList)
        {

            for (int i = 1; i <= dialogueLineSize; i++)
            {
                //Debug.Log("Ile?: " data.observationToSave.ContainsKey(i.ToString() + ":" + observationName) +" : "+ data.observationToSave.ContainsValue(observationDownload(i, observationName);
                if (data.dialogueLineToSave.ContainsKey(i.ToString() + ":" + name))
                {
                    data.dialogueLineToSave.Remove(i.ToString() + ":" + name);
                }
                //Debug.Log("Ile?: " + i.ToString() + ":" + observationName);
                data.dialogueLineToSave.Add(i.ToString() + ":" + name, DialogueLineDownload(i, name));
            }
        }


        //data.observationToSave = this.observation;

    }


    //Wczytywanko dancyh z pliku do dictionary
    public void LoadData(GameData data)
    {
        foreach (string name in dialogueLineList)
        {
            for (int i = 1; i <= dialogueLineSize; i++)
            {
                //Debug.Log("observationToSave Kay: " + i.ToString() + ":" + observationName + " observation in Data: "+data.observationToSave[i.ToString() + ":" + observationName]);
                if (data.dialogueLineToSave.ContainsKey(i.ToString() + ":" + name) && data.dialogueLineToSave[i.ToString() + ":" + name] == true)
                {
                    //zmianna monitoruj�ca czy observationUpdate jest wywo�ywane z LoadData, by np nie wywo�ywac d�wi�ku pczy �adowaniu danych 
                    fromLoad = true;


                    DialogueLineUpdate(i, name);
            
                    
                }
            }

        }
    }


    //Aktualizujemy tablice dziennik dialogue line za posrednictwen observacji, je�li oczywi�cie funkcja co� takigo wykryje
    //Jest wywo�ywana tu i w observation storage za ka�dym razem gdy observacje s� aktualizowane
    public void DialogueLineUpdateFromObservation()
    {
        //P�tla sprawdzajaca czy i je�li mamy inn� observacje lub dialogue line potrzerbne do pokazania tego dowodu
        if (observationNames.Length == observationIndexes.Length && dialogueNames.Length == dialogueIndexes.Length)
        {
            int i = 0;
            // Debug.Log("if");
            foreach (string name in observationNames)
            { 
                if(os.observation.ContainsKey(name))
                {
                    if(os.observationDownload(observationIndexes[i],name)==true)
                    {
                        //Debug.LogError("dialogueIndexes: " + dialogueIndexes[i] + " dialogueNames[i]: "+ dialogueNames[i]);
                        DialogueLineUpdate(dialogueIndexes[i], dialogueNames[i]);
                    }
                }
                else
                {
                    Debug.LogError("Nie ma takiego klucza w obserwacjach: " + name);
                }
                i++;
            }
        }
        else
        {
            Debug.LogError(" Nie mo�na odczyta� oceswacji. D�ugo�� listy obserwacji lub dialogu : " + observationNames.Length + " : "+ dialogueNames.Length +" Nie odpowiada d�ugosci list idex�w: " + observationIndexes.Length + " : " + dialogueIndexes.Length);
        }
    }

    public void DialogueLineUpdate(int i, string s)
    {


        //zmienna wykrywaj�ca czy obserwacja uleg�a zmianie 
        if (DialogueLineDownload(i, s) == false && fromLoad == false)
        {
            allowToPlay = true;
        }


        if (i <= 0)
        {
            Debug.LogError("Poda�e� za ma�y index, minimum 1");
            return;
        }
        else if(!npcDialogueLine.ContainsKey(s))
        {
            Debug.LogError("Nie ma takiego klucza w npcDialogueLine: "+ s);
        }
        //pobieramy tablice bool dowodu przed zmian� do buforu. 
        //Debug.Log(s+" <-NAZWA ZamananpcDialogueLine na true INDEX-> "+ i);
        bool[] observationArrayBufor = npcDialogueLine[s];
        //zminiamy odpowiedni indeks w buforze
        observationArrayBufor[i - 1] = true;
        //zast�pujemy poprzedni� tablice dowodu zmienionym buforem
        npcDialogueLine[s] = observationArrayBufor;

        //Wydajemy d�wi�ki odkrytego przycisku w innej obserwacji, je�li oczywi�cie funkcja co� takigo wykryje   if (allowToPlay), to poniewa� chcemy d�wiek gra� tylko raz
        if (allowToPlay)
        {
            bm.UpdateButtonSound(s, i);
        }
        allowToPlay = false;
        fromLoad = false;


    }

    //analogicznie dzia�anie co powyrzej, tylko zwracamy warto�c a nie nadpisujemy
    public bool DialogueLineDownload(int i, string s)
    {
        if (i <= 0)
        {
            Debug.LogError("Poda�e� za ma�y index, minimum 1");
            return false;
        }
        else if (!npcDialogueLine.ContainsKey(s))
        {
            Debug.LogError("Nie ma takiego klucza w npcDialogueLine: " + s);
            return false;
        }

        bool[] observationArrayBufor = new bool[20];
        observationArrayBufor = npcDialogueLine[s];

        return observationArrayBufor[i - 1];
    }

    public bool[] DialogueLineArrayDownload(string s)
    {
        // observationArrayBufor = new bool[20];
       if (!npcDialogueLine.ContainsKey(s))
        {
            Debug.LogError("Nie ma takiego klucza w npcDialogueLine: " + s);
        }

        bool[] observationArrayBufor = npcDialogueLine[s];
        /*
        foreach(bool observations in observation[s])
        {
            Debug.Log("observationsDownlad: " + observations);
        }
        */
        //if(observations.Exists(x => x.name == "s")
        //return false;
        return observationArrayBufor;
    }

    //Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Menu").SetActive(false);
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        dialoguePlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
            int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(" sellected: " + EventSystem.current.currentSelectedGameObject.transform.name);

        if (!dialoguePlaying)
        {
            return;
        }

        //ENTER kontynu�uje historyjk� 
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON, string speaker)
    {


        //blokujemy i nie, rozgl�danei si� i aktywujemy box collider kt�ry plokuje interakcje z oroczeniem

        menu.SetActive(false);
        cc.isLook = true;
        cc.boxCollider(true);

        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);

        
        //ustawaimy imi� rozmowcy przez speaker

        gm.StartListening(currentStory, speaker);

        displayNameText.text = "???";
        portraitAnimator.Play("Default");
        layoutAnimator.Play("right");

  
        ContinueStory();
    }

    void ExitDilogueMode()
    {
        //blokujemy i nie, rozgl�danei si� 
        menu.SetActive(true);

        cc.isLook = false;
        cc.boxCollider(false);

        gm.StopListening(currentStory);

        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }



    public void ContinueStory()
    {

        // p�tla skipuj�ca puste dialogi!!!
        /*
        while (dialogueText.text =="")
        {
             Debug.Log("dzia�a |"+ dialogueText.text + "|");           
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        */

        if (currentStory.canContinue)
        {
            //PREVIOUS_TEXT = currentStory.currentText;
            dialogueText.text = currentStory.Continue();
            //Debug.Log("dialogueText: " + dialogueText.text);
            /*
            if (PREVIOUS_TEXT == "")
            {
                PREVIOUS_TEXT = currentStory.currentText;
            }
            */
            //Debug.Log("ContinueStory() PREVIOUS_TEXT|" + PREVIOUS_TEXT + "|");
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDilogueMode();
        }
        //Debug.Log("dzia�a |" + dialogueText.text + "|: "+ currentStory.variablesState["bob"]);

    }


    void HandleTags(List<string> currentTags)
    {



        foreach (string tag in currentTags)
        {
            //Debug.Log("tag " + tag);
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();            

            switch (tagKey)
            {
                case SPEAKER_Tag:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_Tag:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_Tag:
                    layoutAnimator.Play(tagValue);
                    break;
                case TIME_Tag:
                    gm.SetTime(float.Parse(tagValue));
                    break;
            //Badamy kafelki/dowody i updejtujemy przycisk, gdy zdobendziemy 
                case OBSERVATION_Tag:
                    string[] splitValue = tagValue.Split('-');
                    string name = splitValue[0].Trim();
                    string index = splitValue[1].Trim();
                    os.observationUpdate(int.Parse(index), name);
                    bts.ObservationButtonUpdate();
                    break;
                case SKIP_Tag:

                    //if (currentStory.canContinue)
                    //{
                    //Debug.LogWarning("SKIPED TEXT: " + currentStory.currentText + " dla tagu: " + tagValue);
                    ContinueStory();

                    //}
                    /*
                    if (currentStory.canContinue)
                    {
                        dialogueText.text = PREVIOUS_TEXT;
                        currentStory.Continue();
                        HandleTags(currentStory.currentTags);
                        DisplayChoices();
                    }
                    */
                    break;
                default:
                    Debug.LogWarning("Tag came in but not currently being Handled: " + tag);
                    break;
            }
        }

        //Wykrywanko czy s� tagi, je�li nie to ContinueStory();, S�uzy do skipowania pustych linijek dialogowych
        /*
        if (currentTags.Count == 0)
        {
           Debug.Log("dzia�a |" + dialogueText.text + "|");
            ContinueStory();
        }
        */
    }

    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        //ukrywanko nieurzywanych wybor�w 
        for (int i= index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        choicesAnimator.Play(choices[index].name);
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        Debug.Log(choiceIndex);
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }


    //dokonujemy wyboty przyciskiem w  UI
    public void MakeChoiceFromButton()
    {
        if (!choices[0].activeSelf)
        {
            //Debug.Log("1");
            ContinueStory();
            return;
        }
        MakeChoice(0);
        /*
       

        foreach (GameObject choice in choices)
        {
            Debug.Log("2 choice: " + choice.name + " sellected: "+ EventSystem.current.currentSelectedGameObject.transform.name);
            if (EventSystem.current.currentSelectedGameObject.transform.name == choice.name)
            {              
                choice.GetComponent<Button>().onClick.Invoke();
                return;
            }
        }
       

            //Debug.Log("2: " + EventSystem.current.currentSelectedGameObject);

        //Button selected;
        //selected = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //selected.onClick.Invoke();
        */
    }


    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        gm.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
}
