using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadOnTriggerOrDialogueLine : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] string SceneName;

    [Header("DialogueLine Name")]
    [SerializeField] string[] nameList;

    [Header("DialogueLine Index")]
    [SerializeField] int[] indexList;
    // Start is called before the first frame update
    DialogueMenager dm;
    void Start()
    {
        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player")
        {
            Load();
            //Debug.Log(timeToStop + " : " + gm.inGameTime);
        }

    }
    // Update is called once per frame
    void Update()
    {
        foreach (string name in nameList)
        {
            int i = 0;
                //Debug.Log("Ile?: " data.observationToSave.ContainsKey(i.ToString() + ":" + observationName) +" : "+ data.observationToSave.ContainsValue(observationDownload(i, observationName);
                if (dm.npcDialogueLine.ContainsKey(name) && dm.DialogueLineDownload(indexList[i], name))
                {
                Debug.Log(dm.DialogueLineDownload(indexList[i], name));
                Load();
                }
            i++;
        }
    }
    private void Load()
    {
        EventManager.current.SaveGame();
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
