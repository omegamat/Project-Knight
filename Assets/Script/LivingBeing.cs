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

    private bool IsAlive() // se estiver com 0 de HP considera morto
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
    public bool GetIsAlive
    {
        get
        {
            return IsAlive();
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
        if(!IsAlive())
        {
            StartCoroutine(PlayerDeath());
        }
        //m_animator.SetTrigger("Damage");
    }
    IEnumerator PlayerDeath()
    {

        //death animation
        yield return new WaitForSeconds(2.5f);
        //Death UI
        yield return null;

    }
}
