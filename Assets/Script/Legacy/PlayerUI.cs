using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//legacy script
public class PlayerUI : MonoBehaviour
{
    public Image HP_bar;
    public LivingBeing player;

    float maxHP = 0;

    float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = player.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = player.HP;
        HP_bar.fillAmount = currentHP/maxHP;
    }
}
