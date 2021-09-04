using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float interactiveDistance = 100;
    public bool animation;
    private Quaternion startPos = Quaternion.Euler(0, 0, 0);
    
    void Start()
    {
        startPos = this.transform.rotation;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, interactiveDistance) && hit.collider.tag == "Interactive")
        {      
            startPos = this.transform.rotation;
            //Debug.Log("startPos: " + transform.position);
            StartCoroutine(softLookAt(hit.transform.position, 1f));            
        }
    }

    public void lookBack()
    {
        StartCoroutine(softLookBack(1f));
    }

    IEnumerator softLookBack(float inTime)
    {
        var fromAngle = transform.rotation;
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, startPos, t);
            yield return null;
        }
        transform.rotation = startPos;
    }

    IEnumerator softLookAt(Vector3 target, float inTime)
    {
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
