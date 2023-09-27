using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHandleCollider : MonoBehaviour
{
    public Action OnEnemyGetHit;

    private EnemyCfg m_EnemyCfg;
    private EnemyController m_EnmCtrl;
    private int m_curHp;
    private int m_maxHp;
    private int m_curDamage;
    private bool m_GetHit;

    private void Awake()
    {
        m_EnmCtrl = GetComponent<EnemyController>();
        m_EnemyCfg = m_EnmCtrl.m_EnemyCfg;
    }

    private void OnEnable()
    {
        if (PlayerStats.Instance != null)
        {
            Hp = m_EnemyCfg.maxHp * PlayerStats.Instance.Level;
            MaxHp = m_EnemyCfg.maxHp * PlayerStats.Instance.Level;
            Damage = m_EnemyCfg.damage * PlayerStats.Instance.Level;
        }
        else
        {
            Hp = m_EnemyCfg.maxHp;
            MaxHp = m_EnemyCfg.maxHp;
            Damage = m_EnemyCfg.damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetHit)
            return;

        if (collision.gameObject.CompareTag("AtkPlayer") && m_EnmCtrl.GetisDetectPlayer())
        {
            GetHit = true;
            m_EnmCtrl.SetStateHit();
            Invoke("GetHitFalse", 0.5f);
            AudioManager.Instance.PlaySfxEnemyGetHit();
            PlayerCfg playerCfg = PlayerStats.Instance.playerCfg;

            if (collision.gameObject.GetComponent<MagicCtrl>())
                Hp -= collision.gameObject.GetComponent<MagicCtrl>().magicCfg.damage + playerCfg.damage;
            else
                Hp -= playerCfg.damage;


            if (Hp <= 0)
            {
                SpawmManager.Instance.SpawmCoin(transform.position, m_EnemyCfg.coinDrop);
                Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.5f);
                SpawmManager.Instance.SpawmExp(pos, m_EnemyCfg.Exp * PlayerStats.Instance.Level);

                playerCfg.UpdateQuantyQues(gameObject);
                PlayerStats.Instance.onQuantyQuesChange.Invoke();

                GetHit = false;
                Hp = m_EnemyCfg.maxHp;
                m_EnmCtrl.ReleaseEnemy();
            }
        }
    }

    public int Hp
    {
        get { return m_curHp; }
        set 
        {
            m_curHp = value; 
            if (OnEnemyGetHit != null)
                OnEnemyGetHit(); 
        }
    }

    public int MaxHp
    {
        get { return m_maxHp; }
        set { m_maxHp = value; }
    }

    public int Damage
    {
        get { return m_curDamage; }
        set { m_curDamage = value; }
    }

    private void GetHitFalse()
    {
        GetHit = false;
    }

    public bool GetHit
    {
        get { return m_GetHit; }
        set { m_GetHit = value; }
    }

}
