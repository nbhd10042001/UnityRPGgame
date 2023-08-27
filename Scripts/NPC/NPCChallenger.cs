using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChallenger : MonoBehaviour, Interactable
{
    [SerializeField] private Dialog m_dialog;

    public void Interact()
    {
        DialogManager.Instance.ShowDialog(m_dialog, "Challenger");

    }
}
