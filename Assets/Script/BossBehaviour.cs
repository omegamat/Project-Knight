using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    Animator m_animator;
    LivingBeing m_LivingBeing;

    public GameObject BossUI; 
    public Image bossHP_bar; 
    public bool wakeUp = false;
    public int randomInt = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
        m_LivingBeing = gameObject.GetComponent<LivingBeing>();
        BossUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        m_animator.SetInteger("RandomAttack",randomInt);
        m_animator.SetBool("Awake", wakeUp);
        float hp = m_LivingBeing.HP;
        float maxHP = m_LivingBeing.maxHP;
        bossHP_bar.fillAmount = hp/maxHP;

    }
    public void TriggerBoss()
    {
               
        if (!wakeUp)
        {
            BossUI.SetActive(true);
        }
        wakeUp = true;
        m_LivingBeing.isInmortal = false;
    }
    void RandomValue()
    {
        randomInt = Random.Range(0,2 + 1);
    }

}
