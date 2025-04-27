using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [Header("Audio Sliders")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    [Header("Audio Sources")]
    [SerializeField] private AudioMixer _audioMixerMaster;


    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _masterVolumeSlider.value = PlayerPrefs.GetFloat(PlayerParamsPreferences.PlayerPrefsMasterVol, .5f);
        _musicVolumeSlider.value = PlayerPrefs.GetFloat(PlayerParamsPreferences.PlayerPrefsMusicVol, .5f);
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat(PlayerParamsPreferences.PlayerPrefsSFXVol, .5f);

        _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChange);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChange);

        UpdateMasterVolume();
        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    private void OnMasterVolumeChange(float value)
    {
        PlayerPrefs.SetFloat(PlayerParamsPreferences.PlayerPrefsMasterVol, value);
        UpdateMasterVolume();
    }

    private void OnMusicVolumeChange(float value)
    {
        PlayerPrefs.SetFloat(PlayerParamsPreferences.PlayerPrefsMusicVol, value);
        UpdateMusicVolume();

    }

    private void OnSFXVolumeChange(float value)
    {
        PlayerPrefs.SetFloat(PlayerParamsPreferences.PlayerPrefsSFXVol, value);
        UpdateSFXVolume();
    }

    private void UpdateMasterVolume()
    {
        _audioMixerMaster.SetFloat(PlayerParamsPreferences.PlayerPrefsMasterVol, Mathf.Log10(_masterVolumeSlider.value) * 20);
    }

    private void UpdateMusicVolume()
    {
        _audioMixerMaster.SetFloat(PlayerParamsPreferences.PlayerPrefsMusicVol, Mathf.Log10(_musicVolumeSlider.value) * 20);
    }

    private void UpdateSFXVolume()
    {
        _audioMixerMaster.SetFloat(PlayerParamsPreferences.PlayerPrefsSFXVol, Mathf.Log10(_sfxVolumeSlider.value) * 20);
    }

    private void OnDestroy()
    {
        _masterVolumeSlider.onValueChanged.RemoveListener(OnMasterVolumeChange);
        _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChange);
        _sfxVolumeSlider.onValueChanged.RemoveListener(OnSFXVolumeChange);
    }
}
