using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBeheviour : MonoBehaviour
{
    public int gemsValue = 1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if  (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("AddGems", gemsValue);
            Instantiate(GameAssets.i.gemsParticle,transform.position,transform.rotation);
            SoundsManager.PlaySound(SoundsManager.Sounds.Pickup);
            Destroy(gameObject,0.01f);
        }
    }
}
