using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinMoveLoop : MonoBehaviour
{
    public float m_speed = 10f;
    public const float SCREEN_LENGHT_MIN = -19f;
    public const float SCREEN_LENGHT_MAX = 19f;
    public GameObject m_StartPosition;
    public GameObject m_RestartPosition;
    public SpriteRenderer mySpriteRenderer;
    
    public void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
   
    void Update()
    {
        
        Reappear();
    }

    public void Reappear()
    {
        if (transform.localPosition.x > SCREEN_LENGHT_MAX)
        {
            gameObject.transform.position = m_StartPosition.transform.position;
            transform.Translate(Vector3.right * -m_speed);
            mySpriteRenderer.flipX = false;
        }
        else if (transform.localPosition.x < SCREEN_LENGHT_MIN)
        {
            gameObject.transform.position = m_RestartPosition.transform.position;
            transform.Translate(Vector3.right * m_speed);
            mySpriteRenderer.flipX = true;
        }
        else
        {
            if (mySpriteRenderer.flipX == false)
            {
                transform.Translate(Vector3.right * -m_speed);
            }
            else
            {
                transform.Translate(Vector3.right * m_speed);
            }
        }
    }
}
