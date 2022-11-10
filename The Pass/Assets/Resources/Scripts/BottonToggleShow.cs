using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottonToggleShow : MonoBehaviour
{

    ObservationStorage os;
    CameraController cc;
 
    
    // Start is called before the first frame update
    void Start()
    {
        os = GameObject.Find("Managers").GetComponent<ObservationStorage>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();

        ObservationButtonUpdate();
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    public void ObservationButtonUpdate()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();

        List<string> items = new List<string>();

        foreach (var ob in os.observation)
        {
            //IF poniewa¿ nie chcemy pokazywaæ w QE dowodów których nie zaczeliœmy badaæ 
            if(os.observationDownload(1, ob.Key) == true)
            items.Add(ob.Key);
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        dropdown.RefreshShownValue();
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
        //os.showObservationArray(dropdown.options[dropdown.value].text);



       
        //Debug.Log(dropdown.options[dropdown.value].text +" to "+ Resources.Load("Observations/V2Print.prefab"));


        //Jesli obiekt jest ju¿ na scenie, to nie tworzymy nowego 
        if (!GameObject.Find(dropdown.options[dropdown.value].text))
        {
            //tworzymy obiekt z folderu resources 
            GameObject.Instantiate((UnityEngine.Object)Resources.Load("Observations/" + dropdown.options[dropdown.value].text));
            //GameObject.Find(dropdown.options[dropdown.value].text).transform.SetPositionAndRotation = new Vector3();
            // GameObject.Find(dropdown.options[dropdown.value].text).transform.SetPositionAndRotation(cc.transform.position + cc.transform.forward * 2f, cc.transform.rotation);
            
            //ostawiamy obiekt blisko kamery
            GameObject.Find(dropdown.options[dropdown.value].text).transform.position = (cc.transform.position + cc.transform.forward * 1.5f);
            //obracamy sam model obiektu w kierunku kamery 
            GameObject.Find(dropdown.options[dropdown.value].text+ "/Item").transform.rotation = (cc.transform.rotation);
        }
        //Instantiate(Resources.Load("Observations/V2Print.prefab"), transform.position + transform.forward * 2f, transform.rotation);


        //aktywujemy skrypt ob.observationStudy(); dla danego dowodu, wiemy którego poniewa¿ znamy jego nazwe dropdown.options[dropdown.value].text
        Observation ob;
        ob = GameObject.Find(dropdown.options[dropdown.value].text).GetComponent<Observation>();



        //spogl¹danie na obserwacje
        //if, bo nie obracamy sie w kierunku dowody którego nie podnieœliœmy, bo nie jest "podnaszalny" to w else, patrzymy przed siebie
        if (ob.pickable)
        {
            cc.lookAt(GameObject.Find(dropdown.options[dropdown.value].text).transform.position);
        }
        else
        {
            
            cc.lookAt(cc.transform.position + cc.transform.forward * 1.5f);
        }



        //studiowanie obserwacji
        ob.observationStudy();

        //jest to funkcja wy³¹czaj¹ca edycje dowodu z eq
        ob.setInteractive();
    }

}
