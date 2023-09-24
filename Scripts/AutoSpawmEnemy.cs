using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawmEnemy : MonoBehaviour
{
    public EnemyCfg m_EnmCfg;
    public int amountSpawm = 3;

    private bool isStartGame = true;
    private bool isSpawm;

    private void Start()
    {
        StartCoroutine(AutoSpawm());
    }

    private IEnumerator AutoSpawm()
    {
        while (true)
        {
            if (isStartGame)
            {
                SpawmEnemy();
                isStartGame = false;
            }

            else
            {
                List<EnemyController> List_Enemy = new List<EnemyController>();

                foreach (EnemyController EnmCtrl in FindObjectsOfType<EnemyController>())
                {
                    if (EnmCtrl.m_EnemyCfg.name == m_EnmCfg.name)
                        List_Enemy.Add(EnmCtrl);
                }

                if (List_Enemy.Count == 0 && !isSpawm)
                {
                    Invoke("SpawmEnemy", 10);
                    isSpawm = true;
                }
            }
            yield return null;
        }
    }

    private void SpawmEnemy()
    {
        for (int i = 0; i < amountSpawm; i++)
        {
            int randomX = Random.Range(-2, 2);
            int randomY = Random.Range(-2, 2);
            Vector2 posSpawm = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
            SpawmManager.Instance.SpawmEnemy(posSpawm, m_EnmCfg.name);
        }
        isSpawm = false;
    }
}
