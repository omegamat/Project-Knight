using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerLivingBeing : LivingBeing
{
    //cameras CM
    public CinemachineVirtualCamera m_VirtualCamera;
    private CinemachineBasicMultiChannelPerlin m_VirtualCameraNoise;

    bool canTakeDamageOnShield = true;

    CharacterMovement m_characterMovement;
    public override void Start()
    {
        base.Start();
        m_characterMovement = GetComponent<CharacterMovement>();

        if (m_VirtualCamera != null)
            m_VirtualCameraNoise = m_VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void TakeDamage(int _damege)
    {
        if (m_characterMovement.getIsDefending)
        {
            if(canTakeDamageOnShield)
            {
                StartCoroutine(OnShieldDamage());
            }
            
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
        Instantiate(GameAssets.i.blood_Particule,transform.position,transform.rotation);
        //Camera.main.transform.DOShakePosition(0.5f,5);
        StartCoroutine(ShakeCamera(5f,0.25f));

        OnDamageEvent.Invoke();

        yield return new WaitForSeconds(invicibleTime);

        canTakeDamage = true;
        flashTime = false;

        yield return null;
    }
    IEnumerator OnShieldDamage()
    {   
        canTakeDamageOnShield = false;
        m_characterMovement.subtracShieldEnergy();
        yield return new WaitForSeconds(0.5f);
        canTakeDamageOnShield = true;
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
    private IEnumerator ShakeCamera(float shakeIntensity = 5f, float shakeTiming = 0.5f)
    {
        m_VirtualCameraNoise.m_AmplitudeGain = shakeIntensity;               
        yield return new WaitForSeconds(shakeTiming);
        m_VirtualCameraNoise.m_AmplitudeGain = 0;
    }


    IEnumerator PlayerDeath()
    {

        //death animation
        yield return new WaitForSeconds(2.5f);
        //Death UI
        yield return null;

    }
}
