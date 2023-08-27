using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TxtHP;
    [SerializeField] private TextMeshProUGUI m_TxtMana;
    [SerializeField] private TextMeshProUGUI m_TxtExp;
    [SerializeField] private Image m_ImgHpBar;
    [SerializeField] private Image m_ImgManaBar;
    [SerializeField] private Image m_ImgExpBar;
    [SerializeField] private TextMeshProUGUI m_TxtLevel;

    private PlayerStats playerStats;

    private void OnEnable()
    {
        playerStats = PlayerStats.Instance;

        if (playerStats != null)
        {
            playerStats.OnCurHpChange += oonCurHpChanged;
            playerStats.OnCurManaChange += oonCurManaChanged;
            playerStats.OnCurExpChange += oonCurExpChanged;
            playerStats.OnCurLevelChange += oonCurLevelChanged;
        }
    }

    private void OnDisable()
    {
        if (playerStats != null)
        {
            playerStats.OnCurHpChange -= oonCurHpChanged;
            playerStats.OnCurManaChange -= oonCurManaChanged;
            playerStats.OnCurExpChange -= oonCurExpChanged;
            playerStats.OnCurLevelChange -= oonCurLevelChanged;
        }
    }

    private void oonCurHpChanged(int curHp, int maxHp)
    {
        m_TxtHP.text = $"{curHp}/{maxHp}";
        m_ImgHpBar.fillAmount = curHp * 1f / maxHp;
    }

    private void oonCurManaChanged(int curMana, int maxMana)
    {
        m_TxtMana.text = $"{curMana}/{maxMana}";
        m_ImgManaBar.fillAmount = curMana * 1f / maxMana;
    }

    private void oonCurExpChanged(float curExp, float maxExp)
    {
        m_TxtExp.text = $"{curExp}/{maxExp}";
        m_ImgExpBar.fillAmount = curExp * 1f / maxExp;
    }

    private void oonCurLevelChanged(int curLevel)
    {
        m_TxtLevel.text = $"{curLevel}";
    }
}
