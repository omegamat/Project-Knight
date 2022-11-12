using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : LivingBeing
{
    public int howMuchGems = 4; 

    public void DropCurrence()//Forusing on Death event
    {

        for (int i = 0; i < howMuchGems; i++)
        {
            Vector2 _dir = new Vector2(Random.Range(-0.5f,0.5f),Random.Range(0.5f,1f));
            GameObject _obj = Instantiate(GameAssets.i.gems, transform.position, transform.rotation);
            _obj.GetComponent<Rigidbody2D>().AddForce(_dir * 200);
        }
        Instantiate(GameAssets.i.Smoke_particule,transform.position,transform.rotation);
        Destroy(gameObject,0.1f);
    }
}
