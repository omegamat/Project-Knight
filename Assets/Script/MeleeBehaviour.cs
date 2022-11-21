using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MeleeBehaviour: MonoBehaviour
{
    public CinemachineVirtualCamera m_VirtualCamera;
    private CinemachineBasicMultiChannelPerlin m_VirtualCameraNoise;

    Animator m_animetor;

    public CharacterMovement m_character;

    public string targertTag = "NPCEnemy";
    public int m_Damage = 1;

    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        m_animetor = GetComponent<Animator>();

        if (m_VirtualCamera != null)
            m_VirtualCameraNoise = m_VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    //Do damager to tag collider
    private void OnTriggerEnter2D(Collider2D col) 
    {
        Vector3 _p = new Vector3(gameObject.transform.position.x + 0.85f, gameObject.transform.position.y,0);
        if(col.gameObject.tag == targertTag)
        {
           col.gameObject.SendMessage("TakeDamage", m_Damage);
           
           Instantiate(hitEffect,_p, gameObject.transform.rotation);
           SoundsManager.PlaySound(SoundsManager.Sounds.Hit);

           StartCoroutine(ShakeCamera(3,0.2f));

           Debug.Log(col.gameObject);
        }
        else
        {
            return;
        }
    }

    //used to star animation using unity events
    public void StartAttack()
    {
        m_character.OnEnterAttack();
    }
    public void EndAttack()
    {
        m_character.OnExitAttack();
        m_animetor.SetBool("isAttacking", false);
    }

    private IEnumerator ShakeCamera(float shakeIntensity = 5f, float shakeTiming = 0.5f)
    {
        m_VirtualCameraNoise.m_AmplitudeGain = shakeIntensity;               
        yield return new WaitForSeconds(shakeTiming);
        m_VirtualCameraNoise.m_AmplitudeGain = 0;
    }
}
