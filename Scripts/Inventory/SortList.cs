using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SortList : MonoBehaviour
{
    public ListItem m_ListItem;
    private List<Item> itemList;

    private void Start()
    {
        itemList = m_ListItem.itemList;
    }

    private void Update()
    {
        var query = itemList.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();

        if (query.Count != 0)
        {
            for (int i = 0; i < query.Count; i++)
            {
                for (int x = 0; x < query[i].Counter - 1; x++)
                {
                    itemList.Remove(query[i].Element);
                }
            }
        }
    }
}
