using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;

    //defaul stats
    public int m_maxHp = 3;
    public int m_maxMana = 8;
    public int m_maxSpeed = 4;
    public float m_speedAttack = 1;

    public int m_curHp;
    public int m_curMana;
    public int m_curSpeed;
    public float m_curSpeedAtk;
    public string m_dayCreate;

    public int m_curLevel = 1;
    public float m_curExp = 1;
    public float m_maxExp = 10;

    public void InitStats()
    {
        m_curHp = m_maxHp;
        m_curMana = m_maxMana;
        m_curSpeed = m_maxSpeed;
        m_curSpeedAtk = m_speedAttack;
    }
}
