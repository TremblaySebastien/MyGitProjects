using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public GameObject m_Target;
    public float m_FollowSpeed = 3f;
    private Vector3 m_Offset = new Vector3();
    private float m_LimitTop = 190f;
    private float m_LimitBottom = -50f;
    private float m_LimitLeft = -33f;
    private float m_LimitRight = 29f;
    private Vector3 m_CameraPosition;
    

    private void Start()
    {
        m_Offset = m_Target.transform.position - transform.position;
    }


    private void FixedUpdate()
    {
        m_CameraPosition = m_Target.transform.position - m_Offset;

        if ((m_Target.transform.position.z > m_LimitTop) && (m_Target.transform.position.x < m_LimitLeft))
        {
            m_CameraPosition.z = m_LimitTop - m_Offset.z;
            m_CameraPosition.x = m_LimitLeft;

        }
        else if ((m_Target.transform.position.z > m_LimitTop) && (m_Target.transform.position.x > m_LimitRight))
        {
            m_CameraPosition.z = m_LimitTop - m_Offset.z;
            m_CameraPosition.x = m_LimitRight;

        }
        else if ((m_Target.transform.position.z < m_LimitBottom + m_Offset.z) && (m_Target.transform.position.x < m_LimitLeft))
        {
            m_CameraPosition.z = m_LimitBottom - m_Offset.z;
            m_CameraPosition.x = m_LimitLeft;

        }
        else if ((m_Target.transform.position.z < m_LimitBottom + m_Offset.z) && (m_Target.transform.position.x > m_LimitRight))
        {
            m_CameraPosition.z = m_LimitBottom - m_Offset.z;
            m_CameraPosition.x = m_LimitRight;

        }
        else if (m_Target.transform.position.z < m_LimitBottom + m_Offset.z)
        {
            m_CameraPosition.z = m_LimitBottom - m_Offset.z;
        }
        else if (m_Target.transform.position.x < m_LimitLeft)
        {
            m_CameraPosition.x = m_LimitLeft;
        }
        else if (m_Target.transform.position.x > m_LimitRight)
        {
            m_CameraPosition.x = m_LimitRight;
        }
        else if (m_Target.transform.position.z > m_LimitTop)
        {
            m_CameraPosition.z = m_LimitTop - m_Offset.z;
        }




        transform.position = Vector3.Lerp(transform.position, m_CameraPosition, m_FollowSpeed * Time.fixedDeltaTime);
    }
}
