using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SavePlayer : MonoBehaviour
{
    private static SavePlayer m_Instance;
    public static SavePlayer Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<SavePlayer>();
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }

    private string m_curSlot;
    private string m_curSlotDelete;

    public void SetSlot(string slot)
    {
        PlayerPrefs.SetString("slot", slot);
    }

    public void SetSlotDelete(string slot)
    {
        m_curSlotDelete = slot;
    }

    public PlayerData LoadDataPlayer()
    {
        m_curSlot = PlayerPrefs.GetString("slot");
        string json = File.ReadAllText(Application.dataPath + $"/save/{m_curSlot}/saveFile.json");
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        return playerData;
    }

    public void SaveDataPlayer(PlayerData pD)
    {
        m_curSlot = PlayerPrefs.GetString("slot");
        string json = JsonUtility.ToJson(pD);
        File.WriteAllText(Application.dataPath + $"/save/{m_curSlot}/saveFile.json", json);
    }

    public void DeleteDataPlayer()
    {
        File.Delete(Application.dataPath + $"/save/{m_curSlotDelete}/saveFile.json");
        File.Delete(Application.dataPath + $"/save/{m_curSlotDelete}/saveFile.json.meta");
    }
}
