using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isMoving;
    bool isRotating;
    private Vector3 origPos, targetPos;
    public float timeToMove = 0.8f;
    public float tileSize;


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("transform: "+ transform.position);
        if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            // Debug.Log("W");
            StartCoroutine(MovePlayer(transform.forward, timeToMove-0.2f));
            //timeToMove += 0.1f;
        }
        if (Input.GetKey(KeyCode.S) && !isMoving)
            StartCoroutine(MovePlayer(-transform.forward, timeToMove));
        if (Input.GetKey(KeyCode.A) && !isMoving)
            StartCoroutine(MovePlayer(-transform.right, timeToMove));
        if (Input.GetKey(KeyCode.D) && !isMoving)
            StartCoroutine(MovePlayer(transform.right, timeToMove));
        if (Input.GetKey("e"))
        {
            StartCoroutine(RotateM(Vector3.up * 90, 0.8f));
        }
        if (Input.GetKey("q"))
        {
            StartCoroutine(RotateM(Vector3.up * -90, 0.8f));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction, float moveSpeed)
    {
        Vector3 fromDirection = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        RaycastHit hit;
        bool hitObstacle = false;

        if (Physics.Raycast(fromDirection, direction, out hit, tileSize) && hit.collider.tag == "Ramp")
        {           
            direction = (new Vector3(direction.x, direction.y + 0.5f, direction.z));
        }
        else if(Physics.Raycast(fromDirection, direction, out hit, tileSize) && hit.collider.tag == "RampDown")
        {
            direction = (new Vector3(direction.x, direction.y - 0.5f, direction.z));
        }
        else if(hit.collider)
        {
            hitObstacle = true;
        }

        Debug.DrawRay(fromDirection, direction* tileSize, Color.red);

        if (!hitObstacle&&!isRotating&&!isMoving)
        {
            
            //Debug.Log("move");
            
            isMoving = true;
            float elapsedtime = 0;
            var origPos = transform.position;
            var targetPos = origPos + direction * tileSize;

            while (elapsedtime < moveSpeed)
            {
                //Debug.Log("moveing origPos: " + origPos+ " targetPos: "+ targetPos);
                Debug.DrawRay(fromDirection, direction * tileSize, Color.green);

                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedtime / moveSpeed));
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
