using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCtrl : MonoBehaviour
{
    [SerializeField] private string nameHitAnimation;

    public MagicCfg magicCfg;
    private Rigidbody2D rgb2d;
    private Vector2 inputPlayer;

    private void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        inputPlayer = PlayerController.Instance.getCurInputMovePlayer();
        CheckDirection();
        Invoke("ReleaseMagic", 10);
    }

    private void CheckDirection()
    {
        if (inputPlayer.x == 0 && inputPlayer.y >= 0)
            transform.eulerAngles = new Vector3(0, 0, 90);
        else if (inputPlayer.x == 0 && inputPlayer.y < 0)
            transform.eulerAngles = new Vector3(0, 0, -90);
        else if (inputPlayer.x >= 0 && inputPlayer.y == 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (inputPlayer.x < 0 && inputPlayer.y == 0)
            transform.eulerAngles = new Vector3(0, 0, 180);

        else if (inputPlayer.x > 0 && inputPlayer.y > 0)
            transform.eulerAngles = new Vector3(0, 0, 45);
        else if (inputPlayer.x > 0 && inputPlayer.y < 0)
            transform.eulerAngles = new Vector3(0, 0, -45);
        else if (inputPlayer.x < 0 && inputPlayer.y > 0)
            transform.eulerAngles = new Vector3(0, 0, 135);
        else if (inputPlayer.x < 0 && inputPlayer.y < 0)
            transform.eulerAngles = new Vector3(0, 0, -135);
    }

    private void FixedUpdate()
    {
        UpdateVelocityMagic();
    }

    private void UpdateVelocityMagic()
    {
        if (inputPlayer != null)
            rgb2d.velocity = new Vector3(inputPlayer.x * magicCfg.speed, inputPlayer.y * magicCfg.speed, 0);
        else
            ReleaseMagic();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mineral") || collision.CompareTag("Enemy"))
        {
            if (magicCfg.piercing != true)
                ReleaseMagic();

            SpawmManager.Instance.SpawmHitAnimation(transform.position, nameHitAnimation);
        }
    }

    private void ReleaseMagic()
    {
        SpawmManager.Instance.ReleaseMagic(this, magicCfg.nameSkill);
    }
}
