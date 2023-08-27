using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public Action <int, int> OnCurHpChange;
    public Action <int, int> OnCurManaChange;
    public Action <float, float> OnCurExpChange;
    public Action <int> OnCurLevelChange;

    private static PlayerStats m_Instance;
    public static PlayerStats Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<PlayerStats>();
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


    private PlayerData playerData;

    private void Start()
    {
        playerData = SavePlayer.Instance.LoadDataPlayer();
        OnCurHpChange(playerData.m_curHp, playerData.m_maxHp);
        OnCurManaChange(playerData.m_curMana, playerData.m_maxMana);
        OnCurExpChange(playerData.m_curExp, playerData.m_maxExp);
        OnCurLevelChange(playerData.m_curLevel);
    }


    public void GetHit(int damage)
    {
        playerData.m_curHp -= damage;
        OnCurHpChange(playerData.m_curHp, playerData.m_maxHp);

        if (playerData.m_curHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        playerData.m_dayCreate = DateTime.Now.ToString();
        SavePlayer.Instance.SaveDataPlayer(playerData);
    }

    public int Hp
    {
        get { return playerData.m_curHp; }
        set { playerData.m_curHp = value; }
    }

    public int Mana
    {
        get { return playerData.m_curMana; }
        set { playerData.m_curMana = value; }
    }

    public float Exp
    {
        get { return playerData.m_curExp; }
        set { playerData.m_curExp = value; OnCurExpChange(playerData.m_curExp, playerData.m_maxExp); }
    }

    public float SpeedAtk
    {
        get { return playerData.m_curSpeedAtk; }
        set { playerData.m_curSpeedAtk = value; }
    }

    public int Speed
    {
        get { return playerData.m_curSpeed; }
        set { playerData.m_curSpeed = value; }
    }

    public int Level
    {
        get { return playerData.m_curLevel; }
        set { playerData.m_curLevel = value; }
    }
}
