using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : LivingBeing
{
    public int howMuchGems = 2; 

    public void DropCurrence()//Forusing on Death event
    {

        for (int i = 0; i < howMuchGems; i++)
        {
            Vector2 _dir = new Vector2(Random.Range(-2f,2f),Random.Range(1f,0.5f));
            GameObject _obj = Instantiate(GameAssets.i.gems, transform.position, transform.rotation);
            _obj.GetComponent<Rigidbody2D>().AddForce(_dir * 400);
        }
        Instantiate(GameAssets.i.smoke_Particule,transform.position,transform.rotation);
        Destroy(gameObject,0.1f);
    }
}
