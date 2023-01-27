using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioClip EQSound;
    AudioSource EQSoundSource;
    void Start()
    {
        EQSoundSource = GetComponent<AudioSource>();
    }
    public void PlaySoundClip(AudioClip audioClip)
    {
        if (!EQSoundSource.isPlaying)
        {
            EQSoundSource.PlayOneShot(audioClip);
        }
    }
    public void PlayEQSound()
    {
        //Debug.Log("Sema");
        EQSoundSource.PlayOneShot(EQSound);
    }
}
