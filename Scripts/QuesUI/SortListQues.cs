using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SortListQues : MonoBehaviour
{
    public ListQues m_ListQues;
    private List<QuesCfg> List_Ques;

    private void Start()
    {
        List_Ques = m_ListQues.List_Ques;
    }

    private void Update()
    {
        var query = List_Ques.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();

        if (query.Count != 0)
        {
            for (int i = 0; i < query.Count; i++)
            {
                for (int x = 0; x < query[i].Counter - 1; x++)
                {
                    List_Ques.Remove(query[i].Element);
                }
            }
        }
    }
}
