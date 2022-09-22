using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlataform : MonoBehaviour
{
    public float duration = 5f;
    public bool isActivate = true;
    [SerializeField] Transform plataform;


    [SerializeField] Transform positionA;
    [SerializeField] Transform positionB;
    private Vector3[] positionsArray;

    // Start is called before the first frame update
    void Start()
    {
        //find
        plataform = gameObject.transform.GetChild(0);
        positionA = gameObject.transform.GetChild(1);
        positionB = gameObject.transform.GetChild(2);

        Vector3 pointA = positionA.position;
        Vector3 pointB = positionB.position;
        positionsArray = new Vector3[2] {pointA,pointB};

        plataform.position = positionA.position;
        if(isActivate == true)
        {
            Sequence moveSequence = DOTween.Sequence();
            moveSequence.Append(plataform.DOMove(positionB.position,duration/2f))
                        .Append(plataform.DOMove(positionA.position,duration/2f))
                        .SetLoops(-1,LoopType.Restart);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(plataform.position,positionA.position);
        Gizmos.DrawLine(plataform.position,positionB.position);
        Gizmos.DrawWireSphere(positionA.position,0.5f);
        Gizmos.DrawWireSphere(positionB.position,0.5f);
    }
}
