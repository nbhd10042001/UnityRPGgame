using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPCtrl : MonoBehaviour
{
    public int xp_value;
    private float scaleX;
    private bool loop;

    private void OnEnable()
    {
        scaleX = 1;
        transform.localScale = new Vector3(1, 1, 1);

    }

    private void Update()
    {
        if (scaleX >= 1)
            loop = false;
        else if (scaleX <= -1)
            loop = true;

        if (loop)
            scaleX += Time.deltaTime;
        else
            scaleX -= Time.deltaTime;

        transform.localScale = new Vector3(scaleX, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.Instance.GetExp(xp_value);
            SpawmManager.Instance.ReleaseExp(this);
        }
    }
}
