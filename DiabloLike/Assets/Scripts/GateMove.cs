using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMove : MonoBehaviour
{
    public GameObject m_Button_1;
    public GameObject m_Button_2;
    public GameObject m_Gate;
    private Button1 m_ButtonScript1;
    private Button2 m_ButtonScript2;
    public bool m_Button1ActivatedFirst = false;
    public bool m_Button2ActivatedFirst = false;

    public const float TIMER_VALUE = 5f;

    void Start()
    {
        m_ButtonScript1 = m_Button_1.GetComponent<Button1>();
        m_ButtonScript2 = m_Button_2.GetComponent<Button2>();  
    }

    private void Update()
    {
        if(m_ButtonScript1.m_Button_1_Activated == true && m_ButtonScript2.m_Button_2_Activated == false)
        {
            m_Button1ActivatedFirst = true;

        }

        if (m_ButtonScript1.m_Button_1_Activated == false && m_ButtonScript2.m_Button_2_Activated == true)
        {
            m_Button2ActivatedFirst = true;

        }

        if (m_Button1ActivatedFirst == true && m_ButtonScript2.m_Button_2_Activated == true)
        {
            DestroyGate();
        }

        if (m_Button1ActivatedFirst == false && m_Button2ActivatedFirst == true)
        {
            WrongOrderAndResetColor();
        }
    }
    
    private void DestroyGate()
    {
        Destroy(m_Gate, 2f);
    }

    private void WrongOrderAndResetColor()
    {
        m_ButtonScript2.m_Button_2_Activated = false;
        m_Button2ActivatedFirst = false;
        m_ButtonScript2.rend.material = m_ButtonScript2.m_BadActivatedColor;
        Invoke("TimerEnd", TIMER_VALUE);   
    }

    public void TimerEnd()
    {
        m_ButtonScript2.rend.material = m_ButtonScript2.m_NotActivatedColor;
    }
}
