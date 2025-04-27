using Audio;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioForButton : MonoBehaviour
{
    [SerializeField] private AudioEnum m_AudioEnum = AudioEnum.ClickSound;
    void Start()
    {
        GetComponentInParent<Button>()?.onClick.AddListener(OnClick);
        GetComponentInParent<TMP_Dropdown>()?.onValueChanged.AddListener((_)=>OnClick());
        GetComponentInParent<Toggle>()?.onValueChanged?.AddListener((_)=>OnClick());
    }

    private void OnClick()
    {
        AudioPlayer.instance.PlaySfx(m_AudioEnum);
    }
}
