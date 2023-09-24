using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Hit,
        Fly,
        Attack,
        Deappear,
        Appear
    }

    [SerializeField] private Animator m_Animator;
    [SerializeField] private Transform parentTrans;
    [SerializeField] private float m_scale = 2;
    public EnemyCfg m_EnemyCfg;


    private Rigidbody2D m_rgb2d;
    private State m_curState;
    private int m_DirectionX = 1;
    private Vector2 m_startPos;

    private int ranMv_x;
    private int ranMv_y;
    private Vector2 posMv;
    private float timeWaitToIdle;
    private string nameEnemy;
    private int speed;
    private float delayAtk;

    private GameObject m_player;
    private EnemyHandleCollider m_EnmHandleColli;
    

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_EnmHandleColli = GetComponent<EnemyHandleCollider>();
        m_rgb2d = GetComponent<Rigidbody2D>();
        speed = m_EnemyCfg.speed;
    }

    private void OnEnable()
    {
        PlayerController.Instance.OnActiveEnemy += onActiveEnemy;
    }

    public void SetStart()
    {
        m_startPos = new Vector2(transform.position.x, transform.position.y);
        StartCoroutine(UpdateAI());
    }

    private void OnDisable()
    {
        if (PlayerController.Instance != null)
            PlayerController.Instance.OnActiveEnemy -= onActiveEnemy;
        StopAllCoroutines();
    }

    private void Update()
    {
        if (m_player == null)
            m_player = GameObject.FindGameObjectWithTag("Player");
    }

    public void onActiveEnemy(string name)
    {
        nameEnemy = name;
    }


    public void ReleaseEnemy()
    {
        SpawmManager.Instance.ReleaseEnemy(this, m_EnemyCfg.name);
    }

    private IEnumerator UpdateAI()
    {
        while (true)
        {
            delayAtk += Time.deltaTime;
            if (nameEnemy == m_EnemyCfg.name)
            {
                if (m_curState == State.Idle)
                {
                    yield return new WaitForSeconds(0.5f);
                    m_rgb2d.velocity = new Vector2(0, m_rgb2d.velocity.y);//reset velocity
                    SetStateEnemy(State.Walk);
                }

                else if (m_curState == State.Walk)
                {
                    m_rgb2d.velocity = new Vector2(0, 0);//reset velocity
                    transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, speed * Time.deltaTime);

                    if (transform.position.x <= m_player.transform.position.x)
                        SetDirectionX(-1);
                    else
                        SetDirectionX(1);
                }

                else if (m_curState == State.Hit)
                {
                    Vector2 curInputMvPlayer = PlayerController.Instance.getCurInputMovePlayer();
                    m_rgb2d.velocity = new Vector2(curInputMvPlayer.x * 2, curInputMvPlayer.y * 2);

                    yield return new WaitForSeconds(0.5f);
                    m_rgb2d.velocity = new Vector2(0, 0); //reset velocity

                    SetStateEnemy(State.Idle);
                }

                else if (m_curState == State.Attack)
                {
                    yield return new WaitForSeconds(0.5f);
                    SetStateEnemy(State.Idle);
                }
            }

            else //when enemy not found player stay on Area Enemy
            {
                if (m_curState != State.Walk)
                {
                    yield return new WaitForSeconds(1f);
                    ranMv_x = Random.Range(-2, 2);
                    ranMv_y = Random.Range(-2, 2);
                    posMv = new Vector2(m_startPos.x + ranMv_x, m_startPos.y + ranMv_y);

                    if (posMv.x <= transform.position.x)
                        SetDirectionX(1);
                    else
                        SetDirectionX(-1);

                    SetStateEnemy(State.Walk);
                }

                else if (m_curState == State.Walk)
                {
                    timeWaitToIdle += Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, posMv, speed * Time.deltaTime);

                    if (transform.position.x == posMv.x && transform.position.y == posMv.y)
                    {
                        timeWaitToIdle = 0;
                        SetStateEnemy(State.Idle);
                    }
                    else if (timeWaitToIdle > 3)
                    {
                        timeWaitToIdle = 0;
                        SetStateEnemy(State.Idle);
                    }
                }

            }
            //tac dung la doi 1 frame, tuc la lap lai duy nhat 1 lan moi 1 frame, neu khong co null thi vong lap while nay se lam game se bi crash
            yield return null;
        }
    }

    public void SetStateHit()
    {
        SetStateEnemy(State.Hit);
    }

    public void SetStateAttack()
    {
        if (delayAtk > 2)
        {
            SetStateEnemy(State.Attack);
            delayAtk = 0;
        }
    }

    public bool GetisDetectPlayer()
    {
        return nameEnemy == m_EnemyCfg.name;
    }

    private void SetDirectionX(int directionx)
    {
        m_DirectionX = -directionx;
        transform.localScale = new Vector3(-m_DirectionX * m_scale, m_scale, 1);
    }

    private void SetStateEnemy(State state)
    {
        m_curState = state;

        switch (m_curState)
        {
            case State.Idle:
                PlayIdleAnimation();
                break;
            case State.Walk:
                PlayWalkAnimation();
                break;
            case State.Run:
                PlayRunAnimation();
                break;
            case State.Hit:
                PlayHitAnimation();
                break;
            case State.Fly:
                PlayFlyAnimation();
                break;
            case State.Attack:
                PlayAttackAnimation();
                break;
        }
    }

    //----------Gizmos and Animation-----------------

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 posFrom = new Vector3(parentTrans.position.x + 2, parentTrans.position.y);
    //    Vector3 posTo = new Vector3(parentTrans.position.x - 2, parentTrans.position.y);
    //    Gizmos.DrawLine(posFrom, posTo);

    //    posFrom = new Vector3(parentTrans.position.x, parentTrans.position.y + 2);
    //    posTo = new Vector3(parentTrans.position.x, parentTrans.position.y - 2);
    //    Gizmos.DrawLine(posFrom, posTo);

    //}

    [ContextMenu("Play Idle Animation")]
    private void PlayIdleAnimation()
    {
        m_Animator.SetTrigger("Change");
        m_Animator.SetInteger("State", 1);
    }

    [ContextMenu("Play Walk Animation")]
    private void PlayWalkAnimation()
    {
        m_Animator.SetTrigger("Change");
        m_Animator.SetInteger("State", 2);
    }

    [ContextMenu("Play Run Animation")]
    private void PlayRunAnimation()
    {
        m_Animator.SetTrigger("Change");
        m_Animator.SetInteger("State", 3);
    }

    [ContextMenu("Play Hit Animation")]
    private void PlayHitAnimation()
    {
        m_Animator.SetTrigger("Change");
        m_Animator.SetInteger("State", 4);
    }

    [ContextMenu("Play Fly Animation")]
    private void PlayFlyAnimation()
    {
        m_Animator.SetTrigger("Change");
        m_Animator.SetInteger("State", 5);
    }

    [ContextMenu("Play Attack Animation")]
    private void PlayAttackAnimation()
    {
        m_Animator.SetTrigger("Change");
        m_Animator.SetInteger("State", 6);
    }

    [ContextMenu("Play Deappear Animation")]
    private void PlayDeappearAnimation()
    {
        m_Animator.SetTrigger("Change");
        m_Animator.SetInteger("State", 7);
    }

    [ContextMenu("Play Appear Animation")]
    private void PlayAppearAnimation()
    {
        m_Animator.SetTrigger("Change");
        m_Animator.SetInteger("State", 8);
    }
}
