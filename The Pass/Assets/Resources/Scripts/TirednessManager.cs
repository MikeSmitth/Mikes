using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirednessManager : MonoBehaviour
{
    GlobalManager gm;
    void Awake()
    {
        gm = GameObject.Find("Managers").GetComponent<GlobalManager>();
    }

    //Mechanika spania, narazie dzia³a przyciskiem 
    public void Sleep()
    {
        gm.SetTimeFlat(gm.inGameTime += 20f);
        gm.SettirednessFlat(gm.tiredness-= 50f);
    }
}
