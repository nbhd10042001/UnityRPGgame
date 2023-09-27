using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "ListQuestion", menuName = "Question/NewListQuestion")]
public class ListQues : ScriptableObject
{
    public List<QuesCfg> List_Ques = new List<QuesCfg>();
}
