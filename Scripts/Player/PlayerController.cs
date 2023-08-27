using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
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

    [SerializeField] private LayerMask m_NPCLayer;
    [SerializeField] private Transform m_Foot;

    private PlayerInput m_PlayerInput;
    private Vector2 m_MovementInput;
    private Animator m_Animator;

    private Rigidbody2D m_rb2d;
    private bool m_IsMoving;
    private bool m_IsAttackPress;
    private bool m_IsInteract;
    private Vector3 interactPos;
    private Vector2 m_curInputMov;
    private bool m_isHit;

    private float m_speed;

    private PlayerStats m_PlayerStats;

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

        m_PlayerInput.Enable();

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
    }



    private void Update()
    {
        m_speed = m_PlayerStats.Speed;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPos, 0.2f);
    }

    public void PlaySfxSlash()
    {
        AudioManager.Instance.PlaySfxSlashSword();
    }
}
