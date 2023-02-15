using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //[Header("NPC Notes")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Animator shortHand;
    [SerializeField] Animator longHand;
    [SerializeField] Animator dayNightAnimator;
    [SerializeField] AudioClip tick;
    [SerializeField] AudioClip tock;

    GlobalManager gm;
    SoundsManager sm;

    AnimatorClipInfo[] m_CurrentClipInfoShort;
    AnimatorClipInfo[] m_CurrentClipInfoLong;
    string m_ClipNameShort;
    string m_ClipNameLong;
    //float m_CurrentClipLength;
    
    float time;
   

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Managers").GetComponent<GlobalManager>();
        sm = GameObject.Find("Managers").GetComponent<SoundsManager>();
    }

    void Update()
    {
        m_CurrentClipInfoShort = shortHand.GetCurrentAnimatorClipInfo(0);
        m_ClipNameShort = m_CurrentClipInfoShort[0].clip.name;


        m_CurrentClipInfoLong = longHand.GetCurrentAnimatorClipInfo(0);
        m_ClipNameLong = m_CurrentClipInfoLong[0].clip.name;


        if (gm.inGameTime != time)
        {
            time = gm.inGameTime;
            TimerAnim();
        }


    }
    public void TimerAnim()
    {
        float shortHandTime = Mathf.Round((time % 6f) * 2f) * 0.5f;
        shortHandTime = Mathf.Round(shortHandTime*10);



        if(m_ClipNameShort != ("short_" + shortHandTime))
        {
           // Debug.Log( m_ClipNameShort + "!=" + ("short_" + shortHandTime));
            sm.PlaySoundClip(tick);
        }
        shortHand.Play("short_" + shortHandTime);




        float longHandTime = time % 72;

        longHandTime = Mathf.Round(longHandTime / 6);
        if(longHandTime == 12)
        {
            longHandTime = 0;
        }



        if (m_ClipNameLong != ("long_" + longHandTime))
        {
            Debug.Log( m_ClipNameLong + "!=" + ("long_" + longHandTime));
            sm.PlaySoundClip(tock);
        }
        longHand.Play("long_" + longHandTime);



        float dayNight = Mathf.Round(time % 144);
        Debug.Log("dayNight:" + dayNight);
        if (dayNight >= 36 && dayNight <=108)
        {
            dayNightAnimator.Play("day");
        }
        else
        {
            dayNightAnimator.Play("night");
        }

        float day = Mathf.Round(time / 144);
        if(day ==0 )
        {
            day = 1;
        }

        timeText.text = (""+ day);
        Debug.Log("Time igt:" +time);
    }

    private void ResetAllTriggers(Animator anim)
    {
        foreach (var param in anim.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                anim.ResetTrigger(param.name);
            }
        }
    }
}
