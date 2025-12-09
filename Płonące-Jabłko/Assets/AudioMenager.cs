using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;






public class AudioMenager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public static AudioMenager instance;
    bool isDialogEnd = false;



    public void PlaySound(Sound AudioPlay)
    {
        GetComponent<AudioSource>().clip = AudioPlay.clip;
        GetComponent<AudioSource>().Play();


    }

    public void PlayMusic(string soundName)
    {
        Sound AudioPlay = Array.Find(musicSounds, y => y.soundName == soundName);
        

        musicSource.clip = AudioPlay.clip;
        musicSource.loop = true;
        musicSource.Play();

        
      

    }

    public void PlaySFX(string soundName)
    {
        Sound AudioPlay = Array.Find(sfxSounds, y => y.soundName == soundName);
        sfxSource.PlayOneShot(AudioPlay.clip);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //PlayMusic(musicSounds[0]);
    }
}


