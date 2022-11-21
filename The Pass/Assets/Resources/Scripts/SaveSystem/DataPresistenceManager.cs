using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPresistenceManager : MonoBehaviour
{
   
    public DataPresistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
             
        }
    }
}
