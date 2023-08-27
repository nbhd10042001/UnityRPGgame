using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEngine.UI;


public enum State { Diaglog, Battle, Setting, Save, Load, Volume}

public class GamePlayManager : MonoBehaviour
{
    private static GamePlayManager m_Instance;
    public static GamePlayManager Instance  
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<GamePlayManager>();
            return m_Instance;
        }
    }

    [SerializeField] private GameObject m_DialogPng;
    [SerializeField] private GameObject m_btnNPCChallenger;
    [SerializeField] private GameObject m_SettingUI;
    [SerializeField] private GameObject m_BattleUI;
    [SerializeField] private GameObject m_BoxNameNPC;

    [SerializeField] private TextMeshProUGUI m_nameP;
    [SerializeField] private Text m_nameNPC;

    private State m_curState;
    private PlayerData playerData;
    private SaveLoadManager m_SLmanager;
    private VolumeManager m_VolumeManager;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

    }

    private void Start()
    {
        SetState(State.Battle);
        m_SLmanager = GetComponent<SaveLoadManager>();
        m_VolumeManager = GetComponent<VolumeManager>();
        playerData = SavePlayer.Instance.LoadDataPlayer();
        m_nameP.text = playerData.name;
    }

    private void SetState (State state)
    {
        m_curState = state;

        m_BattleUI.SetActive(m_curState == State.Battle);
        m_DialogPng.SetActive(m_curState == State.Diaglog);
        m_SettingUI.SetActive(m_curState == State.Setting);

    }

    public void ShowDialogUI(bool isShow)
    {
        if (isShow)
            SetState(State.Diaglog);
        else
            SetState(State.Battle);
    }

    public void ShowBoxNameNPC(bool isShow, string name)
    {
        m_BoxNameNPC.SetActive(isShow);
        m_nameNPC.text = $"NPC {name}";
    }

    public void ShowBtnBattle(bool isShow)
    {
        if (isShow)
        {
            m_btnNPCChallenger.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            m_btnNPCChallenger.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Btnleave_Pressed()
    {
        ShowBtnBattle(false);
    }

    public void BtnFight_Pressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Battle");
    }

    public void BtnSetting_Pressed()
    {
        Time.timeScale = 0;
        SetState(State.Setting);
    }

    public void BtnBack_Pressed()
    {
        SetState(State.Setting);
        m_SLmanager.SetUI_Save(false);
        m_SLmanager.SetUI_Load(false);
        m_VolumeManager.SetVolumeUI(false);
    }

    public void BtnCancel_Pressed()
    {
        Time.timeScale = 1;
        SetState(State.Battle);
    }

    public void BtnHome_Pressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void BtnVolume_Pressed()
    {
        SetState(State.Volume);
        m_VolumeManager.SetVolumeUI(true);

    }

    public void BtnSave_Pressed()
    {
        SetState(State.Save);
        m_SLmanager.SetUI_Save(true);

    }

    public void BtnLoad_Pressed()
    {
        SetState(State.Load);
        m_SLmanager.SetUI_Load(true);

    }

    public void LoadNewFilePlayer()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }
}
