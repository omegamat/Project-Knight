using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
     [SerializeField] private MeleeBehaviour m_melee;
    public Animator m_Sword;
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    bool jump = false;

    public UnityEvent OnAttackEvent;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", jump);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime ,false ,jump);
        jump = false;
    }

    void Attack()
    {
        OnAttackEvent.Invoke();
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
}

