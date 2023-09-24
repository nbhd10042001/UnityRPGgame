using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuesUI : MonoBehaviour
{
    public ListQues listQues;
    private List<QuesCfg> List_Ques = new List<QuesCfg>();
    private List<QuesSlot> List_QuesSlot = new List<QuesSlot>();
    [SerializeField] private Transform m_slotQuesTransform;
    [SerializeField] private GameObject m_prefabSlotQues;


    private void OnEnable()
    {
        PlayerStats.Instance.OnCurLevelChange += UpdateLevelOfQuestion;
    }

    private void Start()
    {
        UpdateLevelOfQuestion(PlayerStats.Instance.Level);
    }


    private void UpdateLevelOfQuestion(int level)
    {
        List_Ques.Clear();

        //check level player >= level ques and ques not complete >>>>> add ques to List Ques
        for (int i = 0; i < listQues.List_Ques.Count; i++)
        {
            var index = Array.IndexOf(PlayerStats.Instance.playerCfg.arr_Question, listQues.List_Ques[i].numberQuestion);
            if (listQues.List_Ques[i].levelQues <= level && !PlayerStats.Instance.playerCfg.arr_isCompleteQues[index])
            {
                List_Ques.Add(listQues.List_Ques[i]);
            }
        }
        UpdateQuestionUI();
    }

    private void UpdateQuestionUI()
    {
        int curQuesCount = List_Ques.Count;
        if (curQuesCount > List_QuesSlot.Count)
        {
            //add more item slots
            AddQuesSlots(curQuesCount);
        }

        for (int i = 0; i < List_QuesSlot.Count; i++)
        {
            if (i < curQuesCount)
            {
                List_QuesSlot[i].AddQues(List_Ques[i]);
            }
            else
            {
                List_QuesSlot[i].DestroySlot();
                List_QuesSlot.RemoveAt(i);
            }
        }
    }

    private void AddQuesSlots(int curQuesCount)
    {
        int amount = curQuesCount - List_QuesSlot.Count;
        for (int i = 0; i < amount; i++)
        {
            GameObject gameObj = Instantiate(m_prefabSlotQues, m_slotQuesTransform);
            QuesSlot newSlot = gameObj.GetComponent<QuesSlot>();
            List_QuesSlot.Add(newSlot);
        }
    }

}
