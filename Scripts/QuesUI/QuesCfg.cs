using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Question", menuName = "Question/NewQuestion")]
public class QuesCfg : ScriptableObject
{
    public int numberQuestion;
    public string description;
    public int quanty;
    public int levelQues;
    public int coin;
    public int exp;
    public GameObject prefab;
    public string type;
}
