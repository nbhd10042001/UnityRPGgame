using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Action <int, int> OnCurHpChange;
    public Action <float, float> OnCurManaChange;
    public Action <int, int> OnCurExpChange;
    public Action <int> OnCurCoinChange;
    public Action<int> OnCurLevelChange;

    public delegate void OnQuantyQuesChange();
    public OnQuantyQuesChange onQuantyQuesChange = delegate { };

    public delegate void OnLevelUp();
    public OnLevelUp onLevelUp = delegate { };

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

    [SerializeField] private TextMeshProUGUI m_txtNamePlayer;
    public PlayerCfg playerCfg;
    public PlayerInventory playerInventory;
    private HandlerCollision HandlerColli;
    private float timeReMana;
    private int damInc_temp;
    private int defInc_temp;
    private float speedRM_temp;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
        HandlerColli = GetComponent<HandlerCollision>();
        playerCfg = PlayerManager.Instance.LoadDataCurPlayer();
        playerInventory = PlayerManager.Instance.LoadInventoryCurPlayer();

        m_txtNamePlayer.text = playerCfg.name;
    }

    private void Start()
    {
        OnCurHpChange(playerCfg.curHp, playerCfg.maxHp);
        OnCurManaChange(playerCfg.curMana, playerCfg.maxMana);
        OnCurExpChange(playerCfg.curExp, playerCfg.maxExp);
        OnCurLevelChange(playerCfg.level);
        OnCurCoinChange(playerCfg.coin);
    }

    private void Update()
    {
        timeReMana += Time.deltaTime;
        if (timeReMana >= 1)
        {
            Mana += playerCfg.speed_regenMana;
            if (Mana > playerCfg.maxMana)
                Mana = playerCfg.maxMana;
            timeReMana = 0;
        }
    }
    public void saveData()
    {
        string json = JsonUtility.ToJson(playerCfg);
        File.WriteAllText(pathSave.Instance.GetPathSave_curPlayer_PlayerCfg(), json);

        json = JsonUtility.ToJson(playerInventory);
        File.WriteAllText(pathSave.Instance.GetPathSave_curPlayer_Inventory(), json);
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
            onLevelUp.Invoke();
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
        Hp = (int)(playerCfg.maxHp / 2);
    }

    private void OnApplicationQuit()
    {
        IncreaseDamage(-damInc_temp);
        IncreaseDefend(-defInc_temp);
        IncreaseSpeedRegenMana(-speedRM_temp);
    }

    public void IncreaseDamage(int damage)
    {
        if (damage > 0)
            damInc_temp = damage;
        else if (damage < 0)
            damInc_temp = 0;
        playerCfg.damage += damage;
    }

    public void IncreaseDefend(int defend)
    {
        if (defend > 0)
            defInc_temp = defend;
        else if (defend < 0)
            defInc_temp = 0;
        playerCfg.defend += defend;
    }

    public void IncreaseSpeedRegenMana(float speedRegen)
    {
        if (speedRegen > 0)
            speedRM_temp = speedRegen;
        else if (speedRegen < 0)
            speedRM_temp = 0;
        playerCfg.speed_regenMana += speedRegen;
    }

    //-------Stat Current Player------------------
    public int Hp
    {
        get { return playerCfg.curHp; }
        set 
        { 
            playerCfg.curHp = value; 
            if (playerCfg.curHp > playerCfg.maxHp)
                playerCfg.curHp = playerCfg.maxHp;
            OnCurHpChange(playerCfg.curHp, playerCfg.maxHp);
        }
    }

    public float Mana
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
