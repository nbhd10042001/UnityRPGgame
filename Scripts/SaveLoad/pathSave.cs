using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class pathSave : MonoBehaviour
{
    public static pathSave Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public string GetPathSave_curPlayer_PlayerCfg()
    {
        return (Application.dataPath + $"/Resources/save/curPlayer/PlayerCfg.json");
    }

    public string GetPathSave_curPlayer_Inventory()
    {
        return (Application.dataPath + $"/Resources/save/curPlayer/Inventory.json");
    }

    public string GetPathSave_slot_PlayerCfg(string slot)
    {
        return (Application.dataPath + $"/Resources/save/{slot}/PlayerCfg.json");
    }

    public string GetPathSave_slot_Inventory(string slot)
    {
        return (Application.dataPath + $"/Resources/save/{slot}/Inventory.json");
    }

    public string GetPathSave_curPlayer_PlayerCfg_meta()
    {
        return (Application.dataPath + $"/Resources/save/curPlayer/PlayerCfg.json.meta");
    }

    public string GetPathSave_curPlayer_Inventory_meta()
    {
        return (Application.dataPath + $"/Resources/save/curPlayer/Inventory.json.meta");
    }

    public string GetPathSave_slot_PlayerCfg_meta(string slot)
    {
        return (Application.dataPath + $"/Resources/save/{slot}/PlayerCfg.json.meta");
    }

    public string GetPathSave_slot_Inventory_meta(string slot)
    {
        return (Application.dataPath + $"/Resources/save/{slot}/Inventory.json.meta");
    }
}
