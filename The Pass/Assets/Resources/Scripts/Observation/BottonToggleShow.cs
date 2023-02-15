using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        gameObject.SetActive(false);
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    

    public void ObservationButtonUpdate()
    {
        var dropdown = transform.GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();

        List<string> items = new List<string>();

        foreach (var ob in os.observation)
        {
            //IF poniewa� nie chcemy pokazywa� w QE dowod�w kt�rych nie zaczeli�my bada� 
            //Debug.Log(" Index: 1 "+os.observationDownload(1, ob.Key) + " <--- Bool  Ob.Key ---> "+ ob.Key);
            if (os.observationDownload(1, ob.Key) == true)
            items.Add(ob.Key);
        }

        foreach (var item in items)
        {
            //Debug.Log(item);
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item }); // Add(new Dropdown.OptionData() { text = item });
        }
        dropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
  
        
    }
    public void toggleBottonShow()
    {
        if (gameObject.activeSelf)
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


    //pokazuje dow�d z pami�ci ekwipunku, nie masz mo�liwo�ci edyzji 
    public void consoleShowEvidenceNoEdit()
    {
        Observation ob;


        var dropdown = transform.GetComponent<TMP_Dropdown>();


        //os.showObservationArray(dropdown.options[dropdown.value].text);




        //Debug.Log(dropdown.options[dropdown.value].text +" to "+ Resources.Load("Observations/V2Print.prefab"));


        //Jesli obiekt jest ju� na scenie, to nie tworzymy nowego 


        if (dropdown.options.Count <= 0)
        {
            Debug.LogWarning("ObservationArray = 0");
            return;
        }

       


        if (!GameObject.Find(dropdown.options[dropdown.value].text))
        {
                    
            //tworzymy obiekt z folderu resources 
            GameObject.Instantiate((UnityEngine.Object)Resources.Load("Prefabs/Observations/" + dropdown.options[dropdown.value].text));
            //GameObject.Find(dropdown.options[dropdown.value].text).transform.SetPositionAndRotation = new Vector3();
            // GameObject.Find(dropdown.options[dropdown.value].text).transform.SetPositionAndRotation(cc.transform.position + cc.transform.forward * 2f, cc.transform.rotation);
            
            //ostawiamy obiekt blisko kamery
            GameObject.Find(dropdown.options[dropdown.value].text).transform.position = (cc.transform.position + cc.transform.forward * 1.5f);
            //obracamy sam model obiektu w kierunku kamery 
            GameObject.Find(dropdown.options[dropdown.value].text+ "/Item").transform.rotation = (cc.transform.rotation);

            //zmeinna wykorzytywana do wy��czania obiektu gdy go ztowrzyli�my tutaj. Przydatne po to by nie tworzy� na mapie obserwacji kt�re mog� przeszkadza� 
            ob = GameObject.Find(dropdown.options[dropdown.value].text).GetComponent<Observation>();
            ob.setSpawned();
        }
        //Instantiate(Resources.Load("Observations/V2Print.prefab"), transform.position + transform.forward * 2f, transform.rotation);


        //aktywujemy skrypt ob.observationStudy(); dla danego dowodu, wiemy kt�rego poniewa� znamy jego nazwe dropdown.options[dropdown.value].text
        ob = GameObject.Find(dropdown.options[dropdown.value].text).GetComponent<Observation>();
        

        //Debug.LogWarning(dropdown.options[dropdown.value].text);

        //jest to funkcja wy��czaj�ca edycje dowodu z eq
        ob.setInteractive();

        //studiowanie obserwacji
        ob.observationStudy();

        

        //spogl�danie na obserwacje
        //if, bo nie obracamy sie w kierunku dowody kt�rego nie podnie�li�my, bo nie jest "podnaszalny" to w else, patrzymy przed siebie
        if (ob.pickable)
        {
            cc.lookAt(GameObject.Find(dropdown.options[dropdown.value].text).transform.position);
        }
        else
        {
            
            cc.lookAt(cc.transform.position + cc.transform.forward * 1.5f);
        }

       


    }

}
