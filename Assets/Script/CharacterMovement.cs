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
    private bool isAttacking = false;
    public bool canDefend = true;
    private bool isDefending = false;

    public bool canMove = true;
    public bool canJump = true;

    public UnityEvent OnAttackEvent;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        //animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(canMove)
        {
            if (!isDefending)
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (isDefending)//reduce speed while defending
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed/2;    
        }

        m_PlayerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            m_PlayerAnimator.SetBool("isJumping", true);
        }
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            Attack();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Defend(true);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            Defend(false);
        }       
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime ,false ,jump);
        jump = false;
    }

    void Defend(bool _bool)
    {
        if (_bool == true)
        {
            canAttack = false;//can't Attack while defending
            isDefending = true;
            m_PlayerAnimator.SetBool("IsDefending", true);
        }           
        if (_bool == false)
        {
            canAttack = true;
            isDefending = false;
             m_PlayerAnimator.SetBool("IsDefending", false);
        }
           
    }
    void Attack()
    {       
        m_PlayerAnimator.SetTrigger("isAttacking");
        SoundsManager.PlaySound(SoundsManager.Sounds.PlayerAttack);

        if (controller.m_GetGrounded)
            m_SwordAnimator.SetBool("isAttacking", true);
        if (!controller.m_GetGrounded)
            m_SwordAnimator.SetBool("isJumpAttacking", true);

        
        
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

    public void OnLanding()
    {
        m_PlayerAnimator.SetBool("isJumping", false);
    }
    public void OnJumping()
    {
        m_PlayerAnimator.SetBool("isJumping", true);
    }

    ///Knockback direction
    ///_dir "up"
    ///_dir "rigth"
    ///_dir "left"
    public void KnockBack()
    {
        int _KnockBackForce = 600;
        Vector2 _dir = new Vector2(-0.8f,1f);
        GetComponent<Rigidbody2D>().AddForce(_dir * _KnockBackForce);

    }
}

