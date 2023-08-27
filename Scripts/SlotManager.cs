using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_name;
    [SerializeField] private TextMeshProUGUI m_day;
    [SerializeField] private string m_slot;


    private PlayerData playerData;

    private void Update()
    {
        if (File.Exists(Application.dataPath + $"/save/{m_slot}/saveFile.json"))
        {
            string json = File.ReadAllText(Application.dataPath + $"/save/{m_slot}/saveFile.json");
            playerData = JsonUtility.FromJson<PlayerData>(json);
            m_name.text = playerData.name;
            m_day.text = playerData.m_dayCreate;
        }
        else
        {
            m_name.text = null;
            m_day.text = null;
        }
    }
}
