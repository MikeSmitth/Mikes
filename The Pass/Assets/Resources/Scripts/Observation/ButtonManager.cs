using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("Unlock audioClip")]   
    [SerializeField] AudioClip unlockClip;
    [Header("Observation or DialogueLine Needed To Show and Sound")]   
    [SerializeField] string[] observationOrDialogueNames;
    [SerializeField] int[] observationOrDialogueIndexes;



   
    ObservationStorage os;
    DialogueMenager dm;
    SoundsManager sm;


    // Start is called before the first frame update
    void Start()
    {
        os = GameObject.Find("Managers").GetComponent<ObservationStorage>();
        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
        sm = GameObject.Find("Managers").GetComponent<SoundsManager>();
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    public void UpdateButtonSound(string updatedName, int updatedIndex)
    {
        int i = 0;
        if (observationOrDialogueNames.Length == observationOrDialogueIndexes.Length)
        {
           
            //Debug.Log("if");
            foreach (string name in observationOrDialogueNames)
            {


                if (os.observation.ContainsKey(name))
                {
                    //&& updatedName == name && updatedIndex == observationOrDialogueIndexes[i] poniewaz chcemy udpalaæ dŸwiêk tylko raz gdy odpowiednia obserwacja jest odkrywana 
                    if (os.observationDownload(observationOrDialogueIndexes[i], name) == true && updatedName == name && updatedIndex == observationOrDialogueIndexes[i])
                    {
                        //Debug.Log("os.observationDownload: " + name + " o indexie " + observationOrDialogueIndexes[i] + " :  " + os.observationDownload(observationOrDialogueIndexes[i], name) + " == true");
                        sm.PlaySoundClip(unlockClip);
                        //Debug.Log(" GROMY");
                    }


                }
                else if (dm.npcDialogueLine.ContainsKey(name))
                {
                    //&& updatedName == name && updatedIndex == observationOrDialogueIndexes[i] poniewaz chcemy udpalaæ dŸwiêk tylko raz gdy odpowiednia obserwacja jest odkrywana 
                    if (dm.DialogueLineDownload(observationOrDialogueIndexes[i], name) == true && updatedName == name && updatedIndex == observationOrDialogueIndexes[i])
                    {
                       // Debug.Log("dm.DialogueLineDownload: " + name + " o indexie " + i + " :  " + os.observationDownload(observationOrDialogueIndexes[i], name) + " == true");
                        sm.PlaySoundClip(unlockClip);
                       // Debug.Log(" GROMY");
                    }

                }
                i++;
            }
        }
        else
        {
            Debug.LogError(" Nie mo¿na odczytaæ oceswacji. D³ugoœæ listy obserwacji: " + observationOrDialogueNames.Length + " Nie odpowiada d³ugosci listy idexów: " + observationOrDialogueIndexes.Length);
        }
    }
}
