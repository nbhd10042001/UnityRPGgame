using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    private Item item;
    private int amountItem;
    public GameObject m_padding;
    public TextMeshProUGUI txtAmount;

    public Sprite spriteNone;

    private void Start()
    {
        GamePlayManager.Instance.onActiveFalsePaddingSlot += ActiveFalsePaddingItemChoose;
    }

    private void OnDestroy()
    {
        GamePlayManager.Instance.onActiveFalsePaddingSlot -= ActiveFalsePaddingItemChoose;

    }

    public void AddItem (Item newItem, int amount)
    {
        item = newItem;
        amountItem = amount;
        icon.sprite = newItem.icon;
        txtAmount.text = $"x{amount}";
    }


    public void BtnOpenInfoMaterialTab()
    {
        if (item != null)
        {
            GamePlayManager.Instance.SetItemSlot_Pressed(item);
            GamePlayManager.Instance.onActiveFalsePaddingSlot.Invoke();
            GamePlayManager.Instance.ShowInfoMaterialTab(item);

            m_padding.SetActive(true);
        } 
    }


    private void ActiveFalsePaddingItemChoose()
    {
        if (item != null)
        {
            m_padding.SetActive(false);
        }
    }

    public void ClearSlot()
    {
        item = null;
        amountItem = 0;
        icon.sprite = spriteNone;
        txtAmount.text = "";
        m_padding.SetActive(false);

    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }
}
