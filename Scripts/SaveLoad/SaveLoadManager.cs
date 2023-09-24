using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private GameObject m_SaveUI;
    [SerializeField] private GameObject m_LoadUI;

    private UIManager m_UImanager;
    private GamePlayManager m_gpManager;
    private string slotSave;
    private string slotLoad;
    private string slotDelete;


    private void Start()
    {
        m_UImanager = GetComponent<UIManager>();
        m_gpManager = GetComponent<GamePlayManager>();
    }

    public void SetUI_Save(bool isAct)
    {
        m_SaveUI.SetActive(isAct);
    }

    public void SetUI_Load(bool isAct)
    {
        m_LoadUI.SetActive(isAct);
    }

    //---------button save --------------
    public void btnSaveSlot1()
    {
        slotSave = "slot1";
        SaveCharacter();
    }

    public void btnSaveSlot2()
    {
        slotSave = "slot2";
        SaveCharacter();
    }

    public void btnSaveSlot3()
    {
        slotSave = "slot3";
        SaveCharacter();
    }

    public void CreateNewPlayer()
    {
        PlayerCfg newPlayerCfg = new PlayerCfg();
        newPlayerCfg.SetDefaultCfg();
        newPlayerCfg.name = m_UImanager.getCurNameHero();
        newPlayerCfg.dayCreate = DateTime.Now.ToString();

        string json = JsonUtility.ToJson(newPlayerCfg);
        File.WriteAllText(pathSave.Instance.GetPathSave_curPlayer_PlayerCfg(), json);

        PlayerInventory newInvenPlayer = new PlayerInventory();
        newInvenPlayer.SetDefault();
        json = JsonUtility.ToJson(newInvenPlayer);
        File.WriteAllText(pathSave.Instance.GetPathSave_curPlayer_Inventory(), json);

        m_UImanager.SetNameCharMenu();
        StartCoroutine(WaitUntilFileCurExist());
    }

    private void SaveCharacter()
    {
        PlayerManager.Instance.SetSlot(slotSave);
        PlayerManager.Instance.SaveDataPlayer();
        StartCoroutine(WaitUntilFileSaveExist());
    }

    private IEnumerator WaitUntilFileCurExist()
    {
        while (true)
        {
            if (File.Exists(pathSave.Instance.GetPathSave_curPlayer_PlayerCfg()))
            {
                if (SceneManager.GetActiveScene().name == "Menu")
                    m_UImanager.SetUIMenu();
                else
                    m_gpManager.BtnBack_Pressed();
                break;
            }
            yield return null;
        }
    }

    private IEnumerator WaitUntilFileSaveExist()
    {
        while (true)
        {
            if (File.Exists(pathSave.Instance.GetPathSave_slot_PlayerCfg(slotSave)))
            {
                if (SceneManager.GetActiveScene().name == "Menu")
                    m_UImanager.SetUIMenu();
                else
                    m_gpManager.BtnBack_Pressed();

                break;
            }
            yield return null;
        }
    }

    //-------------button Load ------------

    public void btnLoadSlot1()
    {
        slotLoad = "slot1";
        LoadCharacter();
    }

    public void btnLoadSlot2()
    {
        slotLoad = "slot2";
        LoadCharacter();
    }

    public void btnLoadSlot3()
    {
        slotLoad = "slot3";
        LoadCharacter();
    }
    private void LoadCharacter()
    {
        if (File.Exists(pathSave.Instance.GetPathSave_slot_PlayerCfg(slotLoad)))
        {
            PlayerManager.Instance.SetSlot(slotLoad);
            PlayerManager.Instance.LoadDataPlayer();

            PlayerCfg playerCfg = PlayerManager.Instance.LoadDataCurPlayer();

            if (SceneManager.GetActiveScene().name == "Menu")
            {
                m_UImanager.setCurNameHero(playerCfg.name);
                m_UImanager.SetUIMenu();
                m_UImanager.SetNameCharMenu();
            }

            else
            {
                m_gpManager.goHomeToLoadPlayer();
            }
        }
    }

    //---------button delete-----------
    public void btnDelSlot1()
    {
        slotDelete = "slot1";
        DeleteCharacter();
    }

    public void btnDelSlot2()
    {
        slotDelete = "slot2";
        DeleteCharacter();
    }

    public void btnDelSlot3()
    {
        slotDelete = "slot3";
        DeleteCharacter();
    }

    private void DeleteCharacter()
    {
        PlayerManager.Instance.SetSlotDelete(slotDelete);
        PlayerManager.Instance.DeleteDataPlayer();
    }
}
