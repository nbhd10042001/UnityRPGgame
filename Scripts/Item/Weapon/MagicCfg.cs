using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic", menuName = "Weapon/Magic")]
[System.Serializable]
public class MagicCfg : ScriptableObject
{
    public string nameSkill;
    public bool piercing;
    public int damage;
    public float cd;
    public int consumable_mana;
    public int requiredLevel;
    public Sprite sprite;
    public int speed;
}
