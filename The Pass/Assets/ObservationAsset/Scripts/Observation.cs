using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Observation : MonoBehaviour
{

    public GameObject canvas;
    public GameObject panel;
    //public Animator scopesAnimator;
    int whatScope;
    ObservationStorage os;
    CameraController cc;
    BottonToggleShow bt;

    //Wa�ne!!! Zmiena sprawdzaj�ca czy badamy dow�d z ekwipunku, je�li tak to z regu�y nie mo�emy edytowa�. Jest ona sprawdzana w skrypcie ObservationButtons i edytowana w Observation.
    public bool fromEQ = false;


    //PRACUJE
    //obiekt jest "podnaszalny"
    public bool pickable = false;

    //urzywane do obrotu obiektu do pierwotnej pozycji, gdy go nie podnosimy
    private Quaternion startPos = Quaternion.Euler(0, 0, 0);

    // chyba nei potrzebne
    //public GameObject pickableObservation;
    //pobiera on model samego dowodu by m�c nim manipulowa� 
    public GameObject Item;

    private void Awake()
    {

        //Item  = GameObject.Find("Item");
        //po Instantiate w bottonTohhleShow np, wycinamy s�owo Clone z nowego obiektu
        transform.name = transform.name.Replace("(Clone)", "").Trim();
        startPos = this.transform.rotation;




        os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();       
        bt = GameObject.Find("Dropdown").GetComponent<BottonToggleShow>();       
    }





    void Start()
    {
        //urzywane wcze�niej ale nie dzia�a�o gdy usuwane by�y obeiekty z planszy bo by�y Pickable, teraz jest to w Awake 
       // os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
       // cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void Update()
    {

        //PRACUJE

        //jest zawsze false poniwa� gdy podnosimy objekt to nie musimy si� do niego wraca� by zobaczy� go w pe�nej krasie 
        if (pickable)
            fromEQ = false;






        //wychodzimy z trybu sprawdzania dowod�w, dodatkowo nie przyciskiem w grze. A canvas po to by nie uruchamia� funkcji Exit, niepotrzebnie we wszsytkich dodowach 
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse1)) && canvas.active)
        {
            buttonExit();
        }

    }

    //funkcja uruchamiaj�ca tryb badania obserwacji. 
    void OnMouseDown()
    {

        //sprawdzamy pod if czy dow�d jest w zasi�gu 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, cc.interactiveDistance) && hit.collider.tag == "Interactive")
        {
            //pozwalamy na edycje, je�li w tryb szukania dowod�w weszli�my ze sceny/ miejsca
            fromEQ = false;
            observationStudy();
        }



        //PRACUJE
        //obracamy sam model obiektu w kierunku kamery i przybli�amy, to samo co gdy wyjmujemy obiekt z ekwipunl w BottomToggleShow funkcja consoleShowEvidenceNoEdit(). NIe robimy tego gdy canvasy s� w��czone, bo z ekwipynku obiekt jest ju� obrucony
        if (pickable && !canvas.active)
        {
            StartCoroutine(softRotateAt(cc.transform.position, 0.3f));
        }


        //GameObject.Find("Item").transform.SetPositionAndRotation(cc.transform.position + cc.transform.forward * 2f, cc.transform.rotation);
        //Quaternion directiob = Quaternion.LookRotation( cc.transform.position - transform.position).normalized;
        //GameObject.Find("Item").transform.rotation = Quaternion.Slerp(transform.rotation, directiob, Time.deltaTime * 0.01f);



        //Debug.Log("Dzio�o?");
        //GameObject.Find("Item").transform.position = Vector3.MoveTowards(transform.position, cc.transform.position + cc.transform.forward * 2f, Time.deltaTime * 2);
    }

    //Updatejtuje dane 
    public void observationName(Button number)
    {
        //Debug.Log("Up "+ Convert.ToInt32(number.name)+" "+ name);
        os.observationUpdate(Convert.ToInt32(number.name), name);

        //Updatetujemy dane w przycisku EQ, je�li odkryli�my pierwszy kafelek dowodu, zostanie on dodany do EQ
        bt.ObservationButtonUpdate();
    }

    //Funkcja przycisku wychodz�cego z trybu obserwacji. Uruchamiana przyciskiem
    public void buttonExit()
    {
        //patrz na skrypt CameraController
        CameraController scopes = GameObject.Find("Main Camera").GetComponent<CameraController>();
        scopes.lookBack();

        //PRACUJE niszczymy obiekt po wy�ciu z obserwacji gdy jest do podniesienia. Canvas i sprawdzanie  pierwszego kafelka kt�ry w  ifie, poniewa�  w podoszonych dowodach jest w�a�nie podniesieniem. Nie chcemy nisczy� dowodu, na kt�ry nie patrzymy i niezbadali�my 
        if (pickable && canvas.active && os.observationDownload(1, transform.root.name) == true)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            //Je�li nie nieszczymy obiektu, to wracama na pozycje poczontkow� 
            StartCoroutine(softRotateBack(0.15f));
        }


        //StartCoroutine(waiterAnimator());
        canvas.SetActive(false);


        //Zmiena sprawdzaj�ca czy przygl�dasz si� obiektowi. Blokuje np poruszanie si� postaci
        cc.isLook = false;
        //scopesAnimator.SetInteger("whatScope", 0);
        //os.showObservationArray(transform.root.name);

        

    }

    //Funkcja przycisku wchodz�cego w tryp przygl�dania sie. Znika UI z kafelkami. Uruchamiana przyciskiem
    public void buttonPreview()
    {
        panel.SetActive(!panel.activeSelf);
    }
    //badanie observacji
    public void observationStudy()
    {

       
        //scopesAnimator.enabled = true;

        canvas.SetActive(true);
        //wywo�ujemy funkcje setButtonOff() dla przycisk�w z tagami ObservationButton
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ObservationButton");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ObservationButton>().setButtonOff();
        }
        // os.showObservationArray(transform.root.name);
        //.SetInteger("whatScope", 1);
        //Zmiena sprawdzaj�ca czy przygl�dasz si� obiektowi. Blokuje np poruszanie si� postaci

        cc.isLook = true;

    }


    public void setInteractive()
    {
        // nie pozwalamy na edycje, je�li w tryb szukania dowod�w weszli�my z ekwipunku
        fromEQ=true;
    }

    /*
    IEnumerator waiterAnimator()
    {
        yield return new WaitForSeconds(2f);
        scopesAnimator.enabled = false;
        //tu zr�b znikanie pojawania si� colider�w
    }
    */

    //wy��czamy interaktywnio��/mo�liwo�� edycji obserwacji, np podczas podgl�dania jej w ekwipunku



    //PRACUJE
    IEnumerator softRotateAt(Vector3 target, float inTime)
    {

        var fromAngle = Item.transform.rotation;
        var toAngle = Quaternion.LookRotation(target - transform.position);
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            Item.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        Item.transform.rotation = toAngle;
    }

    //urzywane do obrotu obiektu do pierwotnej pozycji, gdy go nie podnosimy
    IEnumerator softRotateBack(float inTime)
    {
        var fromAngle = Item.transform.rotation;
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            Item.transform.rotation = Quaternion.Slerp(fromAngle, startPos, t);
            yield return null;
        }
        Item.transform.rotation = startPos;
    }
}

      

    