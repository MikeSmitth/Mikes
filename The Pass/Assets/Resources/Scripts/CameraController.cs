using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    //zmiena do przechowywnia konta rotacji
    private Quaternion startPos = Quaternion.Euler(0, 0, 0);

    //zmienne zachowuj¹ce boost wychy³u kamery przy rozgl¹daniu w góre i dó³
    float cameraStrokeYBoost;
    float cameraStrokeXBoost;

    //zmienna maksymalnego wychy³u w ró¿nych kierunkach
    public float cameraStroke = 3;

    //czy patrzymy na intarktywny obiekt 
    public bool isLook;
    //jak daleko mo¿e byæ interaktywny obiekt
    public float interactiveDistance = 5.5f;

    void Start()
    {

        //pobieramy pocz¹tkowe dane o rotacji
        //startPos = this.transform.rotation;
    }
    void Update()
    {
        float cameraStrokeYBoost = 1;
        float cameraStrokeXBoost = 1;


        //ustalamy kierunek pronmienia
        /*
             Debug.Log("good 1");
             Ray rayy = Camera.main.ScreenPointToRay(Input.mousePosition);
             Debug.DrawRay(rayy.origin, rayy.direction * 1f, Color.green);
         */


        //Dzia³aj¹cy debug log
        //Debug.Log("Mause " + Input.mousePosition+ " Screen Width/Height : " + Screen.width+"/"+ Screen.height);


        //Debug.Log("% y " + (((Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * 100f * -1) /*/ 100f * cameraStroke*/));
        //Debug.Log("% x " + (((Input.mousePosition.y - (Screen.width / 2)) / (Screen.width / 2) * 100f) /*/ 100f * cameraStroke*/));
        //Debug.Log("%of cameraStroke y " + (((Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * 100f * -1) / 100f * cameraStroke));
        //Debug.Log("%of cameraStroke x " + (((Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2) * 100f) / 100f * cameraStroke));
        //Debug.Log("Parent rotation x y " + transform.rotation);


        // jeœli nie przygl¹damy siê obiektowi isLook to wychylamy kamere kursorem
        if (!isLook)
        {
            if (Input.mousePosition.y < Screen.height / 2)
            {
                cameraStrokeYBoost=8f;
            }

            var toAngle = Quaternion.Euler(transform.parent.eulerAngles + new Vector3(((Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2)* cameraStroke * cameraStrokeYBoost * -1), ((Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2)* cameraStroke* cameraStrokeXBoost), 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, toAngle, Time.deltaTime * 50f);
        }
            //Debug.Log("currentEulerAngles " + currentEulerAngles);
            //Debug.Log("transform.eulerAngles " + transform.eulerAngles);
    }


    //wychodzimy z trybu przygl¹dania siê. Aktywuje sie przyciskiem Exit w unity
    public void lookBack()
    {
        StartCoroutine(softLookBack(0.15f));
    }

    //Spogl¹damy na obserwacje
    public void lookAt(Vector3 hit)
    {
        startPos = this.transform.rotation;
        StartCoroutine(softLookAt(hit, 0.3f));
    }

    //animacja powrotu
    IEnumerator softLookBack(float inTime)
    {
        isLook = false;      
        var fromAngle = transform.rotation;

        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, startPos, t);
            yield return null;
        }
        transform.rotation = startPos;     
    }


    //animacja przygl¹dania siê
    IEnumerator softLookAt(Vector3 target, float inTime)
    {

       
        isLook = true;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.LookRotation(target - transform.position);
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
          {
             transform.rotation = Quaternion.Slerp(fromAngle, toAngle,  t);
             yield return null;
          }
          transform.rotation = toAngle;
    }


    //funkcja sterujaca Collliderem który blokuje interaktywnoœæ t³a, np gdy przegl¹damy dowód
    public void boxCollider(bool set)
    {
        GetComponent<BoxCollider>().enabled = set;
    }

    //Funkcja badaj¹ca na jaki obiekt patrzymy, wywo³ywana w innych clasach 
    public Collider hitTag()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, interactiveDistance);
       
        if (hit.collider)
        {
            return hit.collider;
        }
        else
        {
            //W przypadku nei natrafienia na ¿aden obiekt zwracam collider kamery 
            return this.gameObject.GetComponent<Collider>();
        }
        
    }
}
