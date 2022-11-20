using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//this is the main scrips for the player
public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator m_PlayerAnimator;
    private MeleeBehaviour m_melee;
    public Animator m_SwordAnimator;
    public GameObject shield;
    public int shieldHP = 3;
    public int maxShieldHP = 3;
    private float shieldCoolDown = 3f;
    public float runSpeed = 40f;
    private float runSpeedActual = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;

    public bool canAttack = true;
    private bool isAttacking = false;
    public bool canDefend = true;
    private bool isDefending = false;
    public bool getIsDefending{get{return isDefending;}}

    public bool canMove = true;
    public bool canJump = true;

    public int gems = 0; //this game currency
    
    public UnityEvent OnAttackEvent;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        shieldHP = maxShieldHP;
        //animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(canMove)
        {  
            if (isDefending)//reduce speed while defending
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed/2;
            if (!isDefending)
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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

        //defend
        if (controller.m_GetGrounded)
        {
            if (Input.GetButtonDown("Fire2") && shieldHP > 0)
            {
                if(shieldHP > 0)
                {
                    Defend(true);
                }               
                if (shieldHP <= 0)
                {
                    Defend(false);
                }
            }
            if (Input.GetButtonUp("Fire2"))
            {
                Defend(false);
                StartCoroutine(ShieldCountDown());
            }
            if (shieldHP <= 0)
            {
                Defend(false);
            }
            

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
            shield.SetActive(true);
            m_PlayerAnimator.SetBool("IsDefending", true);
        }           
        if (_bool == false)
        {
            canAttack = true;
            isDefending = false;
            shield.SetActive(false);
             m_PlayerAnimator.SetBool("IsDefending", false);
        }          
    }
    public void subtracShieldEnergy()
    {
        shieldHP = shieldHP - 1;
        Instantiate(GameAssets.i.ShieldHit_particule, this.transform.position,this.transform.rotation);
        Mathf.Clamp(shieldHP,0,maxShieldHP);
    }

    IEnumerator ShieldCountDown()
    { 
        while(shieldHP < maxShieldHP +1)
        {
            Mathf.Clamp(shieldHP,0,maxShieldHP);
            yield return new WaitForSeconds(shieldCoolDown);
            shieldHP += 1;
            
        }
        
        
    }
    void Attack()
    {       
        m_PlayerAnimator.SetTrigger("isAttacking");
        SoundsManager.PlaySound(SoundsManager.Sounds.PlayerAttack);
        Vector2 _force = new Vector2 (controller.m_Rigidbody2D.velocity.x *-1.2f,0);

        if (controller.m_GetGrounded)
            m_SwordAnimator.SetBool("isAttacking", true);

        if (!controller.m_GetGrounded)
            m_SwordAnimator.SetBool("isJumpAttacking", true);

        
        
    }
    public void OnExitAttack()
    {
        canMove = true;
        canAttack = true;
        isAttacking = false;
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }
    public void OnEnterAttack()
    {
        canMove = false;
        canAttack = false;
        isAttacking = true;
    }  

    public void OnLanding()
    {
        m_PlayerAnimator.SetBool("isJumping", false);
    }
    public void OnJumping()
    {
        m_PlayerAnimator.SetBool("isJumping", true);
    }

    //move player we hit
    public void KnockBack()
    {
        int _KnockBackForce = 600;
        Vector2 _dir = new Vector2(0f,1f);
        GetComponent<Rigidbody2D>().AddForce(_dir * _KnockBackForce);
    }
    
    public void AddGems(int _v)
    {
        gems = gems + _v;
    }
}

