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


    private void Update()
    {
        if (File.Exists(pathSave.Instance.GetPathSave_slot_PlayerCfg(m_slot)))
        {
            string json = File.ReadAllText(pathSave.Instance.GetPathSave_slot_PlayerCfg(m_slot));
            PlayerCfg playerCfg = new PlayerCfg();
            playerCfg = JsonUtility.FromJson<PlayerCfg>(json);

            m_name.text = playerCfg.name;
            m_day.text = playerCfg.dayCreate;
        }
        else
        {
            m_name.text = null;
            m_day.text = null;
        }
    }
}
