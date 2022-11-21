using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform m_cam;
    public Transform[] m_parallaxBackground = new Transform[3];

    public Transform m_playerCharacter;

    Vector2 startPosition;

    float startZ;
    Vector2 travel => (Vector2)m_cam.transform.position - startPosition;

    public Vector2 parallaxFactor = new Vector2(1,1);
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = m_parallaxBackground[0].position;
        //startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
        m_parallaxBackground[0].position = startPosition + travel * parallaxFactor * 1;
        m_parallaxBackground[1].position = startPosition + travel * parallaxFactor * 0.5f;
        m_parallaxBackground[2].position = startPosition + travel * parallaxFactor * 0.1f;
    }
}
