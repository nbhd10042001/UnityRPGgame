using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AreaBattle : MonoBehaviour
{

    public static AreaBattle Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField] private Vector2 m_sizeBox;
    [SerializeField] private EnemyCfg m_EnemyCfg;

    private BoxCollider2D m_boxColli;

    private void Start()
    {
        m_boxColli = GetComponent<BoxCollider2D>();
        m_boxColli.size = new Vector2(m_sizeBox.x, m_sizeBox.y);
    }
    
    public string GetNameArea()
    {
        return m_EnemyCfg.name;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, m_sizeBox);
    }
}
