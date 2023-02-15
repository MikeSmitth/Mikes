using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    CameraController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(" sellected: " + EventSystem.current.currentSelectedGameObject.transform.name);
        //ENTER kontynu³uje historyjkê 
       // Debug.Log("1");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("2");
            if (menu.activeSelf)
            {
                Time.timeScale = 1f;
                cc.isLook = false;
                cc.boxCollider(false);
                menu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                cc.isLook = true;
                cc.boxCollider(true);
                menu.SetActive(true);
            }
        }

    }
}
