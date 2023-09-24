using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ListEnemy", menuName = "Enemy/ListEnemy")]
public class ListEnemy : ScriptableObject
{
    public List<EnemyCfg> List_Enemy = new List<EnemyCfg>();
}
