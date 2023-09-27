using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleUI : MonoBehaviour
{
    public Action OnClickInfoSkill;

    [SerializeField] private TextMeshProUGUI m_TxtHP;
    [SerializeField] private TextMeshProUGUI m_TxtMana;
    [SerializeField] private TextMeshProUGUI m_TxtExp;
    [SerializeField] private Image m_ImgHpBar;
    [SerializeField] private Image m_ImgManaBar;
    [SerializeField] private Image m_ImgExpBar;
    [SerializeField] private TextMeshProUGUI m_TxtLevel;
    [SerializeField] private TextMeshProUGUI m_TxtCoin;


    private PlayerStats playerStats;

    private void OnEnable()
    {
        playerStats = PlayerStats.Instance;
        if (playerStats != null)
        {
            playerStats.OnCurHpChange += onCurHpChanged;
            playerStats.OnCurManaChange += onCurManaChanged;
            playerStats.OnCurExpChange += onCurExpChanged;
            playerStats.OnCurLevelChange += onCurLevelChanged;
            playerStats.OnCurCoinChange += onCurCoinChanged;
        }
    }


    private void onCurHpChanged(int curHp, int maxHp)
    {
        m_TxtHP.text = $"{curHp}/{maxHp}";
        m_ImgHpBar.fillAmount = curHp * 1f / maxHp;
    }

    private void onCurManaChanged(float curMana, float maxMana)
    {
        m_TxtMana.text = $"{(int)curMana}/{(int)maxMana}";
        m_ImgManaBar.fillAmount = (int)curMana * 1f / (int)maxMana;
    }

    private void onCurExpChanged(int curExp, int maxExp)
    {
        m_TxtExp.text = $"{curExp}/{maxExp}";
        m_ImgExpBar.fillAmount = curExp * 1f / maxExp;
    }

    private void onCurLevelChanged(int curLevel)
    {
        m_TxtLevel.text = $"{curLevel}";
    }

    private void onCurCoinChanged(int curCoin)
    {
        m_TxtCoin.text = $"{curCoin}";
    }
}
