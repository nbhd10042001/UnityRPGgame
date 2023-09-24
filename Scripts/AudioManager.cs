using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Instance;
    public static AudioManager Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<AudioManager>();
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }

    [SerializeField] private AudioSource m_Music;
    [SerializeField] private AudioSource m_SFX;

    [SerializeField] private AudioClip m_musicHomeClip;
    [SerializeField] private AudioClip m_musicMenuClip;
    [SerializeField] private AudioClip m_musicBattleClip;
    [SerializeField] private AudioClip m_sfxSwordSlash;
    [SerializeField] private AudioClip m_sfxEnemyGetHit;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
            PlayMenuMusic();
        else if (SceneManager.GetActiveScene().name == "Home")
            PlayHomeMusic();

    }

    public void SetVolumeSourceMusic(float value)
    {
        if (value == 0)
            m_Music.mute = true;
        else
        {
            m_Music.mute = false;
            m_Music.volume = value;
        }

    }

    public void SetVolumeSourceEffect(float value)
    {
        if (value == 0)
            m_SFX.mute = true;
        else
        {
            m_SFX.mute = false;
            m_SFX.volume = value;
        }

    }

    public void PlayMenuMusic()
    {
        m_Music.loop = true;
        m_Music.clip = m_musicMenuClip;
        m_Music.Play();
    }

    public void PlayHomeMusic()
    {
        m_Music.loop = true;
        m_Music.clip = m_musicHomeClip;
        m_Music.Play();
    }

    public void PlayBattleMusic()
    {
        m_Music.loop = true;
        m_Music.clip = m_musicBattleClip;
        m_Music.Play();
    }

    public void PlaySfxSlashSword()
    {
        m_SFX.pitch = Random.Range(0.5f, 1f);
        m_SFX.PlayOneShot(m_sfxSwordSlash);
    }

    public void PlaySfxEnemyGetHit()
    {
        m_SFX.pitch = Random.Range(0.5f, 1f);
        m_SFX.PlayOneShot(m_sfxEnemyGetHit);
    }
}
