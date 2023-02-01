using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioClip EQSound;
    [SerializeField] AudioClip ButtonSound;
    AudioSource EQSoundSource;
    void Start()
    {
        EQSoundSource = GetComponent<AudioSource>();
    }
    public void PlaySoundClip(AudioClip audioClip)
    {
       // if (!EQSoundSource.isPlaying)
       /// {
            EQSoundSource.PlayOneShot(audioClip);
       /// }
    }
    public void PlayEQSound()
    {
        //Debug.Log("Sema");
        //Nie ma zabezpieczenia, bo i tak wy�wietlany jest wwarnning w razie braku d�wi�ku
        EQSoundSource.PlayOneShot(EQSound);
    }
    public void PlayButtonSound()
    {

        //Debug.Log("Sema");
        //Nie ma zabezpieczenia, bo i tak wy�wietlany jest wwarnning w razie braku d�wi�ku
            EQSoundSource.PlayOneShot(ButtonSound);      
    }
}
