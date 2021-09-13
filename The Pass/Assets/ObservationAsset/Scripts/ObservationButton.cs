using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ObservationButton : MonoBehaviour
{
    public int time=1;
    public GameObject observationName;
    ObservationStorage os;
    //GameObject[] arrows;
   
    void Start()
    {
        //arrows = GameObject.FindGameObjectsWithTag("Arrow");
        // Debug.Log(name+" arrow: " + arrows.Length);
        
       
        
    }
    public void setButtonOff()
    {
        //Debug.Log(transform.root.name + " :name ");
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
        if ((os.observationDownload((Convert.ToInt32(name)), transform.root.name)== false)&&(Convert.ToInt32(name)-1)!=0)
        {
            gameObject.SetActive(false);
        } 
        
        else if ((os.observationDownload((Convert.ToInt32(name)), transform.root.name) == true))
        {
            //Debug.Log(name+" clicked ");
            GetComponent<Button>().onClick.Invoke();
            GetComponent<Image>().color = new Color32(212, 212, 212, 255);
            showArrows();
        }
        
        if (os.observationDownload((Convert.ToInt32(name)), transform.root.name) == false)
        {
            //Debug.Log(name + " hide ");
            hideArrows();
        }

        //os.showObservationArray(transform.root.name);
    }
    public void setButton()
    {
       // Debug.Log("11.5: " + arrows.Length);
        //hideArrows();
        gameObject.SetActive(true);
    }

    public void showArrows()
    {
         //Debug.Log("Show "+ name);
        foreach (Transform child in transform)
        {
            if (child.tag == "Arrow")
                //Debug.Log("arrow " + child.name);
            child.gameObject.SetActive(true);
        }
    }
    public void hideArrows()
    {
        //Debug.Log("1.5: " + arrows.Length);
        foreach (Transform child in transform)
        {
            if (child.tag == "Arrow")
                child.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
}
