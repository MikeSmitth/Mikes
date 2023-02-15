using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJSON;

    // Start is called before the first frame update
    DialogueMenager dm;
    void Start()
    {
        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("sema1");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("sema2");
        if (other.tag == "Player")
        {
            dm.EnterDialogueMode(inkJSON, name);
            Debug.Log("sema3");
        }
    }
}
