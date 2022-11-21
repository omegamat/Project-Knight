using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingGolemBehaviour : MonoBehaviour
{
    public float m_speed = 2.5f;
    public bool isActivate = true;

    public bool canFlip;
    [SerializeField] Rigidbody2D m_rigidBody2D;
    [SerializeField] Transform m_position;


    [SerializeField] Transform positionA;
    [SerializeField] Transform positionB;

    private enum MovingTo 
    {
        PointA,
        PointB
    }
    private MovingTo movingTo;
    private Vector3 m_direction;
    private Vector3[] positionsArray;

    // Start is called before the first frame update
    void Awake()
    {
        //find
        m_rigidBody2D = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();

        m_position = gameObject.transform.GetChild(0);
        positionA = gameObject.transform.GetChild(1);
        positionB = gameObject.transform.GetChild(2);



        //plataform.position = positionA.position;

        movingTo = MovingTo.PointB;      
        
    }
    void Start()
    {
        m_position.localPosition = positionA.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_position.localPosition.x > positionB.localPosition.x + 1)
                movingTo = MovingTo.PointA;
        if(m_position.localPosition.x < positionA.localPosition.x - 1)
                movingTo = MovingTo.PointB;
        if(movingTo == MovingTo.PointB)
        {
            m_direction = positionB.localPosition.normalized;
            //plataform.DOLocalMoveX(positionB.localPosition.x + 0.1f,3).SetEase(Ease.Linear);
            m_rigidBody2D.velocity = m_direction * m_speed;
            m_position.localScale = new Vector3(Mathf.Abs(m_position.localScale.x),m_position.localScale.y,m_position.localScale.z);
            

        }
        if(movingTo == MovingTo.PointA)
        {
            m_direction = positionA.localPosition.normalized;
            //plataform.DOLocalMoveX(positionA.localPosition.x + 0.1f,5).SetEase(Ease.Linear);
            m_rigidBody2D.velocity = m_direction*m_speed;
            m_position.localScale = new Vector3(-1,m_position.localScale.y,m_position.localScale.z);
            

        }
    }
    void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(m_position.position,positionA.position);
        Gizmos.DrawLine(m_position.position,positionB.position);
        Gizmos.DrawWireSphere(positionA.position,0.5f);
        Gizmos.DrawWireSphere(positionB.position,0.5f);
    }
}
