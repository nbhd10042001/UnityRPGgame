using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerCollision : MonoBehaviour
{
    private PlayerStats m_PlayerStats;
    private PlayerController m_PlayerCtrl;

    private float m_GetHitTime;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_GetHit;

    private void Awake()
    {
        m_PlayerStats = GetComponent<PlayerStats>();
        m_PlayerCtrl = GetComponent<PlayerController>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (m_GetHit)
            m_GetHitTime -= Time.deltaTime;
        if (m_GetHitTime <= 0)
        {
            m_GetHit = false;
            m_PlayerCtrl.GetHit(m_GetHit);
        }
    }

    private IEnumerator GetHitFX()
    {
        CameraShake.Instance.Shake(0.2f);

        SpriteRenderer spt;
        TryGetComponent(out spt);

        Color transparent = Color.white;
        transparent.a = 0.25f;
        int i = 0;
        while (m_GetHitTime > 0)
        {
            if (i % 2 == 0)
                spt.color = Color.white;
            else
                spt.color = transparent;
            i++;
            yield return null;
        }
        spt.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_GetHit)
            return;

        if (collision.CompareTag("Enemy"))
        {
            m_GetHit = true;
            m_PlayerCtrl.GetHit(m_GetHit);

            m_GetHitTime = 0.5f;
            m_PlayerStats.GetHit(1);

            Vector2 knockbackDirection = transform.position - collision.transform.position;
            knockbackDirection = knockbackDirection.normalized;
            m_Rigidbody2D.AddForce(knockbackDirection * 5, ForceMode2D.Impulse);

            StartCoroutine(GetHitFX());
        }

    }
}
