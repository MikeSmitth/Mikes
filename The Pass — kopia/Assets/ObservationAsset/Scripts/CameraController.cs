using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    //zmiena do przechowywnia konta rotacji
    private Quaternion startPos = Quaternion.Euler(0, 0, 0);

    //zmienne zachowuj�ce boost wychy�u kamery przy rozgl�daniu w g�re i d�
    float cameraStrokeYBoost;
    float cameraStrokeXBoost;

    //zmienna maksymalnego wychy�u w r�nych kierunkach
    public float cameraStroke = 3;

    //czy patrzymy na intarktywny obiekt 
    public bool isLook;
    //jak daleko mo�e by� interaktywny obiekt
    public float interactiveDistance = 100;

    void Start()
    {
        //pobieramy pocz�tkowe dane o rotacji
        startPos = this.transform.rotation;
    }
    void Update()
    {
        float cameraStrokeYBoost = 1;
        float cameraStrokeXBoost = 1;

       //ustalamy kierunek pronmienia
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //kastuje promie� sprawdzaj�cy czy pod kursorem jest obiekt interaktywny
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, interactiveDistance) && hit.collider.tag == "Interactive")
        {      
            startPos = this.transform.rotation;
            //Debug.Log("startPos: " + transform.position);

            //aktywowanie animacji przygl�dania si�
            StartCoroutine(softLookAt(hit.transform.position, 1f));            
        }

        //Dzia�aj�cy debug log
        //Debug.Log("Mause " + Input.mousePosition+ " Screen Width/Height : " + Screen.width+"/"+ Screen.height);


        //Debug.Log("% y " + (((Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * 100f * -1) /*/ 100f * cameraStroke*/));
        //Debug.Log("% x " + (((Input.mousePosition.y - (Screen.width / 2)) / (Screen.width / 2) * 100f) /*/ 100f * cameraStroke*/));
        //Debug.Log("%of cameraStroke y " + (((Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * 100f * -1) / 100f * cameraStroke));
        //Debug.Log("%of cameraStroke x " + (((Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2) * 100f) / 100f * cameraStroke));
        //Debug.Log("Parent rotation x y " + transform.rotation);


        // je�li nie przygl�damy si� obiektowi isLook to wychylamy kamere kursorem
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


    //wychodzimy z trybu przygl�dania si�. Aktywuje sie przyciskiem Exit w unity
    public void lookBack()
    {
        StartCoroutine(softLookBack(0.5f));
    }

    //animacja przygl�dania si�
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


    //animacja powrotu
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
    
}
