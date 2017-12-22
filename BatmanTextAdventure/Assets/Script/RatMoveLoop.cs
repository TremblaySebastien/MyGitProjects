using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMoveLoop : MonoBehaviour
{
    public float m_speed = 10f;
    public const float SCREEN_LENGHT_MIN = -19f;
    public const float SCREEN_LENGHT_MAX = 19f;
    public GameObject m_RestartPosition;

    void Update()
    {
        transform.Translate(Vector3.right * m_speed);
        Reappear();
    }

    public void Reappear()
    {
        if (transform.localPosition.x < SCREEN_LENGHT_MIN || transform.localPosition.x > SCREEN_LENGHT_MAX)
        {
            gameObject.transform.position = m_RestartPosition.transform.position;
        }
    }
}
