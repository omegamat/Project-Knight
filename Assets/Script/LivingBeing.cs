using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
//esse script e aplicado para todos os gameobjects qual podem ser destruidos de caixas a player.
public class LivingBeing: MonoBehaviour
{
    
    public int HP; //vida atual
    public Animator m_animator;
    public int maxHP = 100; //vida maxima

    public float invicibleTime = 3f;
    public bool isInmortal = false;
    protected bool canTakeDamage = true;
    protected bool flashTime;

    public UnityEvent OnDamageEvent;
    public UnityEvent OnDeathEvent;

    protected SpriteRenderer m_SpriteRenderer;
    protected bool IsAlive() // se estiver com 0 de HP considera morto
    {
        if(HP <= 0 && !isInmortal)
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
    
    public virtual void Start()
    {
        m_SpriteRenderer =  gameObject.GetComponent<SpriteRenderer>();
        m_animator = gameObject.GetComponent<Animator>();
        HP = maxHP;
    }

    public virtual void TakeDamage(int _damage)
    {
        if (canTakeDamage)
        {
            if (IsAlive())
            {
                HP = HP - _damage;

                OnDamageEvent.Invoke();
                SoundsManager.PlaySound(SoundsManager.Sounds.Hit);
            }
            if (!IsAlive())
            {
                OnDeathEvent.Invoke();
            }
           
        }
    }

    IEnumerator DeathCourotine()
    {

        //death animation
        yield return new WaitForSeconds(2.5f);
        //Death UI
        yield return null;

    }

    public void shake()
    {
        gameObject.transform.DOShakeScale(0.25f,0.15f,5);
    }

}
