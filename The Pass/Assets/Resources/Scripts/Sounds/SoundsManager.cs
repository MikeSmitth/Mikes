using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioClip EQSound;
    [SerializeField] AudioClip ButtonSound;
    AudioSource EQSoundSource;
    AudioSource[] allAudioSources;
    void Start()
    {
        EQSoundSource = GetComponent<AudioSource>();
    }
    void Awake()
    {
        allAudioSources = GameObject.FindSceneObjectsOfType(typeof(AudioSource)) as AudioSource[];
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
    public void StopPlaying()
    {

        //Debug.Log("Sema");
        //Nie ma zabezpieczenia, bo i tak wy�wietlany jest wwarnning w razie braku d�wi�ku
        EQSoundSource.Stop();
    }

    public void StopPlayingAllAudio()
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }
}
