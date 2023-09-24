using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEngine.UI;


public enum State { Diaglog, Battle, Setting, Save, Load, Volume}

public class GamePlayManager : MonoBehaviour
{
    private static GamePlayManager m_Instance;
    public static GamePlayManager Instance  
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<GamePlayManager>();
            return m_Instance;
        }
    }

    public delegate void OnActiveFalsePaddingSlot();
    public OnActiveFalsePaddingSlot onActiveFalsePaddingSlot = delegate { };

    [SerializeField] private GameObject m_DialogPng;
    [SerializeField] private GameObject m_UIselectNPC;
    [SerializeField] private GameObject m_BoxNameNPC;
    [SerializeField] private GameObject m_SettingUI;
    [SerializeField] private GameObject m_BattleUI;
    [SerializeField] private GameObject m_QuestionUI;
    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private GameObject m_MissionUI;
    [SerializeField] private GameObject m_BodyEquipUI;

    [SerializeField] private GameObject m_InfoMaterialTab;
    [SerializeField] private TextMeshProUGUI m_txtInfoMaterial;
    [SerializeField] private InputField m_inputDrop;
    [SerializeField] private Image m_btnEquipment;


    [SerializeField] private Transform m_PointToGoDarkSky;
    [SerializeField] private Transform m_PointToGoSurvival;
    [SerializeField] private Transform m_PointHome;
    [SerializeField] private Transform m_PointTransfer1;
    [SerializeField] private Transform m_PointTransfer2;

    [SerializeField] private TextMeshProUGUI m_nameP;
    [SerializeField] private Text m_nameNPC;
    [SerializeField] private TextMeshProUGUI m_TxtDead;

    [SerializeField] private TextMeshProUGUI m_TxtWarning;


    private State m_curState;
    private PlayerCfg playerCfg;
    private SaveLoadManager m_SLmanager;
    private VolumeManager m_VolumeManager;
    private string nameNpc;
    private Item itemChoose;
    private int amountItemDrop;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

    }

    private void Start()
    {
        SetState(State.Battle);
        m_SLmanager = GetComponent<SaveLoadManager>();
        m_VolumeManager = GetComponent<VolumeManager>();

        playerCfg = PlayerManager.Instance.LoadDataCurPlayer();
        m_nameP.text = playerCfg.name;
    }

    private void SetState (State state)
    {
        m_curState = state;

        m_BattleUI.SetActive(m_curState == State.Battle);
        m_DialogPng.SetActive(m_curState == State.Diaglog);
        m_SettingUI.SetActive(m_curState == State.Setting);
    }

    public void ShowDialogUI(bool isShow)
    {
        if (isShow)
            SetState(State.Diaglog);
        else
            SetState(State.Battle);
    }

    public void ShowBoxNameNPC(bool isShow, string name)
    {
        m_BoxNameNPC.SetActive(isShow);
        m_nameNPC.text = $"NPC {name}";
    }

    public void ShowUINpc(bool isShow, string name)
    {
        nameNpc = name;
        if (isShow)
        {
            m_UIselectNPC.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            m_UIselectNPC.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void BtnTravel_Pressed()
    {
        if (nameNpc == "Travel")
        {
            if (PlayerStats.Instance.Level >= 5)
            {
                Time.timeScale = 1;
                m_UIselectNPC.SetActive(false);
                PlayerController.Instance.MovePlayerToOtherPoint(m_PointToGoDarkSky);
            }
            else
                SetTextWarning("Your level need over 5 to travel to DarkSky!");
        }

        else if (nameNpc == "ModeSurvival")
        {
            if (PlayerStats.Instance.Level >= 8)
            {
                Time.timeScale = 1;
                m_UIselectNPC.SetActive(false);
                PlayerController.Instance.MovePlayerToOtherPoint(m_PointToGoSurvival);
            }
            else
                SetTextWarning("Your level need over 8 to travel to Survival!");
        }
    }

    public void Btnleave_Pressed()
    {
        ShowUINpc(false, null);
    }

    private IEnumerator TextPlayerDead()
    {
        m_TxtDead.gameObject.SetActive(true);
        float count = 0f;
        while (true)
        {
            count += Time.deltaTime;
            m_TxtDead.text = $"You dead !!! Respawm in {5 - (int)count}s";

            if (count >= 5f)
                break;
            yield return null;
        }
        m_TxtDead.gameObject.SetActive(false);
    }

    public void StartCoroutinTextPlayerDead()
    {
        StartCoroutine(TextPlayerDead());
    }

    public void SetTextWarning (string text)
    {
        m_TxtWarning.gameObject.SetActive(true);
        m_TxtWarning.text = text;
        Invoke("DeActive_txtWarning", 1);
    }

    private void DeActive_txtWarning()
    {
        m_TxtWarning.gameObject.SetActive(false);
    }

    #region -----body equipment----------
    public void BtnOpenBodyEquip()
    {
        m_BodyEquipUI.SetActive(true);
        BtnCloseQuestionUI();
    }

    public void BtnCloseBodyEquip()
    {
        m_BodyEquipUI.SetActive(false);
    }

    public void BtnEquipment_Pressed()
    {
        if (itemChoose != null && itemChoose.isEquipment)
        {
            if (itemChoose.slot == 1)
                PlayerStats.Instance.playerCfg.slotEquipment_1 = itemChoose.name;
            else if (itemChoose.slot == 2)
                PlayerStats.Instance.playerCfg.slotEquipment_2 = itemChoose.name;
            else if (itemChoose.slot == 3)
                PlayerStats.Instance.playerCfg.slotEquipment_3 = itemChoose.name;
            else if (itemChoose.slot == 4)
                PlayerStats.Instance.playerCfg.slotEquipment_4 = itemChoose.name;

            PlayerStats.Instance.playerCfg.UpdateItemEquipment();
            BodyEquipmentUI.Instance.UpdateItemEquip();
        }
    }

    #endregion

    #region UI Inventory
    public void BtnOpenInventoryUI()
    {
        m_InventoryUI.SetActive(true);
    }

    public void BtnCloseInventoryUI()
    {
        m_InventoryUI.SetActive(false);
        BtnCloseInfoMaterialTab();
    }

    public void ShowInfoMaterialTab(string name, bool isEquipment)
    {
        m_InfoMaterialTab.SetActive(true);
        m_txtInfoMaterial.text = $"Name: {name}";
        if (isEquipment)
            m_btnEquipment.color = Color.white;
        else
            m_btnEquipment.color = Color.gray;

    }

    public void BtnCloseInfoMaterialTab()
    {
        m_InfoMaterialTab.SetActive(false);
        onActiveFalsePaddingSlot.Invoke();
    }

    public void SetItemSlot_Pressed(Item item)
    {
        itemChoose = item;
        m_inputDrop.text = null;
    }

    public void btn_DropPressed()
    {
        if (!string.IsNullOrEmpty(m_inputDrop.text))
        {
            if (itemChoose != null)
            {
                amountItemDrop = int.Parse(m_inputDrop.text);
                if (amountItemDrop >= PlayerStats.Instance.playerInventory.GetAmountItem(itemChoose.name))
                {
                    amountItemDrop = PlayerStats.Instance.playerInventory.GetAmountItem(itemChoose.name);
                    PlayerStats.Instance.playerCfg.ResetSlot(itemChoose.slot);
                    PlayerStats.Instance.playerCfg.UpdateItemEquipment();
                    BodyEquipmentUI.Instance.UpdateItemEquip();

                    BtnCloseInfoMaterialTab();
                }

                if (!itemChoose.isEquipment)
                    for (int i = 0; i < amountItemDrop; i++)
                        SpawmManager.Instance.SpawmOre(PlayerController.Instance.transform.position, itemChoose.name, false);
                else if (itemChoose.isEquipment)
                    for (int i = 0; i < amountItemDrop; i++)
                        SpawmManager.Instance.SpawmSword(PlayerController.Instance.transform.position, itemChoose.name);

                PlayerStats.Instance.playerInventory.AddItem(itemChoose.name, -amountItemDrop);
                PlayerStats.Instance.playerInventory.UpdateCurCapacity();
                Inventory.Instance.UpdateList();
            }
        }
    }
    #endregion

    #region Mission/Question
    public void BtnOpenQuestionUI()
    {
        m_QuestionUI.SetActive(true);
        BtnCloseBodyEquip();
    }

    public void BtnCloseQuestionUI()
    {
        m_QuestionUI.SetActive(false);
    }

    public void OpenMissionUI()
    {
        m_MissionUI.SetActive(true);
        BtnCloseInventoryUI();
        BtnCloseQuestionUI();
        BtnCloseBodyEquip();
        Time.timeScale = 0;
    }

    public void BtnCloseMissionUI()
    {
        m_MissionUI.SetActive(false);
        Time.timeScale = 1;

    }
    #endregion

    #region Button int Setting
    public void BtnSetting_Pressed()
    {
        Time.timeScale = 0;
        SetState(State.Setting);
        BtnCloseInventoryUI();
        BtnCloseQuestionUI();
        BtnCloseBodyEquip();
    }

    public void Btn_GoToHomePoint()
    {
        PlayerController.Instance.MovePlayerToOtherPoint(m_PointHome);
        Time.timeScale = 1;
        BtnCancel_Pressed();
    }

    public void BtnBack_Pressed()
    {
        SetState(State.Setting);
        m_SLmanager.SetUI_Save(false);
        m_SLmanager.SetUI_Load(false);
        m_VolumeManager.SetVolumeUI(false);
    }

    public void BtnCancel_Pressed()
    {
        Time.timeScale = 1;
        SetState(State.Battle);
    }

    public void BtnMenu_Pressed()
    {
        PlayerStats.Instance.saveData();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void BtnVolume_Pressed()
    {
        SetState(State.Volume);
        m_VolumeManager.SetVolumeUI(true);

    }

    public void BtnSave_Pressed()
    {
        SetState(State.Save);
        m_SLmanager.SetUI_Save(true);

    }

    public void BtnLoad_Pressed()
    {
        SetState(State.Load);
        m_SLmanager.SetUI_Load(true);

    }
    #endregion

    public void goHomeToLoadPlayer()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }
}
