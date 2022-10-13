using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour: MonoBehaviour
{
    Animator m_animetor;

    public CharacterMovement m_character;

    public string targertTag = "NPCEnemy";
    public int m_Damage = 1;

    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        m_animetor = GetComponent<Animator>();
    }

    //Do damager to tag collider
    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.tag == targertTag)
        {
           col.gameObject.SendMessage("TakeDamage",m_Damage);
           Instantiate(hitEffect,col.transform.position,default);
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
}
