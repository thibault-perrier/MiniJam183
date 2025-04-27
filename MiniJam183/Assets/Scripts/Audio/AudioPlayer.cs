using System.Collections.Generic;
using Audio;
using AYellowpaper.SerializedCollections;
using Extensions;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance { get; private set; }
    [HideInInspector] public AudioController audioController;

    public SerializedDictionary<AudioEnum, List<AudioClip>> audioClips = new();

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
        audioController = GetComponent<AudioController>();
    }

    public void PlaySfx(AudioEnum _audioEnum)
    {
        AudioClip _clip = audioClips[_audioEnum].PickRandom();
        if (_clip != null)
        {
            var _newAudioObject = new GameObject("AudioObject", typeof(AudioSource));
            var _audioSource = _newAudioObject.GetComponent<AudioSource>();
            _audioSource.clip = _clip;
            _audioSource.loop = false;
            _audioSource.outputAudioMixerGroup = audioController.audioMixerSfx;
            _audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + _audioEnum);
        }
    }
}
