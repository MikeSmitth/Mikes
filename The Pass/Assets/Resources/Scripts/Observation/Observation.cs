using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Observation : MonoBehaviour
{
    [Header("Time To Destroy")]
    [SerializeField] float timeToDestroy = 0;

    //Jeœli observacja jest 
    [Header("Observation Image Panel")]
    [SerializeField] GameObject ObservationImagePanel;


    public GameObject canvas;
    public GameObject panel;
    GameObject GameUI;
    //public Animator scopesAnimator;
    int whatScope;

    ObservationStorage os;
    CameraController cc;
    BottonToggleShow bt;
    GlobalManager gm;


    //Wa¿ne!!! Zmiena sprawdzaj¹ca czy badamy dowód z ekwipunku, jeœli tak to z regu³y nie mo¿emy edytowaæ. Jest ona sprawdzana w skrypcie ObservationButtons i edytowana w Observation.
    public bool fromEQ = false;
    //Zmiena plikuj¹ca przyblizanie siê dowodu do kamery, by nie by³a sytuacji w której szybko klikamy interakcje i z niej wychodzimy, a animacja siê blokuje 
    bool brakeSoftRotateAt = false;
    //czy przedmiot mo¿emy przygl¹daj¹c sie obracaæ 
    public bool obracable = true;
    //obiekt jest "podnaszalny"
    public bool pickable = false;


    //urzywane do obrotu obiektu do pierwotnej pozycji, gdy go nie podnosimy
    private Quaternion startRot = Quaternion.Euler(0, 0, 0);
    private Vector3 startPos;


    // chyba nei potrzebne
    //public GameObject pickableObservation;
    //pobiera on model samego dowodu by móc nim manipulowaæ 
    public GameObject Item;




    private void Awake()
    {
        // jeœli mamy obrazek obserwacji to automatycznie ustawiamy dowód jako nie do podniesienia czy obracania
        if(ObservationImagePanel)
        {
            pickable = false;
            obracable = false;
        }


        //Item  = GameObject.Find("Item");
        //po Instantiate w bottonTohhleShow np, wycinamy s³owo Clone z nowego obiektu
        transform.name = transform.name.Replace("(Clone)", "").Trim();
        startRot = this.transform.rotation;
        startPos = this.transform.position;      


        GameUI = GameObject.FindGameObjectWithTag("GameUI");
        os = GameObject.Find("Managers").GetComponent<ObservationStorage>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();       
        bt = GameObject.Find("Dropdown").GetComponent<BottonToggleShow>();
        gm = GameObject.Find("Managers").GetComponent<GlobalManager>();
    }





    void Start()
    {
        //urzywane wczeœniej ale nie dzia³a³o gdy usuwane by³y obeiekty z planszy bo by³y Pickable, teraz jest to w Awake 
       // os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
       // cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void Update()
    {

        //Niszczymy dowód jeœli up³uno³ wyznaczony czas w grze. !cc.isLook poniewa¿ nie chcemy niszczyæ dowodu w czasie ogl¹dania 
        if (timeToDestroy > 0 && gm.inGameTime >= timeToDestroy && !cc.isLook)
        {
            Destroy(this.gameObject);
        }
        



        var mousePosition = Input.mousePosition;

        float rotateForce = 40;
        //sprawdzamy pod if czy dowód jest w zasiêgu i mo¿emy go pbracaæ podczas przegl¹dania 

        if (canvas.activeSelf && Input.GetKey(KeyCode.Mouse0) && (cc.hitTag().tag == "Interactive") && obracable)
        {
           //tutaj dajemy mo¿liwosæ obracania obiekty gdy go przegl¹damy 
            var toAngle = Quaternion.Euler(Item.transform.eulerAngles + new Vector3(0/*mousePosition.y / (Screen.width / 2) * rotateForce * Input.GetAxis("Mouse Y") * -1*/, mousePosition.x / (Screen.width / 2) * rotateForce * Input.GetAxis("Mouse X") *-1, 0));
            Item.transform.rotation = Quaternion.Slerp(Item.transform.rotation, toAngle, Time.deltaTime * 50f);

        }
        //jest zawsze false poniwa¿ gdy podnosimy objekt to nie musimy siê do niego wracaæ by zobaczyæ go w pe³nej krasie 
        if (pickable)
        {
            fromEQ = false;
        }






        //wychodzimy z trybu sprawdzania dowodów, dodatkowo nie przyciskiem w grze. A canvas po to by nie uruchamiaæ funkcji Exit, niepotrzebnie we wszsytkich dodowach 
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse1)) && canvas.activeSelf)
        {
            buttonExit();
        }

    }

    //funkcja uruchamiaj¹ca tryb badania obserwacji. kolejnoœæ wywo³ywania funkcji w OnMouseDown() jest wa¿na by zmienna lookat siê zgadza³a
    void OnMouseDown()
    {

        //sprawdzamy pod if czy dowód jest w zasiêgu 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // w ifie jest cc.isLook==false, by nie mo¿na by³o wejœæ w przegl¹danie inego dowodu, kedy jeden ju¿ przegl¹damy 
        if (Physics.Raycast(ray, out hit, cc.interactiveDistance) && hit.collider.tag == "Interactive" && cc.isLook==false)
        {

            //obracamy sam model obiektu w kierunku kamery i przybli¿amy, to samo co gdy wyjmujemy obiekt z ekwipunl w BottomToggleShow funkcja consoleShowEvidenceNoEdit(). NIe robimy tego gdy canvasy s¹ w³¹czone, bo z ekwipynku obiekt jest ju¿ obrócony. kolejnoœæ wywo³ywania funkcji w OnMouseDown() jest wa¿na by zmienna lookat siê zgadza³a
            if (obracable && !canvas.activeSelf)
            {
                StartCoroutine(softRotateAt(cc.transform.position, 0.7f));
            }

            //pozwalamy na edycje, jeœli w tryb szukania dowodów weszliœmy ze sceny/ miejsca
            fromEQ = false;
            observationStudy();

            //odpalamy animacje przygl¹dania sie dowodowi, kolejnoœæ wywo³ywania funkcji w OnMouseDown() jest wa¿na by zmienna lookat siê zgadza³a
            cc.lookAt(hit.transform.position);
        }      


        //GameObject.Find("Item").transform.SetPositionAndRotation(cc.transform.position + cc.transform.forward * 2f, cc.transform.rotation);
        //Quaternion directiob = Quaternion.LookRotation( cc.transform.position - transform.position).normalized;
        //GameObject.Find("Item").transform.rotation = Quaternion.Slerp(transform.rotation, directiob, Time.deltaTime * 0.01f);



        //Debug.Log("Dzio³o?");
        //GameObject.Find("Item").transform.position = Vector3.MoveTowards(transform.position, cc.transform.position + cc.transform.forward * 2f, Time.deltaTime * 2);
    }

    //Updatejtuje dane wywo³ywane naciœniêciem 
    public void observationName(Button number)
    {
        //Debug.Log("Up "+ Convert.ToInt32(number.name)+" "+ name);

        //DODAJEMY CZAS PRZY ZBADANEJ OBSERVACJI!!!wraz z zabezpieczeniem, przed wielokrotym nabijaniem czasu przy klikaniu w odkryty przycisk i nie mo¿na dodaæ czasu,
        //jeœli obserwacja zosta³a zaktualizowana inn¹ drog¹, by nie naliczaæ niepotrzebnie czasu
        if (!os.observationDownload(Convert.ToInt32(number.name), name))
        {
            gm.SetTime(number.GetComponent<ObservationButton>().timeNeeded);
        }


        os.observationUpdate(Convert.ToInt32(number.name), name);

        //Updatetujemy dane w przycisku EQ, jeœli odkryliœmy pierwszy kafelek dowodu, zostanie on dodany do EQ
        bt.ObservationButtonUpdate();
    }

    //Funkcja przycisku wychodz¹cego z trybu obserwacji. Uruchamiana przyciskiem
    public void buttonExit()
    {
        //patrz na skrypt CameraController
        CameraController scopes = GameObject.Find("Main Camera").GetComponent<CameraController>();
        scopes.lookBack();

        //niszczymy obiekt po wyœciu z obserwacji gdy jest do podniesienia. Canvas i sprawdzanie  pierwszego kafelka który w  ifie, poniewa¿  w podoszonych dowodach jest w³aœnie podniesieniem. Nie chcemy nisczyæ dowodu, na który nie patrzymy i niezbadaliœmy 
        if (pickable && canvas.activeSelf && os.observationDownload(1, transform.root.name) == true)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            //Jeœli nie nieszczymy obiektu, to wracama na pozycje poczontkow¹ 
            StartCoroutine(softRotateBack(0.4f));
        }


        //StartCoroutine(waiterAnimator());
        canvas.SetActive(false);

        //Zmiena sprawdzaj¹ca czy przygl¹dasz siê obiektowi. Blokuje np poruszanie siê postaci
        cc.isLook = false;
        //scopesAnimator.SetInteger("whatScope", 0);
        //os.showObservationArray(transform.root.name);

        //Wy³¹czamy boxcollider, by mo¿na by³o wchodziæ w iterakcje z otoczeniem gdy nie przegl¹damy dowod
        cc.boxCollider(false);

        //w³¹czamy UI gry gdy jest wy³¹czone UI dowodu
        GameUI.SetActive(true);
    }

    //Funkcja przycisku wchodz¹cego w tryp przygl¹dania sie. Znika UI z kafelkami. Uruchamiana przyciskiem
    public void buttonPreview()
    {
        panel.SetActive(!panel.activeSelf);

        //Wy³¹czamy lub w³¹czamy boxcollider, by mo¿na by³o wchodziæ w iterakcje z otoczeniem gdy nie przegl¹damy dowodu
        cc.boxCollider(panel.activeSelf);

    }


    //badanie observacji
    public void observationStudy()
    {


        //scopesAnimator.enabled = true;


        //W³¹czamy boccollider jeœli siê mu zaczynamy przygl¹dæ, by nie w³¹cza³ siê gdy np ju¿ siê przygl¹damy ale chcielibyœmy go obracaæ na preview, by nie mo¿na by³o wchodziæ w iterakcje z otoczeniem podczas przêgl¹dania dowodu
        if(cc.isLook==false)
        {
           cc.boxCollider(true);
        }

        canvas.SetActive(true);


        //wywo³ujemy funkcje setButtonOff() dla przycisków z tagami ObservationButton
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ObservationButton");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ObservationButton>().setButtonOff();
        }
        // os.showObservationArray(transform.root.name);
        //.SetInteger("whatScope", 1);
        //Zmiena sprawdzaj¹ca czy przygl¹dasz siê obiektowi. Blokuje np poruszanie siê postaci


        //Zawsze ma byæ pnel z dowodami w³¹czony, gdy uruchamiamy z nim interakcje, nawet gdy wczeœniej wy³¹czyliœmy go przyciskiem prewiev
        panel.SetActive(true);

        //wy³¹czamy UI gry gdy jest w³¹czone UI dowodu
        GameUI.SetActive(false);

        cc.isLook = true;

    }


    public void setInteractive()
    {
        // nie pozwalamy na edycje, jeœli w tryb szukania dowodów weszliœmy z ekwipunku
        fromEQ=true;
    }

    /*
    IEnumerator waiterAnimator()
    {
        yield return new WaitForSeconds(2f);
        scopesAnimator.enabled = false;
        //tu zrób znikanie pojawania siê coliderów
    }
    */

    //wy³¹czamy interaktywnioœæ/mo¿liwoœæ edycji obserwacji, np podczas podgl¹dania jej w ekwipunku




    IEnumerator softRotateAt(Vector3 target, float inTime)
    {
        //Zmiena plikuj¹ca przyblizanie siê dowodu do kamery, by nie by³a sytuacji w której szybko klikamy interakcje i z niej wychodzimy, a animacja siê blokuje 
        brakeSoftRotateAt = false;

        var fromAngle = Item.transform.rotation;
        var toAngle = Quaternion.LookRotation(target - transform.position);
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            if (!brakeSoftRotateAt)
            {
                Item.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                //Item.transform.position = Quaternion.Slerp(fromAngle, toAngle, t);
                //zmieniamy pozycje samej obserwacji, nie samego Item, by boxCollider by³ w miejscu gdzie znajduje sie model obserwacji
                transform.position = Vector3.Lerp(transform.position, cc.transform.position + cc.transform.forward * 1.5f, t / (inTime*10) );
                yield return null;
            }
        }
        //po pêtli ustawiamy sztywno pozycje by nie by³a np o 0.0001 zminiona 
        Item.transform.rotation = toAngle;
        //transform.position = (cc.transform.position + cc.transform.forward * 1.5f);
    }

    //urzywane do obrotu obiektu do pierwotnej pozycji, gdy go nie podnosimy
    IEnumerator softRotateBack(float inTime)
    {
        //Zmiena plikuj¹ca przyblizanie siê dowodu do kamery, by nie by³a sytuacji w której szybko klikamy interakcje i z niej wychodzimy, a animacja siê blokuje 
        brakeSoftRotateAt = true;

        var fromAngle = Item.transform.rotation;
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            Item.transform.rotation = Quaternion.Slerp(fromAngle, startRot, t);
            //zmieniamy pozycje samej obserwacji, nie samego Item, by boxCollider by³ w miejscu gdzie znajduje sie model obserwacji
            transform.position = Vector3.Lerp(transform.position, startPos, t);
            yield return null;
        }
        //po pêtli ustawiamy sztywno pozycje by nie by³a np o 0.0001 zminiona 
        Item.transform.rotation = startRot;
        transform.position = startPos;
    }
}

      

    