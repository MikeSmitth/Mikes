using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    [Header("Monologue Mark")]
    [SerializeField] GameObject dialogueMark;

    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJSON;



    CameraController cc;
    DialogueMenager dm;

    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        dm = GameObject.Find("Managers").GetComponent<DialogueMenager>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((cc.hitTag().name == this.gameObject.name) && !dm.dialoguePlaying)
        {

            dialogueMark.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //w name ustalamy imiê rozmówcy i wchodzimy w tryb dialogu
                dm.EnterDialogueMode(inkJSON, cc.hitTag().name);
            }
        }
        else
        {
            dialogueMark.SetActive(false);
        }
    }


}
