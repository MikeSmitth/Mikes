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

    //Je�li observacja jest 
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


    //Wa�ne!!! Zmiena sprawdzaj�ca czy badamy dow�d z ekwipunku, je�li tak to z regu�y nie mo�emy edytowa�. Jest ona sprawdzana w skrypcie ObservationButtons i edytowana w Observation.
    public bool fromEQ = false;
    //Zmiena plikuj�ca przyblizanie si� dowodu do kamery, by nie by�a sytuacji w kt�rej szybko klikamy interakcje i z niej wychodzimy, a animacja si� blokuje 
    bool brakeSoftRotateAt = false;
    //czy przedmiot mo�emy przygl�daj�c sie obraca� 
    public bool obracable = true;
    //obiekt jest "podnaszalny"
    public bool pickable = false;


    //urzywane do obrotu obiektu do pierwotnej pozycji, gdy go nie podnosimy
    private Quaternion startRot = Quaternion.Euler(0, 0, 0);
    private Vector3 startPos;


    // chyba nei potrzebne
    //public GameObject pickableObservation;
    //pobiera on model samego dowodu by m�c nim manipulowa� 
    public GameObject Item;




    private void Awake()
    {
        // je�li mamy obrazek obserwacji to automatycznie ustawiamy dow�d jako nie do podniesienia czy obracania
        if(ObservationImagePanel)
        {
            pickable = false;
            obracable = false;
        }


        //Item  = GameObject.Find("Item");
        //po Instantiate w bottonTohhleShow np, wycinamy s�owo Clone z nowego obiektu
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
        //urzywane wcze�niej ale nie dzia�a�o gdy usuwane by�y obeiekty z planszy bo by�y Pickable, teraz jest to w Awake 
       // os = GameObject.Find("Main Camera").GetComponent<ObservationStorage>();
       // cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void Update()
    {

        //Niszczymy dow�d je�li up�uno� wyznaczony czas w grze. !cc.isLook poniewa� nie chcemy niszczy� dowodu w czasie ogl�dania 
        if (timeToDestroy > 0 && gm.inGameTime >= timeToDestroy && !cc.isLook)
        {
            Destroy(this.gameObject);
        }
        



        var mousePosition = Input.mousePosition;

        float rotateForce = 40;
        //sprawdzamy pod if czy dow�d jest w zasi�gu i mo�emy go pbraca� podczas przegl�dania 

        if (canvas.activeSelf && Input.GetKey(KeyCode.Mouse0) && (cc.hitTag().tag == "Interactive") && obracable)
        {
           //tutaj dajemy mo�liwos� obracania obiekty gdy go przegl�damy 
            var toAngle = Quaternion.Euler(Item.transform.eulerAngles + new Vector3(0/*mousePosition.y / (Screen.width / 2) * rotateForce * Input.GetAxis("Mouse Y") * -1*/, mousePosition.x / (Screen.width / 2) * rotateForce * Input.GetAxis("Mouse X") *-1, 0));
            Item.transform.rotation = Quaternion.Slerp(Item.transform.rotation, toAngle, Time.deltaTime * 50f);

        }
        //jest zawsze false poniwa� gdy podnosimy objekt to nie musimy si� do niego wraca� by zobaczy� go w pe�nej krasie 
        if (pickable)
        {
            fromEQ = false;
        }






        //wychodzimy z trybu sprawdzania dowod�w, dodatkowo nie przyciskiem w grze. A canvas po to by nie uruchamia� funkcji Exit, niepotrzebnie we wszsytkich dodowach 
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse1)) && canvas.activeSelf)
        {
            buttonExit();
        }

    }

    //funkcja uruchamiaj�ca tryb badania obserwacji. kolejno�� wywo�ywania funkcji w OnMouseDown() jest wa�na by zmienna lookat si� zgadza�a
    void OnMouseDown()
    {

        //sprawdzamy pod if czy dow�d jest w zasi�gu 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // w ifie jest cc.isLook==false, by nie mo�na by�o wej�� w przegl�danie inego dowodu, kedy jeden ju� przegl�damy 
        if (Physics.Raycast(ray, out hit, cc.interactiveDistance) && hit.collider.tag == "Interactive" && cc.isLook==false)
        {

            //obracamy sam model obiektu w kierunku kamery i przybli�amy, to samo co gdy wyjmujemy obiekt z ekwipunl w BottomToggleShow funkcja consoleShowEvidenceNoEdit(). NIe robimy tego gdy canvasy s� w��czone, bo z ekwipynku obiekt jest ju� obr�cony. kolejno�� wywo�ywania funkcji w OnMouseDown() jest wa�na by zmienna lookat si� zgadza�a
            if (obracable && !canvas.activeSelf)
            {
                StartCoroutine(softRotateAt(cc.transform.position, 0.7f));
            }

            //pozwalamy na edycje, je�li w tryb szukania dowod�w weszli�my ze sceny/ miejsca
            fromEQ = false;
            observationStudy();

            //odpalamy animacje przygl�dania sie dowodowi, kolejno�� wywo�ywania funkcji w OnMouseDown() jest wa�na by zmienna lookat si� zgadza�a
            cc.lookAt(hit.transform.position);
        }      


        //GameObject.Find("Item").transform.SetPositionAndRotation(cc.transform.position + cc.transform.forward * 2f, cc.transform.rotation);
        //Quaternion directiob = Quaternion.LookRotation( cc.transform.position - transform.position).normalized;
        //GameObject.Find("Item").transform.rotation = Quaternion.Slerp(transform.rotation, directiob, Time.deltaTime * 0.01f);



        //Debug.Log("Dzio�o?");
        //GameObject.Find("Item").transform.position = Vector3.MoveTowards(transform.position, cc.transform.position + cc.transform.forward * 2f, Time.deltaTime * 2);
    }

    //Updatejtuje dane wywo�ywane naci�ni�ciem 
    public void observationName(Button number)
    {
        //Debug.Log("Up "+ Convert.ToInt32(number.name)+" "+ name);

        //DODAJEMY CZAS PRZY ZBADANEJ OBSERVACJI!!!wraz z zabezpieczeniem, przed wielokrotym nabijaniem czasu przy klikaniu w odkryty przycisk i nie mo�na doda� czasu,
        //je�li obserwacja zosta�a zaktualizowana inn� drog�, by nie nalicza� niepotrzebnie czasu
        if (!os.observationDownload(Convert.ToInt32(number.name), name))
        {
            gm.SetTime(number.GetComponent<ObservationButton>().timeNeeded);
        }


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

        //niszczymy obiekt po wy�ciu z obserwacji gdy jest do podniesienia. Canvas i sprawdzanie  pierwszego kafelka kt�ry w  ifie, poniewa�  w podoszonych dowodach jest w�a�nie podniesieniem. Nie chcemy nisczy� dowodu, na kt�ry nie patrzymy i niezbadali�my 
        if (pickable && canvas.activeSelf && os.observationDownload(1, transform.root.name) == true)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            //Je�li nie nieszczymy obiektu, to wracama na pozycje poczontkow� 
            StartCoroutine(softRotateBack(0.4f));
        }


        //StartCoroutine(waiterAnimator());
        canvas.SetActive(false);

        //Zmiena sprawdzaj�ca czy przygl�dasz si� obiektowi. Blokuje np poruszanie si� postaci
        cc.isLook = false;
        //scopesAnimator.SetInteger("whatScope", 0);
        //os.showObservationArray(transform.root.name);

        //Wy��czamy boxcollider, by mo�na by�o wchodzi� w iterakcje z otoczeniem gdy nie przegl�damy dowod
        cc.boxCollider(false);

        //w��czamy UI gry gdy jest wy��czone UI dowodu
        GameUI.SetActive(true);
    }

    //Funkcja przycisku wchodz�cego w tryp przygl�dania sie. Znika UI z kafelkami. Uruchamiana przyciskiem
    public void buttonPreview()
    {
        panel.SetActive(!panel.activeSelf);

        //Wy��czamy lub w��czamy boxcollider, by mo�na by�o wchodzi� w iterakcje z otoczeniem gdy nie przegl�damy dowodu
        cc.boxCollider(panel.activeSelf);

    }


    //badanie observacji
    public void observationStudy()
    {


        //scopesAnimator.enabled = true;


        //W��czamy boccollider je�li si� mu zaczynamy przygl�d�, by nie w��cza� si� gdy np ju� si� przygl�damy ale chcieliby�my go obraca� na preview, by nie mo�na by�o wchodzi� w iterakcje z otoczeniem podczas prz�gl�dania dowodu
        if(cc.isLook==false)
        {
           cc.boxCollider(true);
        }

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


        //Zawsze ma by� pnel z dowodami w��czony, gdy uruchamiamy z nim interakcje, nawet gdy wcze�niej wy��czyli�my go przyciskiem prewiev
        panel.SetActive(true);

        //wy��czamy UI gry gdy jest w��czone UI dowodu
        GameUI.SetActive(false);

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




    IEnumerator softRotateAt(Vector3 target, float inTime)
    {
        //Zmiena plikuj�ca przyblizanie si� dowodu do kamery, by nie by�a sytuacji w kt�rej szybko klikamy interakcje i z niej wychodzimy, a animacja si� blokuje 
        brakeSoftRotateAt = false;

        var fromAngle = Item.transform.rotation;
        var toAngle = Quaternion.LookRotation(target - transform.position);
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            if (!brakeSoftRotateAt)
            {
                Item.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                //Item.transform.position = Quaternion.Slerp(fromAngle, toAngle, t);
                //zmieniamy pozycje samej obserwacji, nie samego Item, by boxCollider by� w miejscu gdzie znajduje sie model obserwacji
                transform.position = Vector3.Lerp(transform.position, cc.transform.position + cc.transform.forward * 1.5f, t / (inTime*10) );
                yield return null;
            }
        }
        //po p�tli ustawiamy sztywno pozycje by nie by�a np o 0.0001 zminiona 
        Item.transform.rotation = toAngle;
        //transform.position = (cc.transform.position + cc.transform.forward * 1.5f);
    }

    //urzywane do obrotu obiektu do pierwotnej pozycji, gdy go nie podnosimy
    IEnumerator softRotateBack(float inTime)
    {
        //Zmiena plikuj�ca przyblizanie si� dowodu do kamery, by nie by�a sytuacji w kt�rej szybko klikamy interakcje i z niej wychodzimy, a animacja si� blokuje 
        brakeSoftRotateAt = true;

        var fromAngle = Item.transform.rotation;
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            Item.transform.rotation = Quaternion.Slerp(fromAngle, startRot, t);
            //zmieniamy pozycje samej obserwacji, nie samego Item, by boxCollider by� w miejscu gdzie znajduje sie model obserwacji
            transform.position = Vector3.Lerp(transform.position, startPos, t);
            yield return null;
        }
        //po p�tli ustawiamy sztywno pozycje by nie by�a np o 0.0001 zminiona 
        Item.transform.rotation = startRot;
        transform.position = startPos;
    }
}

      

    