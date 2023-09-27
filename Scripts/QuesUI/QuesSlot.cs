using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuesSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_txtDescription;
    [SerializeField] private TextMeshProUGUI m_txtReward;
    [SerializeField] private TextMeshProUGUI m_txtLevel;
    [SerializeField] private TextMeshProUGUI m_txtQuanty;
    [SerializeField] private Image m_sprite;
    [SerializeField] private Image m_ImageComplete;

    private QuesCfg quesCfg;

    private void OnEnable()
    {
        PlayerStats.Instance.onQuantyQuesChange += UpdateQuantyEachQuestion;
    }

    public void AddQues(QuesCfg newQues)
    {
        quesCfg = newQues;
        m_sprite.sprite = quesCfg.prefab.GetComponent<SpriteRenderer>()?.sprite;
        m_txtDescription.text = quesCfg.description;
        m_txtReward.text = $"Reward: {quesCfg.coin} coin, {quesCfg.exp} exp";
        m_txtLevel.text = $"Level: {quesCfg.levelQues}";
        m_ImageComplete.color = Color.red;
        UpdateQuantyEachQuestion();
    }

    public void UpdateQuantyEachQuestion()
    {
        if (quesCfg.quanty != 0)
        {
            int indexQues = Array.IndexOf(PlayerStats.Instance.playerCfg.arr_Question, quesCfg.numberQuestion);
            int curQuantyQues = PlayerStats.Instance.playerCfg.arr_quantyQues[indexQues];

            m_txtQuanty.text = $"{curQuantyQues}/{quesCfg.quanty}";

            if (curQuantyQues >= quesCfg.quanty)
                m_ImageComplete.color = Color.green;
        }      
    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }
}
