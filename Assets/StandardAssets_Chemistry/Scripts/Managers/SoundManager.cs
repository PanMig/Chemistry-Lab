using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    //Molecule construction
    public AudioClip elementCollision;
    public AudioClip elementConstructed;


    void Awake()
    {
        //sigleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(GameManager.currentLevel != GameManager.Levels.lab)
        {
            instance.musicSource.Play();
        }
    }

    public void PlaySingle(AudioClip clip)
    {
        instance.efxSource.clip = clip;
        instance.efxSource.Play();
    }

    public void StopBackAudio()
    {
        if (!instance.musicSource.enabled)
        {
            instance.musicSource.enabled = true;
            instance.musicSource.Play();
        }
        else
        {
            instance.musicSource.enabled = false;
        }
    }

}
