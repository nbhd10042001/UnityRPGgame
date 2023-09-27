using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public Action<string> OnActiveEnemy;

    private static PlayerController m_Instance;
    public static PlayerController Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<PlayerController>();
            return m_Instance;
        }
    }

    public Action<float> OnCDSkill_1;
    public Action<float> OnCDSkill_2;
    public Action<float> OnCDSkill_3;
    public Action<float> OnCDSkill_4;
    public Action<float> OnCDSkill_5;
    public Action<float> OnCDSkill_6;

    [SerializeField] private LayerMask m_NPCLayer;
    [SerializeField] private LayerMask m_RegionEnemyLayer;
    [SerializeField] private Transform m_Foot;
    [SerializeField] private Transform m_PointHome;

    private PlayerInput m_PlayerInput;
    private Vector2 m_MovementInput;
    private Animator m_Animator;
    private PlayerStats m_PlayerStats;


    private Rigidbody2D m_rb2d;
    private bool m_IsMoving;
    private bool m_IsAttackPress;
    private bool m_IsInteract;
    private Vector3 interactPos;
    private Vector2 m_curInputMov;
    private bool m_isHit;

    private float m_speed;

    public MagicCfg MagicBolt;
    public MagicCfg MagicCharged;
    public MagicCfg MagicCrossed;
    public MagicCfg MagicPulse;
    public MagicCfg MagicSpark;
    public MagicCfg MagicWaveForm;

    private bool skill_1 = true;
    private bool skill_2 = true;
    private bool skill_3 = true;
    private bool skill_4 = true;
    private bool skill_5 = true;
    private bool skill_6 = true;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

        m_rb2d = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_PlayerStats = GetComponent<PlayerStats>();

        m_PlayerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        m_PlayerInput.Player.Movement.started += OnMovement;
        m_PlayerInput.Player.Movement.performed += OnMovement;
        m_PlayerInput.Player.Movement.canceled += OnMovement;

        m_PlayerInput.Player.Interact.started += OnInteract;
        m_PlayerInput.Player.Interact.performed += OnInteract;
        m_PlayerInput.Player.Interact.canceled += OnInteract;

        m_PlayerInput.Player.Attack.started += OnAttack;
        m_PlayerInput.Player.Attack.performed += OnAttack;
        m_PlayerInput.Player.Attack.canceled += OnAttack;

        m_PlayerInput.Player.Magic.started += OnMagic;
        m_PlayerInput.Player.Magic.performed += OnMagic;
        m_PlayerInput.Player.Magic.canceled += OnMagic;

        m_PlayerInput.Enable();

        MovePlayerToOtherPoint(m_PointHome);
    }

    private void OnMovement (InputAction.CallbackContext context)
    {
        
        if (context.started || context.performed)
            m_MovementInput = context.ReadValue<Vector2>();
        else
            m_MovementInput = Vector2.zero;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
            m_IsInteract = true;
        else
            m_IsInteract = false;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_IsAttackPress = true;
        }
        else if (context.canceled)
        {
            m_IsAttackPress = false;
        }
    }


    private void OnDisable()
    {
        m_PlayerInput.Disable();
        skill_1 = true;
        skill_2 = true;
        skill_3 = true;
        skill_4 = true;
        skill_5 = true;
        skill_6 = true;
    }

    private void Start()
    {
        //OnCDMagicBolt(cd_MagicBolt, MagicBolt.cd);
    }

    private void Update()
    {
        m_speed = m_PlayerStats.Speed;

        if (Input.GetKeyDown(KeyCode.X))
        {
            //
        }
    }

    private void FixedUpdate()
    {
        if (m_isHit)
            return;

        CheckMovement();
        CheckAnimation();

        m_Animator.SetFloat("speedAtk", m_PlayerStats.SpeedAtk);

        Vector3 facingDir = new Vector3(m_Animator.GetFloat("moveX"), m_Animator.GetFloat("moveY"));
        interactPos = transform.position + facingDir;
        //interactPos = transform.position;
        //CheckTourchNPC(interactPos);
        CheckTourchNPC(m_Foot.position);
        CheckOnRegionEnemy();
    }

    private void CheckOnRegionEnemy()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 0.00001f, m_RegionEnemyLayer);

        if (collider != null)
        {
            string name = collider.GetComponent<AreaBattle>()?.GetNameArea();
            OnActiveEnemy(name);
        }
        else
        {
            if (OnActiveEnemy != null)
                OnActiveEnemy("");
        }
    }

    private void CheckTourchNPC(Vector3 interactPos)
    {
        var collider = Physics2D.OverlapCircle(interactPos, 0.00001f, m_NPCLayer);
        if (collider != null && m_IsInteract)
            collider.GetComponent<Interactable>()?.Interact();
    }

    private void CheckMovement()
    {
        if (m_IsAttackPress)
        {
            m_rb2d.velocity = new Vector2(m_MovementInput.x * 0, m_MovementInput.y * 0);
            return;
        }

        if (m_MovementInput.x != 0 || m_MovementInput.y != 0)
        {
            if (m_MovementInput.x > 0 && (m_MovementInput.y < 0.3f && m_MovementInput.y > -0.3f))
            {
                m_MovementInput.x = 1;
                m_MovementInput.y = 0;
            }
            else if (m_MovementInput.x < 0 && (m_MovementInput.y < 0.3f && m_MovementInput.y > -0.3f))
            {
                m_MovementInput.x = -1;
                m_MovementInput.y = 0;
            }
            else if (m_MovementInput.y > 0 && (m_MovementInput.x < 0.3f && m_MovementInput.x > -0.3f))
            {
                m_MovementInput.x = 0;
                m_MovementInput.y = 1;
            }
            else if (m_MovementInput.y < 0 && (m_MovementInput.x < 0.3f && m_MovementInput.x > -0.3f))
            {
                m_MovementInput.x = 0;
                m_MovementInput.y = -1;
            }
            else if (m_MovementInput.x > 0.3f && m_MovementInput.y > 0.3f)
            {
                m_MovementInput.x = 0.7f;
                m_MovementInput.y = 0.7f;
            }
            else if (m_MovementInput.x > 0.3f && m_MovementInput.y < -0.3f)
            {
                m_MovementInput.x = 0.7f;
                m_MovementInput.y = -0.7f;
            }
            else if (m_MovementInput.x < -0.3f && m_MovementInput.y > 0.3f)
            {
                m_MovementInput.x = -0.7f;
                m_MovementInput.y = 0.7f;
            }
            else if (m_MovementInput.x < -0.3f && m_MovementInput.y < -0.3f)
            {
                m_MovementInput.x = -0.7f;
                m_MovementInput.y = -0.7f;
            }

            m_IsMoving = true;
            m_Animator.SetFloat("moveX", m_MovementInput.x);
            m_Animator.SetFloat("moveY", m_MovementInput.y);
            m_curInputMov = new Vector2(m_MovementInput.x, m_MovementInput.y);
        }
        else
            m_IsMoving = false;


        m_rb2d.velocity = new Vector2(m_MovementInput.x * m_speed, m_MovementInput.y * m_speed);
    }

    private void CheckAnimation()
    {
        m_Animator.SetBool("IsMoving", !m_IsAttackPress && m_IsMoving);
        m_Animator.SetBool("IsAttack", m_IsAttackPress);

    }

    public Vector2 getCurInputMovePlayer()
    {
        return m_curInputMov;
    }

    public void GetHit(bool isHit)
    {
        m_isHit = isHit;
    }

    public void MovePlayerToOtherPoint(Transform trans)
    {
        gameObject.transform.position = trans.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPos, 0.2f);
    }

    public void PlaySfxSlash()
    {
        AudioManager.Instance.PlaySfxSlashSword();
    }

    #region --------------------- Event click skill ------------------------
    private void OnMagic(InputAction.CallbackContext context)
    {
        if (context.performed && context.control.name.ToString() == "1")
        {
            if (skill_1 && PlayerStats.Instance.Mana >= MagicBolt.consumable_mana
                && PlayerStats.Instance.Level >= MagicBolt.requiredLevel)
            {
                skill_1 = false;
                PlayerStats.Instance.Mana -= MagicBolt.consumable_mana;
                SpawmManager.Instance.SpawmMagic(transform.position, MagicBolt.nameSkill);
                StartCoroutine(ResetSkill_1());

            }
        }
        else if (context.performed && context.control.name.ToString() == "2")
        {
            if (skill_2 && PlayerStats.Instance.Mana >= MagicCharged.consumable_mana
                && PlayerStats.Instance.Level >= MagicCharged.requiredLevel)
            {
                skill_2 = false;
                PlayerStats.Instance.Mana -= MagicCharged.consumable_mana;
                SpawmManager.Instance.SpawmMagic(transform.position, MagicCharged.nameSkill);
                StartCoroutine(ResetSkill_2());

            }
        }

        else if (context.performed && context.control.name.ToString() == "3")
        {
            if (skill_3 && PlayerStats.Instance.Mana >= MagicCrossed.consumable_mana
                && PlayerStats.Instance.Level >= MagicCrossed.requiredLevel)
            {
                skill_3 = false;
                PlayerStats.Instance.Mana -= MagicCrossed.consumable_mana;
                SpawmManager.Instance.SpawmMagic(transform.position, MagicCrossed.nameSkill);
                StartCoroutine(ResetSkill_3());

            }
        }

        else if (context.performed && context.control.name.ToString() == "4")
        {
            if (skill_4 && PlayerStats.Instance.Mana >= MagicPulse.consumable_mana
                && PlayerStats.Instance.Level >= MagicPulse.requiredLevel)
            {
                skill_4 = false;
                PlayerStats.Instance.Mana -= MagicPulse.consumable_mana;
                SpawmManager.Instance.SpawmMagic(transform.position, MagicPulse.nameSkill);
                StartCoroutine(ResetSkill_4());

            }
        }

        else if (context.performed && context.control.name.ToString() == "5")
        {
            if (skill_5 && PlayerStats.Instance.Mana >= MagicSpark.consumable_mana
                && PlayerStats.Instance.Level >= MagicSpark.requiredLevel)
            {
                skill_5 = false;
                PlayerStats.Instance.Mana -= MagicSpark.consumable_mana;
                SpawmManager.Instance.SpawmMagic(transform.position, MagicSpark.nameSkill);
                StartCoroutine(ResetSkill_5());

            }
        }

        else if (context.performed && context.control.name.ToString() == "6")
        {
            if (skill_6 && PlayerStats.Instance.Mana >= MagicWaveForm.consumable_mana
                && PlayerStats.Instance.Level >= MagicWaveForm.requiredLevel)
            {
                skill_6 = false;
                PlayerStats.Instance.Mana -= MagicWaveForm.consumable_mana;
                SpawmManager.Instance.SpawmMagic(transform.position, MagicWaveForm.nameSkill);
                StartCoroutine(ResetSkill_6());
            }
        }
    }

    private IEnumerator ResetSkill_1()
    {
        float cur_cd = MagicBolt.cd;
        while (true)
        {
            cur_cd -= Time.deltaTime;
            if (cur_cd <= 0)
                break;
            OnCDSkill_1(cur_cd);
            yield return null;
        }
        skill_1 = true;
    }

    private IEnumerator ResetSkill_2()
    {
        float cur_cd = MagicCharged.cd;
        while (true)
        {
            cur_cd -= Time.deltaTime;
            if (cur_cd <= 0)
                break;
            OnCDSkill_2(cur_cd);
            yield return null;
        }
        skill_2 = true;
    }

    private IEnumerator ResetSkill_3()
    {
        float cur_cd = MagicCrossed.cd;
        while (true)
        {
            cur_cd -= Time.deltaTime;
            if (cur_cd <= 0)
                break;
            OnCDSkill_3(cur_cd);
            yield return null;
        }
        skill_3 = true;
    }

    private IEnumerator ResetSkill_4()
    {
        float cur_cd = MagicPulse.cd;
        while (true)
        {
            cur_cd -= Time.deltaTime;
            if (cur_cd <= 0)
                break;
            OnCDSkill_4(cur_cd);
            yield return null;
        }
        skill_4 = true;
    }

    private IEnumerator ResetSkill_5()
    {
        float cur_cd = MagicSpark.cd;
        while (true)
        {
            cur_cd -= Time.deltaTime;
            if (cur_cd <= 0)
                break;
            OnCDSkill_5(cur_cd);
            yield return null;
        }
        skill_5 = true;
    }

    private IEnumerator ResetSkill_6()
    {
        float cur_cd = MagicWaveForm.cd;
        while (true)
        {
            cur_cd -= Time.deltaTime;
            if (cur_cd <= 0)
                break;
            OnCDSkill_6(cur_cd);
            yield return null;
        }
        skill_6 = true;
    }
    #endregion
}
