using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private GameObject m_inforSkill;
    [SerializeField] private TextMeshProUGUI m_textInfor;
    [SerializeField] private Image imgSkill;
    [SerializeField] private BattleUI m_battleUI;
    [SerializeField] private Image m_ImgMagicCD;

    private PlayerController playerController;
    public MagicCfg magicCfg;
    private int countPress;
    private float cur_cd;

    private void Awake()
    {
        imgSkill.sprite = magicCfg.sprite;
        playerController = PlayerController.Instance;
        if (playerController != null)
        {
            if (magicCfg.nameSkill == "bolt")
                playerController.OnCDSkill_1 += CDSkill;
            else if (magicCfg.nameSkill == "charged")
                playerController.OnCDSkill_2 += CDSkill;
            else if (magicCfg.nameSkill == "crossed")
                playerController.OnCDSkill_3 += CDSkill;
            else if (magicCfg.nameSkill == "pulse")
                playerController.OnCDSkill_4 += CDSkill;
            else if (magicCfg.nameSkill == "spark")
                playerController.OnCDSkill_5 += CDSkill;
            else if (magicCfg.nameSkill == "waveform")
                playerController.OnCDSkill_6 += CDSkill;
        }
    }

    private void OnEnable()
    {
        PlayerStats.Instance.OnCurLevelChange += CheckLevelPlayer;
        m_battleUI.OnClickInfoSkill += ResetCount;
    }

    private void OnDisable()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.OnCurLevelChange -= CheckLevelPlayer;
        m_battleUI.OnClickInfoSkill -= ResetCount;
    }

    private void Start()
    {
        CheckLevelPlayer(PlayerStats.Instance.Level);
    }

    private void CheckLevelPlayer(int level)
    {
        if (level >= magicCfg.requiredLevel)
            m_ImgMagicCD.fillAmount = 0f;
        else
            m_ImgMagicCD.fillAmount = 1f;
    }

    private void CDSkill(float cur_cd)
    {
        m_ImgMagicCD.fillAmount = cur_cd * 1f / magicCfg.cd;
    }

    

    private void ResetCount()
    {
        countPress = 0;
    }

    private void ShowInfoSkill()
    {
        m_textInfor.text = $" Skill: {magicCfg.name}\n" +
            $" Damage: {magicCfg.damage}\n" +
            $" Mana: {magicCfg.consumable_mana}\n" +
            $" Level: {magicCfg.requiredLevel}\n" +
            $" CD: {magicCfg.cd}s";
    }

    public void btn_SkillPressed()
    {
        if (countPress == 0)
        {
            m_battleUI.OnClickInfoSkill();
            m_inforSkill.SetActive(true);
            ShowInfoSkill();
            countPress++;
        }
        else if (countPress == 1)
        {
            m_inforSkill.SetActive(false);
            ResetCount();
        }
    }
}
