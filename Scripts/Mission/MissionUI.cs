using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissionUI : MonoBehaviour
{
    public ListQues listQues;
    private List<QuesCfg> List_Ques = new List<QuesCfg>();
    private List<MissionSlot> List_MissionSlot = new List<MissionSlot>();
    [SerializeField] private Transform m_slotMissionTransform;
    [SerializeField] private GameObject m_prefabMissionSlot;


    private void OnEnable()
    {
        PlayerStats.Instance.OnCurLevelChange += UpdateLevelOfQuestion;
        UpdateLevelOfQuestion(PlayerStats.Instance.Level);
    }


    private void UpdateLevelOfQuestion(int level)
    {
        List_Ques.Clear();
        for (int i = 0; i < listQues.List_Ques.Count; i++)
        {
            var index = Array.IndexOf(PlayerStats.Instance.playerCfg.arr_Question, listQues.List_Ques[i].numberQuestion);
            if (listQues.List_Ques[i].levelQues <= level && !PlayerStats.Instance.playerCfg.arr_isCompleteQues[index])
            {
                List_Ques.Add(listQues.List_Ques[i]);
            }
        }
        UpdateMissionUI();
    }

    private void UpdateMissionUI()
    {
        int curQuesCount = List_Ques.Count;

        if (curQuesCount > List_MissionSlot.Count)
        {
            //add more item slots
            AddMissionSlots(curQuesCount);
        }

        for (int i = 0; i < List_MissionSlot.Count; i++)
        {
            if (i < curQuesCount)
            {
                List_MissionSlot[i].AddQues(List_Ques[i]);
            }
            else
            {
                List_MissionSlot[i].DestroySlot();
                List_MissionSlot.RemoveAt(i);
            }
        }
    }

    private void AddMissionSlots(int curQuesCount)
    {
        int amount = curQuesCount - List_MissionSlot.Count;
        for (int i = 0; i < amount; i++)
        {
            GameObject gameObj = Instantiate(m_prefabMissionSlot, m_slotMissionTransform);
            MissionSlot newSlot = gameObj.GetComponent<MissionSlot>();
            List_MissionSlot.Add(newSlot);
        }
    }
}
