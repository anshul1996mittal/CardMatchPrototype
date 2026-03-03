using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    public AudioSource _SFXAudio;

    public void OnEnable()
    {
        Events.playAudio += playSFX;
    }
    public void OnDisable()
    {
        Events.playAudio -= playSFX;
    }

    public void playSFX(AudioClip _clip)
    {
        _SFXAudio.PlayOneShot(_clip);
    }
}
