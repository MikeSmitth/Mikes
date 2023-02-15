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

    //w global menad¿erze obs³ugujemy globalny plik z zmeinnymi od dialogów
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

 
        //Musi to byæ w tym miejscu gdy¿ wpisujemy w plik ink wczeœniej zapisene zmienne 
        VariablesToStory(story);

        //MUSZ¥ BYÆ PO  "VariablesToStory(story);"
        //ustawaimy imiê rozmowcy przez speaker
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

            //jeœli zosta³a zmieniona wartosæ timeline przypisana do NPC i wartoœæ changeDialogueLine jest true to aktu³alizujemy wartoœæ w naszej tablicy przypisanej do NPC. jeœli wartosæ jest inna niæ true to znzaczy,
            //¿e mamy poprostu sprawdziæ czy ta wartoœæ zosta³a ju¿ wczeœniej zmieniona W ELSE IF
            if (dm.npcDialogueLine.ContainsKey(name) && variables["changeDialogueLine"].ToString() == "true")
            {
                //Debug.Log("Przed zmian¹: " + name + " = " + dm.DialogueLineDownload(int.Parse(value.ToString()), name));


                dm.DialogueLineUpdate(int.Parse(value.ToString()), name);
                //Debug.Log(name + " indx: " + int.Parse(value.ToString()) + "-"+ dm.DialogueLineDownload(int.Parse(value.ToString()), name) + ": Zosta³a teraz odkryta i zmieniona");



                //Debug.Log("Po Zmianie: " + name + " = " + dm.DialogueLineDownload(int.Parse(value.ToString()), name));



                //odkryliœmy i zauktoalizowaliœmy dialog line wiêc resetujemy zmienn¹              
                //currentstory.variablesState["changeDialogueLine"] = "false";


                //Debug.Log("Po ZmianieJedenpo: " + name + " = " + dm.DialogueLineDownload(int.Parse(value.ToString()) + 1, name));
            }

            //Mamy tu dodatkowy warunek w psotaci && int.Parse(value.ToString())!=0 poniewa¿ po wyzerowaniu "currentstory.variablesState[name] = 0;" funkcja wywo³ywa³a by siê w nieskoñczonoœæ, gdy¿ wywo³uje siê po zmianie dowolnej zmiennej,
            //dm.DialogueLineDownload(int.Parse(value.ToString()), name)==false, a to moniewa¿ nei chcemy zerowaæ wartoœci jeœli ju¿ wczeœniej odpolowaliœmy ten dialogueline
            else if ( (dm.npcDialogueLine.ContainsKey(name) && int.Parse(value.ToString())!=0) && variables["changeDialogueLine"].ToString() == "false"&& dm.DialogueLineDownload(int.Parse(value.ToString()), name)==false)
            {            
                //Debug.Log(name + " indx: " + int.Parse(value.ToString()) + "-" + dm.DialogueLineDownload(int.Parse(value.ToString()), name) + ": Nie zosta³a odkrta, bo: "+  variables["changeDialogueLine"].ToString());
                currentstory.variablesState[name] = 0;
                //Debug.Log(name + ": "+ currentstory.variablesState[name] + " w currentstory : "+ currentstory.currentText);
                //Debug.Log("1");
            }

            else if(!dm.npcDialogueLine.ContainsKey(name) || name == "changeDialogueLine")
            {    

                variables.Remove(name);
                variables.Add(name, value);
                //Debug.Log(name + " " + value + " Zosta³a teraz zmieniona");

            }
            //Debug.Log(currentstory.variablesState["pokemon_name"] + " TUTAJ PACZ");
        }
    }

    //tu mamy aktualizacje zmiennych w pliku INK wykorzystuj¹c Dictionary variables
    void VariablesToStory(Story story)
    {
      


        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            //If, dlatego, ¿e nie chcemy aoktualizowaæ zmiennych odpowiedzialnych za dialogi z NPC, s¹ one zmieniane tylko w plikach i resetowane po kazdym wyjœciu z dialogu. Zmiwena tej zmiennej w pliku 
            //jest równoznaczna z testem sprawdzenia czy zosta³a odkryta 
             //if (!dm.npcDialogueLine.ContainsKey(variable.Key)  && variable.Key != "changeDialogueLine")
            //{
            //Debug.Log(variable.Key + " " + variable.Value + " Zosta³a nadpisana PACZ");
            story.variablesState.SetGlobal(variable.Key, variable.Value);
            //}
        }

    }

   
}
