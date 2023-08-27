using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleCollider : MonoBehaviour
{
    private EnemyController m_EnmCtrl;
    private EnemyStats m_EnmStats;
    private bool m_GetHit;
    private bool m_isDetectPlayer;


    private void Awake()
    {
        m_EnmCtrl = GetComponent<EnemyController>();
        m_EnmStats = GetComponent<EnemyStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_GetHit)
            return;

        if (collision.gameObject.CompareTag("AtkPlayer") && m_isDetectPlayer)
        {
            m_GetHit = true;
            m_EnmCtrl.SetStateHit();
            AudioManager.Instance.PlaySfxEnemyGetHit();

            m_EnmStats.Hp -= 1;
            if (m_EnmStats.Hp <= 0)
            {
                PlayerStats.Instance.Exp += m_EnmStats.Exp;
                Destroy(GameObject.Find("AngryPig"));
            }
        }
    }

    public void SetGetHit(bool isHit)
    {
        m_GetHit = isHit;
    }

    public void SetDetectPlayer(bool isDetect)
    {
        m_isDetectPlayer = isDetect;
        if (!isDetect)
            m_EnmCtrl.SetStateIdle();
    }

    public bool GetIsDetectPlayer()
    {
        return m_isDetectPlayer;
    }
}
