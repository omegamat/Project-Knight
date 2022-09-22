using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing: MonoBehaviour
{
    public int HP;
    public Animator m_animator;
    public int maxHP = 100;

    public bool isInmortal = false;

    bool isAlive()
    {
        if(HP < 0 && !isInmortal)
        {
            return true;
        }
        else
        {
           return false;
        }
            
    }
    void Start()
    {
        m_animator = GetComponent<Animator>();
        HP = maxHP;
    }

    // Update is called once per frame


    public void TakeDamage(int _damege)
    {
        HP = HP - _damege;
        isAlive();
        m_animator.SetTrigger("Damage");
    }
}
