using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Mark")]
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if ((Physics.Raycast(ray, out hit, cc.interactiveDistance) && hit.collider.tag == "NPC") && !dm.dialoguePlaying)
        {
            dialogueMark.SetActive(true);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                dm.EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            dialogueMark.SetActive(false);
        }
    }
}
