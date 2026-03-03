using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Manages audio settings, including background music and sound effects volume.
/// Handles AudioMixer parameter updates based on player preferences.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;

    private const float MIN_DB_MUSIC = -80f, MAX_DB_MUSIC = 0f;
    private const float MIN_DB_SFX = -80f, MAX_DB_SFX = 0f;
    
    
    private const float MAX_VOLUME_VALUE = 100f;
    private const float CURVE_DIVISOR = 10000f;

    void Awake()
    {
        // Singleton enforcement: If an instance already exists, destroy this one.
        if (IsAwake)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    void OnEnable()
    {
        // Delay initialization slightly to ensure GameManager and AudioMixer are ready
        StartCoroutine(GameManager.Instance.ActionCallAfterTime(0.1f, true, () => 
        {
            SetToggleValue(PlayerPrefsHalper.IS_SOUND_ON);
            SetSoundFXValue(PlayerPrefsHalper.SOUNDFX_VOLUME * 10);
            SetMusicValue(PlayerPrefsHalper.MUSUSIC_VOLUME * 10);
        }));
    }
   
    /// <summary>
    /// Sets the Sound FX volume exposed parameter on the AudioMixer.
    /// </summary>
    /// <param name="value">Volume level (0-100).</param>
    public void SetSoundFXValue(float value)
    {
        float normalizedVolume = CalculateLogarithmicVolume(value);
        float finalDb = Mathf.Lerp(MIN_DB_SFX, MAX_DB_SFX, normalizedVolume);
        
        audioMixer.SetFloat("SFX", finalDb);
    }

    /// <summary>
    /// Sets the Music volume exposed parameter on the AudioMixer.
    /// </summary>
    /// <param name="value">Volume level (0-100).</param>
    public void SetMusicValue(float value)
    {
        float normalizedVolume = CalculateLogarithmicVolume(value);
        float finalDb = Mathf.Lerp(MIN_DB_MUSIC, MAX_DB_MUSIC, normalizedVolume);

        audioMixer.SetFloat("GameMusic", finalDb);
    }

    /// <summary>
    /// Toggles the Master volume between Mute (-80dB) and Unmute (0dB).
    /// </summary>
    /// <param name="isSoundOn">True to unmute, false to mute.</param>
    public void SetToggleValue(bool isSoundOn)
    {
        audioMixer.SetFloat("Master", isSoundOn ? 0 : -80);
    }

    /// <summary>
    /// Calculates a normalized volume curve (0.0 to 1.0) from a linear input (0 to 100).
    /// Applies a cubic curve to approximate audio perception.
    /// </summary>
    private float CalculateLogarithmicVolume(float linearValue)
    {
        // Invert value: 100 (loud) -> 0, 0 (quiet) -> 100
        float invertedValue = MAX_VOLUME_VALUE - (int)linearValue;
        
        // Apply cubic curve: x^3 / 10000
        // This creates a curve where volume drops off slowly at first, then quickly
        float curvedValue = invertedValue * invertedValue * invertedValue / CURVE_DIVISOR;
        
        // Re-invert and normalize to 0-1 range
        return (MAX_VOLUME_VALUE - curvedValue) / MAX_VOLUME_VALUE;
    }
}
