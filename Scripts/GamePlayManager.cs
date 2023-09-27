using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
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
    public Action OnItemEquipmentChange;

    [SerializeField] private GameObject m_DialogPng;
    [SerializeField] private GameObject m_UIselectNPC;
    [SerializeField] private GameObject m_BoxNameNPC;
    [SerializeField] private GameObject m_SettingUI;
    [SerializeField] private GameObject m_BattleUI;
    [SerializeField] private GameObject m_QuestionUI;
    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private GameObject m_MissionUI;
    [SerializeField] private GameObject m_BodyEquipUI;
    [SerializeField] private GameObject m_ShopUI;
    [SerializeField] private GameObject m_UpgradeUI;

    [SerializeField] private GameObject m_InfoMaterialTab;
    [SerializeField] private TextMeshProUGUI m_txtInfoMaterial;
    [SerializeField] private InputField m_inputDrop;
    [SerializeField] private InputField m_inputSell;
    [SerializeField] private Image m_btnSell;
    [SerializeField] private Image m_btnEquipment;
    [SerializeField] private Image m_btnMobileInteract;


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

    private void Update()
    {
        if (m_ShopUI.gameObject.activeSelf == true)
            m_btnSell.color = Color.white;
        else
            m_btnSell.color = Color.gray;

    }

    private void SetState (State state)
    {
        m_curState = state;

        m_BattleUI.SetActive(m_curState == State.Battle);
        m_DialogPng.SetActive(m_curState == State.Diaglog);
        m_SettingUI.SetActive(m_curState == State.Setting);
    }

    #region ------------------ show text/dialog/warning -----------------
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
    #endregion

    // function update ------------
    private void UpdateInventoryPlayer(string name, int amount)
    {
        PlayerStats.Instance.playerInventory.AddItem(name, amount);
        PlayerStats.Instance.playerInventory.UpdateCurCapacity();
        Inventory.Instance.UpdateList();
    }

    private void UpdateItemEquipment()
    {
        PlayerStats.Instance.playerCfg.UpdateItemEquipment();
        BodyEquipmentUI.Instance.UpdateItemEquip();
        OnItemEquipmentChange();
    }


    #region ------------------- body equipment----------------------
    public void BtnOpenBodyEquip()
    {
        m_BodyEquipUI.SetActive(true);
        BtnCloseQuestionUI();
        btnCloseUpgradeUI();
        btnCloseShop_Pressed();
    }

    public void BtnCloseBodyEquip()
    {
        m_BodyEquipUI.SetActive(false);
    }

    public void BtnEquipment_Pressed()
    {
        if (itemChoose != null && itemChoose.isEquipment)
        {
            if (PlayerStats.Instance.Level >= itemChoose.requiredLevel)
            {
                if (itemChoose.slot == 1)
                {
                    if (PlayerStats.Instance.playerCfg.slotEquipment_1 != null)
                        UpdateInventoryPlayer(PlayerStats.Instance.playerCfg.slotEquipment_1, 1);
                    PlayerStats.Instance.playerCfg.slotEquipment_1 = itemChoose.name;
                }
                else if (itemChoose.slot == 2)
                {
                    if (PlayerStats.Instance.playerCfg.slotEquipment_2 != null)
                        UpdateInventoryPlayer(PlayerStats.Instance.playerCfg.slotEquipment_2, 1);
                    PlayerStats.Instance.playerCfg.slotEquipment_2 = itemChoose.name;
                }

                UpdateInventoryPlayer(itemChoose.name, -1);
                UpdateItemEquipment();
            }
            else
            {
                string text = "Your level not enough to equipment!";
                SetTextWarning(text);
            }
        }
    }

    public void btnReEquipmentItem_1_Pressed()
    {
        UpdateInventoryPlayer(PlayerStats.Instance.playerCfg.slotEquipment_1, 1);
        PlayerStats.Instance.playerCfg.slotEquipment_1 = null;
        UpdateItemEquipment();
    }

    public void btnReEquipmentItem_2_Pressed()
    {
        UpdateInventoryPlayer(PlayerStats.Instance.playerCfg.slotEquipment_2, 1);
        PlayerStats.Instance.playerCfg.slotEquipment_2 = null;
        UpdateItemEquipment();
    }

    #endregion

    #region-------------------- UI Inventory----------------------------
    public void BtnOpenInventoryUI()
    {
        m_InventoryUI.SetActive(true);
    }

    public void BtnCloseInventoryUI()
    {
        m_InventoryUI.SetActive(false);
        BtnCloseInfoMaterialTab();
    }

    public void ShowInfoMaterialTab(Item item)
    {
        m_InfoMaterialTab.SetActive(true);
        if (item.isEquipment)
        {
            m_btnEquipment.color = Color.white;
            m_txtInfoMaterial.text = $"Name: {item.name}\n" +
                $"Price: {item.price}\n" +
                $"Damage: {item.damage}\n" +
                $"Defend: {item.defend}\n" +
                $"Required Level: {item.requiredLevel}";
        }
        else
        {
            m_btnEquipment.color = Color.gray;
            m_txtInfoMaterial.text = $"Name: {item.name}\n" +
                $"Price: {item.price}";
        }

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
                    BtnCloseInfoMaterialTab();
                }

                // spawm = amount drop
                if (!itemChoose.isEquipment)
                    for (int i = 0; i < amountItemDrop; i++)
                        SpawmManager.Instance.SpawmOre(PlayerController.Instance.transform.position, itemChoose.name, false);
                else if (itemChoose.isEquipment)
                    if (itemChoose.slot == 1)
                        for (int i = 0; i < amountItemDrop; i++)
                            SpawmManager.Instance.SpawmSword(PlayerController.Instance.transform.position, itemChoose.name);
                if (itemChoose.slot == 2)
                    for (int i = 0; i < amountItemDrop; i++)
                        SpawmManager.Instance.SpawmShield(PlayerController.Instance.transform.position, itemChoose.name);

                UpdateInventoryPlayer(itemChoose.name, -amountItemDrop);
            }
        }
    }
    #endregion

    #region -------------------Mission/Question-----------------------
    public void BtnOpenQuestionUI()
    {
        m_QuestionUI.SetActive(true);
        BtnCloseBodyEquip();
        btnCloseUpgradeUI();
        btnCloseShop_Pressed();
    }

    public void BtnCloseQuestionUI()
    {
        m_QuestionUI.SetActive(false);
    }

    public void CloseAllUI()
    {
        BtnCloseInventoryUI();
        BtnCloseQuestionUI();
        BtnCloseBodyEquip();
        btnCloseShop_Pressed();
        btnCloseUpgradeUI();
    }

    public void OpenMissionUI()
    {
        m_MissionUI.SetActive(true);
        CloseAllUI();
        Time.timeScale = 0;
    }

    public void BtnCloseMissionUI()
    {
        m_MissionUI.SetActive(false);
        Time.timeScale = 1;

    }
    #endregion

    #region ----------------Button int Setting------------------------
    public void BtnSetting_Pressed()
    {
        Time.timeScale = 0;
        SetState(State.Setting);
        CloseAllUI();
    }

    public void Btn_GoToHomePoint()
    {
        PlayerController.Instance.MovePlayerToOtherPoint(m_PointHome);
        Time.timeScale = 1;
        BtnCloseSettingUI_Pressed();
    }

    public void BtnBack_Pressed()
    {
        SetState(State.Setting);
        m_SLmanager.SetUI_Save(false);
        m_SLmanager.SetUI_Load(false);
        m_VolumeManager.SetVolumeUI(false);
    }

    public void BtnCloseSettingUI_Pressed()
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

    public void btnOpenUpgradeUI()
    {
        m_UpgradeUI.SetActive(true);
        BtnCloseQuestionUI();
        BtnCloseBodyEquip();
    }

    public void btnCloseUpgradeUI()
    {
        m_UpgradeUI.SetActive(false);
    }

    public void btnSellPressed()
    {
        if (m_ShopUI.gameObject.activeSelf == true)
        {
            int amountSell = 0;
            if (!string.IsNullOrEmpty(m_inputSell.text))
            {
                amountSell = int.Parse(m_inputSell.text);
                if (itemChoose != null)
                {
                    if (amountSell <= PlayerStats.Instance.playerInventory.GetAmountItem(itemChoose.name))
                    {
                        UpdateInventoryPlayer(itemChoose.name, -amountSell);
                        for (int i = 0; i < amountSell; i++)
                            PlayerStats.Instance.Coin += itemChoose.price;
                    }
                }
            }
        }
        else
            SetTextWarning("You need to go Shop to Sell item!");
    }

    public void btnOpenShopUI()
    {
        CloseAllUI();
        m_ShopUI.SetActive(true);
    }

    public void btnCloseShop_Pressed()
    {
        m_ShopUI.SetActive(false);
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

    public void goHomeToLoadPlayer()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }

    public void SetColorButtonInteractMobile(Color color)
    {
        Color transparent = color;
        transparent.a = 0.5f;
        m_btnMobileInteract.color = transparent;
    }

    private void OnApplicationQuit()
    {
        PlayerStats.Instance.saveData();
    }
}
