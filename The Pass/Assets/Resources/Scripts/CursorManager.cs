using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorTexturesIdle;
    [SerializeField] Texture2D cursorTexturesInteractive;
    CameraController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        if (cursorTexturesIdle != null)
        {
            Cursor.SetCursor(cursorTexturesIdle, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (cc.hitTag().tag == "MonologueTrigger" || cc.hitTag().tag == "Interactive" || cc.hitTag().tag == "NPC")
        {
            //Debug.Log("Interactive");
            Cursor.SetCursor(cursorTexturesInteractive, Vector2.zero, CursorMode.ForceSoftware);
        }
       else
        {
            Cursor.SetCursor(cursorTexturesIdle, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
