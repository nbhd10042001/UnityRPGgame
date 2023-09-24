using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int value;
    public string nameCoin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.Instance.Coin += value;
            SpawmManager.Instance.ReleaseCoin(this, nameCoin);
        }
    }
}
