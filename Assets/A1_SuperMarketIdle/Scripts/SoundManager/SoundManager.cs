using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicAudioSource;

    public static SoundManager instance;
    [SerializeField] bool hapticsSupported;

    private void Awake()
    {
        SingletonCheck();
    }

    void SingletonCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void MusicOnOff(bool state)
    {
        //print("state: " + state + " isPlaying: " + musicAudioSource.isPlaying);
        if (state == true && !musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
        else if (state == false && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }       
    }
}
