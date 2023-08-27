using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBattle : MonoBehaviour
{
    [SerializeField] private Transform m_parentTran;
    [SerializeField] private Vector2 m_sizeBox;
    [SerializeField] private EnemyHandleCollider m_EnmHandleColli;

    private BoxCollider2D m_boxColli;

    private void Start()
    {
        m_boxColli = GetComponent<BoxCollider2D>();
        m_boxColli.size = new Vector2(m_sizeBox.x, m_sizeBox.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            m_EnmHandleColli.SetDetectPlayer(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            m_EnmHandleColli.SetDetectPlayer(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(m_parentTran.position, m_sizeBox);
    }
}
