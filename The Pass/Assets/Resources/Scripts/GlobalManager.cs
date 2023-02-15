using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;
using UnityEngine.SceneManagement;

//nie ma MonoBehaviour!!!
public class GlobalManager : MonoBehaviour, IDataPresistence
{
    [Header("In Game Time(144 = day, 1 = 10min)")]
    public float inGameTime = 0;

    [Header("Tiredness (No More Than 100)")]
    public float tiredness = 0;
 
    DialogueMenager dm;

    Story currentstory;

    //w global menad�erze obs�ugujemy globalny plik z zmeinnymi od dialog�w
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }
    
    
    public void SaveData(ref GameData data)
    {   
        data.inGameTimeToSave = this.inGameTime;     
        data.tirednessToSave = this.tiredness;     
        //Debug.Log("Seved in global manager = " + data.inGameTimeToSave);
    }

    public void LoadData(GameData data)
    {
        this.inGameTime = data.inGameTimeToSave;
        this.tiredness = data.tirednessToSave;
        //Debug.Log("Loaded in global manager = " + this.inGameTime);
    }

    public void GlobalManagerLoadGlobalJson(TextAsset loadgloabalJSON)
    {
        Story globalVariablesStory = new Story(loadgloabalJSON.text); 
        

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            //Debug.Log("Variable " + name + " value :" + value);
        }
    }
    

    static GlobalManager instance;
    private void Awake()
    {   

        if (inGameTime != 0)
        {
            Debug.LogWarning("Czas nie jest wyzerowany");
        }
        if (instance != null)
        {
            Debug.LogWarning("More than one Time Menager");
        }
        instance = this;

        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
    
    }






    public void Sleep()
    {
       // inGameTime = Mathf.Round(inGameTime += 0.3f * 10f) ;
        //inGameTime = Mathf.Round(inGameTime += 0.3f * 10f) ;
        SetTimeFlat(inGameTime + 36.5f);
        SettirednessFlat(tiredness - 50f);
    }

    public void SetTime(float time)
    {
       // Debug.Log(time);
       inGameTime = Mathf.Round((time + inGameTime) * 10.0f) * 0.1f;
       // Debug.Log(inGameTime);

        if (tiredness < 100)
        {
            tiredness = Mathf.Round((time + tiredness) * 10.0f) * 0.1f;
        }
        else
        {
            tiredness = 100;
        }
      // Debug.Log("InGameTime: " + inGameTime + " tiredness: " + tiredness);
    }

    public void SetTimeFlat(float timeSet)
    {
        inGameTime = Mathf.Round(timeSet * 10.0f) * 0.1f;
       // Debug.Log("InGameTime: " + inGameTime);
    }

    public void SettirednessFlat(float tirednessSet)
    {
        if(tirednessSet>100)
        {
            tirednessSet = 100;
        }
        tiredness = Mathf.Round(tirednessSet * 10.0f) * 0.1f;       
       // Debug.Log("Tiredness: " + tiredness);
    }

    public void StartListening(Story story, string speaker)
    {



        //Debug.Log("Przed: " + story.variablesState["speaker"]);
        currentstory = story;
        //przechowywujemy w 
        //currentstory = story;       
        //Debug.Log("Po: " + story.variablesState["time"]);

 
        //Musi to by� w tym miejscu gdy� wpisujemy w plik ink wcze�niej zapisene zmienne 
        VariablesToStory(story);

        //MUSZ� BY� PO  "VariablesToStory(story);"
        //ustawaimy imi� rozmowcy przez speaker
        story.variablesState["speaker"] = speaker;
        //ustawiamy czas jaki mamy w grze 
        story.variablesState["time"] = inGameTime;


        story.variablesState.variableChangedEvent += DialogueVariablesSet;

        
    }

    public void StopListening(Story story)
    {
        
        currentstory = story;
        story.variablesState.variableChangedEvent -= DialogueVariablesSet;
    }

    //Tu mamu aktualizacje naszego dictionary nowymi wartosciami 
    void DialogueVariablesSet(string name, Ink.Runtime.Object value)
    {

        //Debug.Log("Chaged: " + name + " = " + value);

        if (variables.ContainsKey(name))
        {

            //Debug.Log("variables dialog: " + variables["changeDialogueLine"]);

            //je�li zosta�a zmieniona wartos� timeline przypisana do NPC i warto�� changeDialogueLine jest true to aktu�alizujemy warto�� w naszej tablicy przypisanej do NPC. je�li wartos� jest inna ni� true to znzaczy,
            //�e mamy poprostu sprawdzi� czy ta warto�� zosta�a ju� wcze�niej zmieniona W ELSE IF
            if (dm.npcDialogueLine.ContainsKey(name) && variables["changeDialogueLine"].ToString() == "true")
            {
                //Debug.Log("Przed zmian�: " + name + " = " + dm.DialogueLineDownload(int.Parse(value.ToString()), name));


                dm.DialogueLineUpdate(int.Parse(value.ToString()), name);
                //Debug.Log(name + " indx: " + int.Parse(value.ToString()) + "-"+ dm.DialogueLineDownload(int.Parse(value.ToString()), name) + ": Zosta�a teraz odkryta i zmieniona");



                //Debug.Log("Po Zmianie: " + name + " = " + dm.DialogueLineDownload(int.Parse(value.ToString()), name));



                //odkryli�my i zauktoalizowali�my dialog line wi�c resetujemy zmienn�              
                //currentstory.variablesState["changeDialogueLine"] = "false";


                //Debug.Log("Po ZmianieJedenpo: " + name + " = " + dm.DialogueLineDownload(int.Parse(value.ToString()) + 1, name));
            }

            //Mamy tu dodatkowy warunek w psotaci && int.Parse(value.ToString())!=0 poniewa� po wyzerowaniu "currentstory.variablesState[name] = 0;" funkcja wywo�ywa�a by si� w niesko�czono��, gdy� wywo�uje si� po zmianie dowolnej zmiennej,
            //dm.DialogueLineDownload(int.Parse(value.ToString()), name)==false, a to moniewa� nei chcemy zerowa� warto�ci je�li ju� wcze�niej odpolowali�my ten dialogueline
            else if ( (dm.npcDialogueLine.ContainsKey(name) && int.Parse(value.ToString())!=0) && variables["changeDialogueLine"].ToString() == "false"&& dm.DialogueLineDownload(int.Parse(value.ToString()), name)==false)
            {            
                //Debug.Log(name + " indx: " + int.Parse(value.ToString()) + "-" + dm.DialogueLineDownload(int.Parse(value.ToString()), name) + ": Nie zosta�a odkrta, bo: "+  variables["changeDialogueLine"].ToString());
                currentstory.variablesState[name] = 0;
                //Debug.Log(name + ": "+ currentstory.variablesState[name] + " w currentstory : "+ currentstory.currentText);
                //Debug.Log("1");
            }

            else if(!dm.npcDialogueLine.ContainsKey(name) || name == "changeDialogueLine")
            {    

                variables.Remove(name);
                variables.Add(name, value);
                //Debug.Log(name + " " + value + " Zosta�a teraz zmieniona");

            }
            //Debug.Log(currentstory.variablesState["pokemon_name"] + " TUTAJ PACZ");
        }
    }

    //tu mamy aktualizacje zmiennych w pliku INK wykorzystuj�c Dictionary variables
    void VariablesToStory(Story story)
    {
      


        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            //If, dlatego, �e nie chcemy aoktualizowa� zmiennych odpowiedzialnych za dialogi z NPC, s� one zmieniane tylko w plikach i resetowane po kazdym wyj�ciu z dialogu. Zmiwena tej zmiennej w pliku 
            //jest r�wnoznaczna z testem sprawdzenia czy zosta�a odkryta 
             //if (!dm.npcDialogueLine.ContainsKey(variable.Key)  && variable.Key != "changeDialogueLine")
            //{
            //Debug.Log(variable.Key + " " + variable.Value + " Zosta�a nadpisana PACZ");
            story.variablesState.SetGlobal(variable.Key, variable.Value);
            //}
        }

    }

   
}
