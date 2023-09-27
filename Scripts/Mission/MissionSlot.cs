using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MissionSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_txtDescription;
    [SerializeField] private TextMeshProUGUI m_txtReward;
    [SerializeField] private TextMeshProUGUI m_txtLevel;
    [SerializeField] private TextMeshProUGUI m_txtQuanty;
    [SerializeField] private Image m_sprite;
    [SerializeField] private Image m_ImageComplete;
    [SerializeField] private Image m_paddingBox;
    [SerializeField] private Image m_bgBox;

    private QuesCfg quesCfg;
    private bool isComplete;

    private void OnEnable()
    {
        PlayerStats.Instance.onQuantyQuesChange += UpdateQuantyEachQuestion;
    }


    public void AddQues(QuesCfg newQues)
    {
        quesCfg = newQues;
        m_sprite.sprite = newQues.prefab.GetComponent<SpriteRenderer>()?.sprite;
        m_txtDescription.text = newQues.description;
        m_txtReward.text = $"Reward: {newQues.coin} coin, {newQues.exp} exp";
        m_txtLevel.text = $"Level: {newQues.levelQues}";
        SetColorRed();
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
                SetColorGreen();
        }
            
    }

    public void btn_GetRewardMission()
    {
        if (isComplete)
        {
            int index_Ques = Array.IndexOf(PlayerStats.Instance.playerCfg.arr_Question, quesCfg.numberQuestion);
            PlayerStats.Instance.playerCfg.arr_isCompleteQues[index_Ques] = true;
            PlayerStats.Instance.GetExp(quesCfg.exp);
            PlayerStats.Instance.Coin += quesCfg.coin;
            PlayerStats.Instance.OnCurLevelChange(PlayerStats.Instance.Level);
        }
        
    }

    private void SetColorGreen()
    {
        Color transparent = Color.green;
        transparent.a = 0.2f;
        m_bgBox.color = transparent;

        m_ImageComplete.color = Color.green;
        m_paddingBox.color = Color.green;
        isComplete = true;
    }

    private void SetColorRed()
    {
        Color transparent = Color.red;
        transparent.a = 0.2f;
        m_bgBox.color = transparent;

        m_ImageComplete.color = Color.red;
        m_paddingBox.color = Color.red;
        isComplete = false;
    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }
}
