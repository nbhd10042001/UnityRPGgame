using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private LayerMask m_PlayerLayerMask;
    private EnemyController enmCtrl;
    private float number;

    private void Awake()
    {
        enmCtrl = GetComponent<EnemyController>();
        number = (1 + enmCtrl.m_scale);
    }

    private void FixedUpdate()
    { 
        Vector2 pointA = new Vector2(transform.position.x + number, transform.position.y);
        Vector2 pointB = new Vector2(transform.position.x - number, transform.position.y);
        bool collider = Physics2D.Linecast(pointA, pointB, m_PlayerLayerMask);
        if (collider == true)
            gameObject.GetComponent<EnemyController>().SetStateAttack();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 pointA = new Vector2(transform.position.x + number, transform.position.y);
        Vector2 pointB = new Vector2(transform.position.x - number, transform.position.y);
        Gizmos.DrawLine(pointA, pointB);
    }
}
