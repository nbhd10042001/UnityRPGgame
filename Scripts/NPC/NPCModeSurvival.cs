using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCModeSurvival : MonoBehaviour, Interactable
{
    [SerializeField] private Dialog m_dialog;

    public void Interact()
    {
        DialogManager.Instance.ShowDialog(m_dialog, "ModeSurvival");

    }
}
