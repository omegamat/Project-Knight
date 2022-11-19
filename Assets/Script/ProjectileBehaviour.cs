using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 16;
        Destroy(this.gameObject, 6f);
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        
        if(col.gameObject.tag == "Player")
        {
           col.gameObject.SendMessage("TakeDamage", 1);
           

           Debug.Log(col.gameObject);
        }
        Destroy(this.gameObject);
    }
}
