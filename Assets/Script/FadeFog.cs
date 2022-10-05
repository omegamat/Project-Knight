using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Esse script usa a DOtween para fazer a 'nevoa'(fog) de terrena desaparecer quando colide com o player.
//O fog e apenas um sprite preto que perde a trasparencia quando colide com o player.
public class FadeFog : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Start() 
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.tag == "Player")
        {
            sprite.DOColor(new Color(0,0,0,0),0.5f);
            Debug.Log("Fog BEGONE");
        }
    }
}
