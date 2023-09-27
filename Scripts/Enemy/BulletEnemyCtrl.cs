using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyCtrl : MonoBehaviour
{
    public EnemyCfg enmCfg;
    private Rigidbody2D rgb2d;
    private float scaleX;

    private void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
    }

    public void SetStart(float scalex)
    {
        scaleX = scalex;
        Invoke("ReleaseBullet", 10);
        transform.localScale = new Vector3(scaleX, System.Math.Abs(scaleX), 0);
    }

    private void FixedUpdate()
    {
        if (scaleX < 0)
            rgb2d.velocity = new Vector3(6, 0, 0);
        else if (scaleX >= 0)
            rgb2d.velocity = new Vector3(-6, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ReleaseBullet();
        }
    }

    private void ReleaseBullet()
    {
        SpawmManager.Instance.ReleaseBulletTrunk(this);
    }
}
