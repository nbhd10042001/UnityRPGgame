using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using TMPro;



public class PlayerStats : MonoBehaviour
{
    public Action <int, int> OnCurHpChange;
    public Action <int, int> OnCurManaChange;
    public Action <int, int> OnCurExpChange;
    public Action <int> OnCurLevelChange;
    public Action <int> OnCurCoinChange;
    public Action OnQuantyQuesChange;

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

    public PlayerCfg playerCfg;
    public PlayerInventory playerInventory;
    private HandlerCollision HandlerColli;
    private float timeReMana;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

        playerCfg = PlayerManager.Instance.LoadDataCurPlayer();
        playerInventory = PlayerManager.Instance.LoadInventoryCurPlayer();
        HandlerColli = GetComponent<HandlerCollision>();
    }

    private void Start()
    {
        OnCurHpChange(playerCfg.curHp, playerCfg.maxHp);
        OnCurManaChange(playerCfg.curMana, playerCfg.maxMana);
        OnCurExpChange(playerCfg.curExp, playerCfg.maxExp);
        OnCurLevelChange(playerCfg.level);
        OnCurCoinChange(playerCfg.coin);
        Vector3 posPlayer = PlayerStats.Instance.transform.position;
        Vector3 pos = new Vector3(posPlayer.x, posPlayer.y - 1, 0);
    }

    private void Update()
    {
        timeReMana += Time.deltaTime * playerCfg.speed_regenMana; 
        if (timeReMana >= 1)
        {
            Mana += 1;
            if (Mana > playerCfg.maxMana)
                Mana -= 1;
            timeReMana = 0f;
        }
    }

    public void GetExp(int exp)
    {
        Exp += exp;
        while (Exp > playerCfg.maxExp)
        {
            int redundant_exp = Exp - playerCfg.maxExp;
            playerInventory.LevelUp();
            playerCfg.LevelUp();
            Hp = playerCfg.maxHp;
            Mana = playerCfg.maxMana;
            
            Exp = redundant_exp;
            Level += 1;
        }
    }

    public void GetHit(int damage)
    {
        Hp -= (damage - playerCfg.defend);
        HandlerColli.StartCoroutineGetHitFX();

        if (Hp <= 0)
        {
            GamePlayManager.Instance.StartCoroutinTextPlayerDead();
            Invoke("SetActivePlayer", 5);
            gameObject.SetActive(false);
        }
    }

    private void SetActivePlayer()
    {
        gameObject.SetActive(true);
        Hp = playerCfg.maxHp;
    }

    public void saveData ()
    {
        string json = JsonUtility.ToJson(playerCfg);
        File.WriteAllText(pathSave.Instance.GetPathSave_curPlayer_PlayerCfg(), json);

        json = JsonUtility.ToJson(playerInventory);
        File.WriteAllText(pathSave.Instance.GetPathSave_curPlayer_Inventory(), json);
    }

    private void OnApplicationQuit()
    {
        saveData();
    }

    //-------Stat Current Player------------------
    public int Hp
    {
        get { return playerCfg.curHp; }
        set { playerCfg.curHp = value; OnCurHpChange(playerCfg.curHp, playerCfg.maxHp); }
    }

    public int Mana
    {
        get { return playerCfg.curMana; }
        set { playerCfg.curMana = value; OnCurManaChange(playerCfg.curMana, playerCfg.maxMana); }
    }

    public int Exp
    {
        get { return playerCfg.curExp; }
        set { playerCfg.curExp = value; OnCurExpChange(playerCfg.curExp, playerCfg.maxExp); }
    }

    public float SpeedAtk
    {
        get { return playerCfg.curSpeedAtk; }
        set { playerCfg.curSpeedAtk = value; }
    }

    public float Speed
    {
        get { return playerCfg.curSpeed; }
        set { playerCfg.curSpeed = value; }
    }

    public int Level
    {
        get { return playerCfg.level; }
        set { playerCfg.level = value; OnCurLevelChange(playerCfg.level); }
    }

    public int Coin
    {
        get { return playerCfg.coin; }
        set { playerCfg.coin = value; OnCurCoinChange(playerCfg.coin); }
    }
}
