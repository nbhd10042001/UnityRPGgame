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
    private float m_curValueMusic;
    private float m_curValueEffect;

    private void Awake()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();

        m_curValueMusic = PlayerPrefs.GetFloat("vMusic");
        m_curValueEffect = PlayerPrefs.GetFloat("vEffect");

        if (m_curValueMusic == 0)
            m_curValueMusic = m_sliderMusic.value;
        m_sliderMusic.value = m_curValueMusic;
        m_txtVolumeMusic.text = $"{(int)(m_curValueMusic * 100)}%";

        if (m_curValueEffect == 0)
            m_curValueEffect = m_sliderEffect.value;
        m_sliderEffect.value = m_curValueEffect;
        m_txtVolumeEffect.text = $"{(int)(m_curValueEffect * 100)}%";
    }


    public void onValueVolumeMusicChanged()
    {
        m_curValueMusic = m_sliderMusic.value;
        PlayerPrefs.SetFloat("vMusic", m_curValueMusic);
        m_txtVolumeMusic.text = $"{(int)(m_curValueMusic * 100)}%";
        m_AudioManager.SetVolumeSourceMusic(PlayerPrefs.GetFloat("vMusic"));
    }

    public void onValueVolumeEffectChanged()
    {
        m_curValueEffect = m_sliderEffect.value;
        PlayerPrefs.SetFloat("vEffect", m_curValueEffect);
        m_txtVolumeEffect.text = $"{(int)(m_curValueEffect * 100)}%";
        m_AudioManager.SetVolumeSourceEffect(PlayerPrefs.GetFloat("vEffect"));
    }

    public void SetVolumeUI(bool isAct)
    {
        m_VolumeUI.SetActive(isAct);
    }


}
