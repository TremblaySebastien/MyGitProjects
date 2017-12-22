using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMove2 : MonoBehaviour
{ 
    public GameObject m_Button_3;
    public GameObject m_Button_4;
    public GameObject m_Button_5;
    public GameObject m_Button_6;
    public GameObject m_Gate2;
    private Button3 m_ButtonScript3;
    private Button4 m_ButtonScript4;
    private Button5 m_ButtonScript5;
    private Button6 m_ButtonScript6;
    public bool m_Button3ActivatedSecond = false;
    public bool m_Button4ActivatedFirst = false;
    public bool m_Button5ActivatedLast = false;
    public bool m_Button6ActivatedThird = false;
    public const float TIMER_VALUE = 2f;

    void Start()
    {
        m_ButtonScript3 = m_Button_3.GetComponent<Button3>();
        m_ButtonScript4 = m_Button_4.GetComponent<Button4>();
        m_ButtonScript5 = m_Button_5.GetComponent<Button5>();
        m_ButtonScript6 = m_Button_6.GetComponent<Button6>();
    }



    private void Update()
    {
        if (m_ButtonScript4.m_Button_4_Activated == true && m_ButtonScript3.m_Button_3_Activated == false &&
            m_ButtonScript5.m_Button_5_Activated == false && m_ButtonScript6.m_Button_6_Activated == false)
        {
            m_Button4ActivatedFirst = true;
        }

        if (m_Button4ActivatedFirst == true && m_ButtonScript3.m_Button_3_Activated == true &&
            m_ButtonScript5.m_Button_5_Activated == false && m_ButtonScript6.m_Button_6_Activated == false)
        {
            m_Button3ActivatedSecond = true;
        }

        if (m_Button4ActivatedFirst == true && m_Button3ActivatedSecond == true &&
            m_ButtonScript6.m_Button_6_Activated == true && m_ButtonScript5.m_Button_5_Activated == false)
        {
            m_Button6ActivatedThird = true;
        }

        if (m_Button4ActivatedFirst == true && m_Button3ActivatedSecond == true &&
            m_Button6ActivatedThird == true && m_ButtonScript5.m_Button_5_Activated == true)
        {
            m_Button5ActivatedLast = true;
        }

        if (m_Button4ActivatedFirst == false && (m_ButtonScript3.m_Button_3_Activated == true || m_ButtonScript5.m_Button_5_Activated == true || m_ButtonScript6.m_Button_6_Activated == true))
        {
            WrongOrderAndReset();
        }

        if (m_Button4ActivatedFirst == true && m_Button3ActivatedSecond == true && m_ButtonScript5.m_Button_5_Activated == true && m_Button6ActivatedThird == false)
        {
            WrongOrderAndReset();
        }

        if (m_Button4ActivatedFirst == true && m_Button3ActivatedSecond == true && m_Button6ActivatedThird == true && m_ButtonScript5.m_Button_5_Activated == true)
        {
            GoodOrderAndResetColor();
            DestroyGate();
        }
    }

    private void DestroyGate()
    {
        Destroy(m_Gate2, 2f);
    }

    private void GoodOrderAndResetColor()
    {
        Invoke("TimerEnd", TIMER_VALUE);
    }

    private void WrongOrderAndReset()
    {
        m_ButtonScript3.m_Button_3_Activated = false;
        m_ButtonScript4.m_Button_4_Activated = false;
        m_ButtonScript5.m_Button_5_Activated = false;
        m_ButtonScript6.m_Button_6_Activated = false;
        m_Button4ActivatedFirst = false;
        m_Button3ActivatedSecond = false;
        m_Button6ActivatedThird = false;
        m_Button5ActivatedLast = false;
        m_ButtonScript3.rend.material = m_ButtonScript3.m_BadActivatedColor;
        m_ButtonScript4.rend.material = m_ButtonScript4.m_BadActivatedColor;
        m_ButtonScript5.rend.material = m_ButtonScript5.m_BadActivatedColor;
        m_ButtonScript6.rend.material = m_ButtonScript6.m_BadActivatedColor;
        Invoke("TimerEnd", TIMER_VALUE);
    }

    public void TimerEnd()
    {
        m_ButtonScript3.rend.material = m_ButtonScript3.m_NotActivatedColor;
        m_ButtonScript4.rend.material = m_ButtonScript4.m_NotActivatedColor;
        m_ButtonScript5.rend.material = m_ButtonScript5.m_NotActivatedColor;
        m_ButtonScript6.rend.material = m_ButtonScript6.m_NotActivatedColor;
    }
}
