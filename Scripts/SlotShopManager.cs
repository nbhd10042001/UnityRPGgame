using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotShopManager : MonoBehaviour
{
    [SerializeField] private Image m_sprite;
    [SerializeField] private TextMeshProUGUI m_textDescript;
    public ItemUse itemUse;
    private bool useManaPo;
    private bool useDamPo;
    private bool useDefPo;

    private void Start()
    {
        m_sprite.sprite = itemUse.sprite;
        m_textDescript.text = $"Potion {itemUse.name}\n" +
            $"Price: {itemUse.price}\n" +
            $"Descript: {itemUse.description}";
    }


    public void btnPricePressed()
    {
        if (PlayerStats.Instance.Coin >= itemUse.price)
        {
            if (itemUse.name == "Hp")
            {
                if (PlayerStats.Instance.Hp >= PlayerStats.Instance.playerCfg.maxHp)
                    return;
                PlayerStats.Instance.Hp += itemUse.regenHP;
                PlayerStats.Instance.Coin -= itemUse.price;
            }

            else if (itemUse.name == "Mana")
            {
                if (useManaPo)
                    return;
                useManaPo = true;
                PlayerStats.Instance.IncreaseSpeedRegenMana(itemUse.speedRegenMana);
                PlayerStats.Instance.Coin -= itemUse.price;
                Invoke("ResetSpeedRegenMana", 60f);
            }
                
            else if (itemUse.name == "Damage")
            {
                if (useDamPo)
                    return;
                useDamPo = true;
                PlayerStats.Instance.IncreaseDamage(itemUse.increDamage);
                PlayerStats.Instance.Coin -= itemUse.price;
                Invoke("ResetDamage", 60f);
            }

            else if (itemUse.name == "Defend")
            {
                if (useDefPo)
                    return;
                useDefPo = true;
                PlayerStats.Instance.IncreaseDefend(itemUse.increDefend);
                PlayerStats.Instance.Coin -= itemUse.price;
                Invoke("ResetDefend", 60f);
            }
        }
        else
            GamePlayManager.Instance.SetTextWarning("Your Coin not enough!");
    }

    private void ResetSpeedRegenMana()
    {
        PlayerStats.Instance.IncreaseSpeedRegenMana(-itemUse.speedRegenMana);
        GamePlayManager.Instance.OnItemEquipmentChange();
        useManaPo = false;
    }

    private void ResetDamage()
    {
        PlayerStats.Instance.IncreaseDamage(-itemUse.increDamage);
        GamePlayManager.Instance.OnItemEquipmentChange();
        useDamPo = false;
    }

    private void ResetDefend()
    {
        PlayerStats.Instance.IncreaseDefend(-itemUse.increDefend);
        GamePlayManager.Instance.OnItemEquipmentChange();
        useDefPo = false;
    }
}
