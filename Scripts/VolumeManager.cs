using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private GameObject m_VolumeUI;
    [SerializeField] private Slider m_sliderMusic;
    [SerializeField] private Slider m_sliderEffect;
    [SerializeField] private TextMeshProUGUI m_txtVolumeMusic;
    [SerializeField] private TextMeshProUGUI m_txtVolumeEffect;

    private AudioManager m_AudioManager;

    private void Awake()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();

        if (PlayerPrefs.GetInt("isF") == 0)
        {
            m_sliderMusic.value = 1;
            PlayerPrefs.SetFloat("vMusic", 1);
            m_txtVolumeMusic.text = $"{(int)(m_sliderMusic.value * 100)}%";


            m_sliderEffect.value = 1;
            PlayerPrefs.SetFloat("vEffect", 1);
            m_txtVolumeEffect.text = $"{(int)(m_sliderEffect.value * 100)}%";

            PlayerPrefs.SetInt("isF", 1);
        }
        else
        {
            m_sliderMusic.value = PlayerPrefs.GetFloat("vMusic");
            m_txtVolumeMusic.text = $"{(int)(m_sliderMusic.value * 100)}%";

            m_sliderEffect.value = PlayerPrefs.GetFloat("vEffect");
            m_txtVolumeEffect.text = $"{(int)(m_sliderEffect.value * 100)}%";
        }
    }


    public void onValueVolumeMusicChanged()
    {
        m_txtVolumeMusic.text = $"{(int)(m_sliderMusic.value * 100)}%";

        PlayerPrefs.SetFloat("vMusic", m_sliderMusic.value);
        m_AudioManager.SetVolumeSourceMusic(PlayerPrefs.GetFloat("vMusic"));
    }

    public void onValueVolumeEffectChanged()
    {
        m_txtVolumeEffect.text = $"{(int)(m_sliderEffect.value * 100)}%";

        PlayerPrefs.SetFloat("vEffect", m_sliderEffect.value);
        m_AudioManager.SetVolumeSourceEffect(PlayerPrefs.GetFloat("vEffect"));
    }

    public void SetVolumeUI(bool isAct)
    {
        m_VolumeUI.SetActive(isAct);
    }


}
