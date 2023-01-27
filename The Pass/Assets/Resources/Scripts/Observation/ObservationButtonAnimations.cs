using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ObservationButtonAnimations : MonoBehaviour
{
    private Animator pinAnimator;


    ObservationStorage os;
    SoundsManager sm;
    Observation ob;

    // Start is called before the first frame update
    void Start()
    {
        os = GameObject.Find("Managers").GetComponent<ObservationStorage>();
        sm = GameObject.Find("Managers").GetComponent<SoundsManager>();
        pinAnimator = GetComponent<Animator>();
        ob = transform.root.GetComponent<Observation>();

        //ColorBlock cb = GetComponent<Button>().colors;
        //cb.normalColor = Color.red;
        //GetComponent<Button>().Color = Color.red;
    } 
        void OnEnable()
        {
       


        }   
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.root.name);
        if (os.observationDownload((Convert.ToInt32(name)), transform.root.name))
        {
            //Debug.Log("ObservationPin");
            pinAnimator.Play("ObservationPin");

            if (!pinAnimator.GetCurrentAnimatorStateInfo(0).IsName("ObservationPin"))
                sm.PlaySoundClip(ob.PinSound);
        }
        //Debug.Log(" Kurwa ");
        // Debug.Log((Convert.ToInt32(name)) + " : " + transform.root.name);
    }
}
