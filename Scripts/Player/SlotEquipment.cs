using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotEquipment : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Sprite noneUI;

    public void GetItemEquipment (Item item)
    {
        Icon.sprite = item.icon;
        Text.text = $"Damage: {item.damage}\n" +
            $"Defend: {item.defend}\n" +
            $"Level: {item.requiredLevel}\n";
    }

    public void ResetItemEquipment()
    {
        Icon.sprite = noneUI;
        Text.text = $"...";
    }

}
