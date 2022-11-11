using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollider : MonoBehaviour
{
    public string targetTag = "Player";
    
    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == targetTag)
        {
            col.gameObject.SendMessage("TakeDamage", 1);
        }
        
    }
}
