using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int m_speed;
    [SerializeField] private int m_maxHp;
    [SerializeField] private float m_Exp = 1;

    private int m_curHp;
    private int m_curSpeed;

    private void Start()
    {
        m_curHp = m_maxHp;
        m_curSpeed = m_speed;
    }

    public int Hp
    {
        get { return m_curHp; }
        set { m_curHp = value; }
    }

    public int Speed
    {
        get { return m_curSpeed; }
        set { m_curSpeed = value; }
    }

    public float Exp
    {
        get { return m_Exp; }
    }
}
