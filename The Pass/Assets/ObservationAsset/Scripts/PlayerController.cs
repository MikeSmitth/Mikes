using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isMoving;
    bool isRotating;
    private Vector3 origPos, targetPos;
    public float timeToMove = 0.6f;
    public float tileSize;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isMoving)
        {
           // Debug.Log("W");
            StartCoroutine(MovePlayer(transform.forward));
        }
        if (Input.GetKeyDown(KeyCode.S) && !isMoving)
            StartCoroutine(MovePlayer(-transform.forward));
        if (Input.GetKeyDown(KeyCode.A) && !isMoving)
            StartCoroutine(MovePlayer(-transform.right));
        if (Input.GetKeyDown(KeyCode.D) && !isMoving)
            StartCoroutine(MovePlayer(transform.right));
        if (Input.GetKeyDown("e"))
        {
            StartCoroutine(RotateM(Vector3.up * 90, 0.8f));
        }
        if (Input.GetKeyDown("q"))
        {
            StartCoroutine(RotateM(Vector3.up * -90, 0.8f));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        if (!Physics.Raycast(transform.position, direction, tileSize)&&!isRotating&&!isMoving)
        {
            //Debug.Log("move");
            isMoving = true;

            float elapsedtime = 0;

            origPos = transform.position;
            targetPos = origPos + direction * tileSize;

            while (elapsedtime < timeToMove)
            {
                Debug.Log("moveing origPos: " + origPos+ " targetPos: "+ targetPos);
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedtime / timeToMove));
                elapsedtime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;

            isMoving = false;
        }
    }

    IEnumerator RotateM(Vector3 byAngles, float inTime)
    {
        if (!isMoving&&!isRotating )
        {
            isRotating = true;
            var fromAngle = transform.rotation;
            var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
            for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
            {
                transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                yield return null;
            }
            transform.rotation = toAngle;
            isRotating = false;
        }
    }
}
