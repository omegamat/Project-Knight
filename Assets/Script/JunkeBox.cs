using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkeBox : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<AudioManager>().Play("Music 1");
    }
}
