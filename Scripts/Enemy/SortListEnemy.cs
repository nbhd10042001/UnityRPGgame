using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SortListEnemy : MonoBehaviour
{
    public ListEnemy m_ListEnemy;
    private List<EnemyCfg> List_Enemy;

    private void Start()
    {
        List_Enemy = m_ListEnemy.List_Enemy;
    }

    private void Update()
    {
        var query = List_Enemy.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();

        if (query.Count != 0)
        {
            for (int i = 0; i < query.Count; i++)
            {
                for (int x = 0; x < query[i].Counter - 1; x++)
                {
                    List_Enemy.Remove(query[i].Element);
                }
            }
        }
    }
}
