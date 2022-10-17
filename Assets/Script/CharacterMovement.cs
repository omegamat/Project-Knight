using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator m_PlayerAnimator;
     [SerializeField] private MeleeBehaviour m_melee;
    public Animator m_SwordAnimator;
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;

    public bool canAttack = true;

    public bool canMove = true;

    public UnityEvent OnAttackEvent;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        //animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }

        m_PlayerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            m_PlayerAnimator.SetBool("isJumping", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            //canAttack = false;
            OnAttack();
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime ,false ,jump);
        jump = false;
    }

    void OnAttack()
    {
        
        //OnAttackEvent.Invoke();
        m_SwordAnimator.SetBool("isAttacking", true);
        m_PlayerAnimator.SetTrigger("isAttacking");
        
    }
    public void OnExitAttack()
    {
        canMove = true;
        canAttack = true;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }
    public void OnEnterAttack()
    {
        canMove = false;
        canAttack = false;
    }
    

    void OnDamage()
    {

    }

    public void OnLanding()
    {
        m_PlayerAnimator.SetBool("isJumping", false);
    }
    public void OnJumping()
    {
        m_PlayerAnimator.SetBool("isJumping", true);
    }
}

