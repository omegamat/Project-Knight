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
    private bool canTakeDamage = true;
    private bool flashTime;

    public UnityEvent OnDamageEvent;

    private SpriteRenderer m_SpriteRenderer;
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
    
    void Awake()
    {
        if (OnDamageEvent == null)
			OnDamageEvent = new UnityEvent();
    }
    void Start()
    {
        m_SpriteRenderer =  gameObject.GetComponent<SpriteRenderer>();
        m_animator = gameObject.GetComponent<Animator>();
        HP = maxHP;
    }

    // Update is called once per frame


    public virtual void TakeDamage(int _damege)
    {
        if (canTakeDamage)
        {
            HP = HP - _damege;
            StartCoroutine(OnDamage());
            SoundsManager.PlaySound(SoundsManager.Sounds.Hit);
            
        } 
        if (flashTime)
            StartCoroutine(FlashOnDamage());     
        if(!IsAlive())
        {
            StartCoroutine(PlayerDeath());
        }


        //m_animator.SetTrigger("Damage");
    }

    IEnumerator OnDamage()
    {
        canTakeDamage = false;
        flashTime = true;

        OnDamageEvent.Invoke();

        yield return new WaitForSeconds(invicibleTime);

        canTakeDamage = true;
        flashTime = false;

        yield return null;
    }
    IEnumerator FlashOnDamage()
    {
        while(flashTime)
        {
            m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        m_SpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        }
        

    }

    IEnumerator PlayerDeath()
    {

        //death animation
        yield return new WaitForSeconds(2.5f);
        //Death UI
        yield return null;

    }
}
