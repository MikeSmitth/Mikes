using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerTester : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float clockwise = 1000.0f;
    public float counterClockwise = -5.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += transform.forward * movementSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += Vector3.back * movementSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left  * movementSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right * movementSpeed;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0, clockwise, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0, counterClockwise, 0);
        }
    }


}

