using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SoundTester : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    void PlaySound()
    {
        audioSource.Play();
    }

    #region Button

    [Title("PlaySound")]
    [Button("PlaySound", ButtonSizes.Large)]
    void ButtonPlaySound()
    {
        PlaySound();
    }
    #endregion
}
