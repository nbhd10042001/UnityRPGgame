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
    private void SaveCharacter()
    {
        SavePlayer.Instance.SetSlot(slotSave);

        //create a new hero on Scene Menu
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            PlayerData playerData = new PlayerData();
            playerData.InitStats();
            playerData.name = m_UImanager.getCurNameHero();
            playerData.m_dayCreate = DateTime.Now.ToString();
            SavePlayer.Instance.SaveDataPlayer(playerData);

            m_UImanager.SetNameCharMenu();
            StartCoroutine(WaitUntilFileExist());
        }

        if (SceneManager.GetActiveScene().name == "Battle" || SceneManager.GetActiveScene().name == "Home")
        {
            PlayerStats.Instance.SaveData();
            StartCoroutine(WaitUntilFileExist());
        }
    }

    private IEnumerator WaitUntilFileExist()
    {
        while (true)
        {
            if (File.Exists(Application.dataPath + $"/save/{slotSave}/saveFile.json"))
            {
                if (SceneManager.GetActiveScene().name == "Menu")
                    m_UImanager.SetUIMenu();
                if (SceneManager.GetActiveScene().name == "Battle" || SceneManager.GetActiveScene().name == "Home")
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
        if (File.Exists(Application.dataPath + $"/save/{slotLoad}/saveFile.json"))
        {
            SavePlayer.Instance.SetSlot(slotLoad);
            PlayerData playerData = SavePlayer.Instance.LoadDataPlayer();

            if (SceneManager.GetActiveScene().name == "Menu")
            {
                m_UImanager.setCurNameHero(playerData.name);
                m_UImanager.SetUIMenu();
                m_UImanager.SetNameCharMenu();
            }

            else if (SceneManager.GetActiveScene().name == "Battle" || SceneManager.GetActiveScene().name == "Home")
            {
                m_gpManager.LoadNewFilePlayer();
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
        SavePlayer.Instance.SetSlotDelete(slotDelete);
        SavePlayer.Instance.DeleteDataPlayer();
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            m_UImanager.setCurNameHero(null);
            m_UImanager.SetUIMenu();
            m_UImanager.SetNameCharMenu();
        }
    }
}
