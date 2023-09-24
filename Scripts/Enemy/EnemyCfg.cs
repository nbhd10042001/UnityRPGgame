using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyCfg")]
public class EnemyCfg : ScriptableObject
{
    new public string name;
    public int speed;
    public int maxHp;
    public Sprite sprite;
    public int Exp;
    public int damage;
    public string coinDrop;
}
