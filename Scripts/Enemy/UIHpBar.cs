using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHpBar : MonoBehaviour
{
    [SerializeField] private Image m_ImgHpBar;
    [SerializeField] private TextMeshProUGUI m_TxtHP;
    [SerializeField] private GameObject UIHp;

    private EnemyHandleCollider EnmHandl;

    private void OnEnable()
    {
        EnmHandl.OnEnemyGetHit += onEnenmyGetHit;
        onEnenmyGetHit();
    }

    private void Awake()
    {
        EnmHandl = gameObject.GetComponent<EnemyHandleCollider>();
    }

    private void Update()
    {
        if (gameObject.transform.localScale.x > 0)
            UIHp.transform.localScale = new Vector3(1, 1, 1);
        else
            UIHp.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void onEnenmyGetHit()
    {
        int curHp = EnmHandl.Hp;
        int maxHp = EnmHandl.MaxHp;

        m_TxtHP.text = $"{curHp}/{maxHp}";
        m_ImgHpBar.fillAmount = curHp * 1f / maxHp;
    }
}
