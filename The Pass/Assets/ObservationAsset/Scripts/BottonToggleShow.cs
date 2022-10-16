using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottonToggleShow : MonoBehaviour
{

    ObservationStorage os;
    
    // Start is called before the first frame update
    void Start()
    {
        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();

        List<string> items = new List<string>();

        foreach (var ob in os.observation)
        {
            items.Add(ob.Key);
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        dropdown.RefreshShownValue();
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    public void toggleBottonShow()
    {
        if (gameObject.active)
        {
            gameObject.SetActive(false);
        }
        else
        {     
            gameObject.SetActive(true);      
        }
        //consoleShowEvidence();

    }

    public void consoleShowAllEvidence()
    {
        foreach (var ob in os.observation)
        {
            //Debug.Log(ob.Key);
            os.showObservationArray(ob.Key);
        }
    }


    //pokazuje dowód z pamiêci ekwipunku, nie masz mo¿liwoœci edyzji 
    public void consoleShowEvidenceNoEdit()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        //Debug.Log(dropdown.options[dropdown.value].text);
        os.showObservationArray(dropdown.options[dropdown.value].text);

        //aktywujemy skrypt ob.observationStudy(); dla danego dowodu, wiemy którego poniewa¿ znamy jego nazwe dropdown.options[dropdown.value].text
        Observation ob;
        ob = GameObject.Find(dropdown.options[dropdown.value].text).GetComponent<Observation>();

        ob.observationStudy();
        //jest to funkcja wy³¹czaj¹ca edycje dowodu z eq
        ob.setInteractive();
    }

}
