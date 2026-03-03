using UnityEngine;

// This Class manage all the Event Calling funcnality.
public class Events
{
    public delegate void AudioClipEvent(AudioClip audioClip);
    public static AudioClipEvent playAudio;
}
