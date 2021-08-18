using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationStorage : MonoBehaviour
{
    bool[] shoePrint = new bool[20];

    public void observationUpdate(int i, string s)
    {
        shoePrint[i-1] = true;
        Debug.Log("Set: " + shoePrint[i-1]);
    }
    public bool observationDownload(int i)
    {
        return shoePrint[i];
    }
}
