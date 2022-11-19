using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OnColliderDoEvent : MonoBehaviour
{
    public UnityEvent onColliderEvent;

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.tag == "Player")
        {
            onColliderEvent.Invoke();
        }
    }
}