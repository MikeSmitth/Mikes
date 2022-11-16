using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("In Game Time")]
    public float inGameTime = 0;
    // Start is called before the first frame update


    static TimeManager instance;
    private void Awake()
    {
        if(inGameTime != 0)
        {
            Debug.LogWarning("Czas nie jest wyzerowany");
        }
        if (instance != null)
        {
            Debug.LogWarning("More than one Time Menager");
        }
        instance = this;
    }

    public void SetTime(float time)
    {
        inGameTime = time;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
