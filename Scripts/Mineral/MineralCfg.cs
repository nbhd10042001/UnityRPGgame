using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Mineral", menuName = "Mineral/baseMineral")]
public class MineralCfg : ScriptableObject
{
    new public string name = "Mineral";
    public Item Ore;
    public int hp;
    public int level;
}
