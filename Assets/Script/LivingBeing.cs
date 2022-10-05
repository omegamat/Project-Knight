using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//esse script e aplicado para todos os gameobjects qual podem ser destruidos de caixas a player.
public class LivingBeing: MonoBehaviour
{
    public int HP; //vida atual
    public Animator m_animator;
    public int maxHP = 100; //vida maxima

    public bool isInmortal = false;

    bool isAlive() // se estiver com 0 de HP considera morto
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
        if(!isAlive())
        {
            OnDeath();
        }
        m_animator.SetTrigger("Damage");
    }
    public void OnDeath()
    {
        //corroutine para morte
        //animação de morte
        //...
        //destroy game object

    }
}
