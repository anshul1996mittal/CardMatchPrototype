using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the settings UI panel, including sound and music toggles/sliders.
/// </summary>
public class SettingPanel : MonoBehaviour
{
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Slider soundFxSlider;
    [SerializeField] private Slider musicSlider;

    private void OnEnable()
    {
        SetUpSettingInfo();
    }

    /// <summary>
    /// Initializes the UI elements with values from PlayerPrefs.
    /// </summary>
    private void SetUpSettingInfo()
    {
        soundToggle.isOn = PlayerPrefsHalper.IS_SOUND_ON;
        soundFxSlider.value = PlayerPrefsHalper.SOUNDFX_VOLUME;
        musicSlider.value = PlayerPrefsHalper.MUSUSIC_VOLUME;
    }

    /// <summary>
    /// Updates the master sound toggle setting in AudioManager.
    /// </summary>
    public void SoundToggleUpdate()
    {
        AudioManager.Instance.SetToggleValue(soundToggle.isOn);
    }

    /// <summary>
    /// Updates the SFX volume in AudioManager based on the slider value.
    /// </summary>
    public void SFXVolumeChnageUpdate()
    {
        AudioManager.Instance.SetSoundFXValue(soundFxSlider.value * 10);
    }

    /// <summary>
    /// Updates the Music volume in AudioManager based on the slider value.
    /// </summary>
    public void MusicVolumeChnageUpdate()
    {
        AudioManager.Instance.SetMusicValue(musicSlider.value * 10);
    }

    /// <summary>
    /// Saves the current UI settings to PlayerPrefs and updates the AudioManager.
    /// </summary>
    public void SettingUpdate()
    {
        PlayerPrefsHalper.IS_SOUND_ON = soundToggle.isOn;
        PlayerPrefsHalper.SOUNDFX_VOLUME = soundFxSlider.value;
        PlayerPrefsHalper.MUSUSIC_VOLUME = musicSlider.value;

        AudioManager.Instance.SetToggleValue(soundToggle.isOn);
        AudioManager.Instance.SetSoundFXValue(soundFxSlider.value*10);
        AudioManager.Instance.SetMusicValue(musicSlider.value * 10);
    }
    private void OnDisable()
    {
        SetUpSettingInfo();
    }

}
