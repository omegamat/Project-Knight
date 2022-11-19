using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivingBeing : LivingBeing
{

    CharacterMovement m_characterMovement;
    public override void Start()
    {
        base.Start();
        m_characterMovement = GetComponent<CharacterMovement>();
    }

    public override void TakeDamage(int _damege)
    {
        if (m_characterMovement.getIsDefending)
        {
            m_characterMovement.subtracShieldEnergy();
        }

        if(!m_characterMovement.getIsDefending)
        {
            if (canTakeDamage)
            {
                HP = HP - _damege;
                StartCoroutine(OnDamage());
                SoundsManager.PlaySound(SoundsManager.Sounds.Hit);
                if (flashTime)
                {
                    StartCoroutine(Flash());  
                }
                       
                if(!IsAlive())
                {
                    StartCoroutine(PlayerDeath());
                }
            }
            
            
        }
        
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
    IEnumerator Flash()
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
