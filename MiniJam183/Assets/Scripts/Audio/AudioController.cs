using System;
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


    private string _playerPrefsMasterVol = "MasterVolume";
    private string _playerPrefsMusicVol = "MusicVolume";
    private string _playerPrefsSFXVol = "SFXVolume";

    private string _masterVolumeMixer = "MasterVolume";
    private string _musicVolumeMixer = "MusicVolume";
    private string _sfxVolumeMixer = "SFXVolume";


    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _masterVolumeSlider.value = PlayerPrefs.GetFloat(_playerPrefsMasterVol, .5f);
        _musicVolumeSlider.value = PlayerPrefs.GetFloat(_playerPrefsMusicVol, .5f);
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat(_playerPrefsSFXVol, .5f);

        _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChange);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChange);

        UpdateMasterVolume();
        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    private void OnMasterVolumeChange(float value)
    {
        PlayerPrefs.SetFloat(_playerPrefsMasterVol, value);
        UpdateMasterVolume();
    }

    private void OnMusicVolumeChange(float value)
    {
        PlayerPrefs.SetFloat(_playerPrefsMusicVol, value);
        UpdateMusicVolume();

    }

    private void OnSFXVolumeChange(float value)
    {
        PlayerPrefs.SetFloat(_playerPrefsSFXVol, value);
        UpdateSFXVolume();
    }

    private void UpdateMasterVolume()
    {
        _audioMixerMaster.SetFloat(_masterVolumeMixer, Mathf.Log10(_masterVolumeSlider.value) * 20);
    }

    private void UpdateMusicVolume()
    {
        _audioMixerMaster.SetFloat(_musicVolumeMixer, Mathf.Log10(_musicVolumeSlider.value) * 20);
    }

    private void UpdateSFXVolume()
    {
        _audioMixerMaster.SetFloat(_sfxVolumeMixer, Mathf.Log10(_sfxVolumeSlider.value) * 20);
    }

    private void OnDestroy()
    {
        _masterVolumeSlider.onValueChanged.RemoveListener(OnMasterVolumeChange);
        _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChange);
        _sfxVolumeSlider.onValueChanged.RemoveListener(OnSFXVolumeChange);
    }
}
