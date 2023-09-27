using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    [SerializeField] private MineralCfg m_mineralCfg;

    private int m_curHp;
    SpriteRenderer spt;

    private void OnEnable()
    {
        int value = Random.Range(-1, 1);
        if (value == 0)
            value = 1;
        transform.localScale = new Vector3(value, 1, 1);
    }

    private void Start()
    {
        TryGetComponent(out spt);
        SetStart();
    }

    private void SetStart()
    {
        m_curHp = m_mineralCfg.hp;
        spt.color = Color.white;
    }

    private IEnumerator FxHitAttackPlayer()
    {
        Color transparent = Color.white;
        transparent.a = 0.2f;

        int i = 0;
        while (i < 50)
        {
            i++;
            spt.color = transparent;
            yield return null;
        }
        spt.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AtkPlayer"))
        {
            if (PlayerStats.Instance.Level >= m_mineralCfg.level)
            {
                m_curHp -= 1;
                if (m_curHp <= 0)
                {
                    for (int i = 0; i < 2; i++)
                        SpawmManager.Instance.SpawmOre(SpawmPos(), m_mineralCfg.Ore.name, true);
                    Invoke("ActiveTrueForThisObj", 3600);
                    gameObject.SetActive(false);
                    return;
                }
                StartCoroutine(FxHitAttackPlayer());
            }
        }
    }

    private void ActiveTrueForThisObj()
    {
        SetStart();
        gameObject.SetActive(true);
    }

    private Vector2 SpawmPos()
    {
        float rdx = Random.Range(-0.5f, 0.5f);
        float rdy = Random.Range(-0.5f, 0.5f);
        Vector2 newPos = new Vector2(transform.position.x + rdx, transform.position.y + rdy);
        return newPos;
    }
}
