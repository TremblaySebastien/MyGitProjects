using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMove3 : MonoBehaviour
{
    public GameObject m_Button_A;
    public GameObject m_Button_B;
    public GameObject m_Button_C;
    public GameObject m_Button_D;
    public GameObject m_Button_E;
    public GameObject m_Button_F;
    public GameObject m_Gate3;

    private ButtonA m_ButtonScriptA;
    private ButtonB m_ButtonScriptB;
    private ButtonC m_ButtonScriptC;
    private ButtonD m_ButtonScriptD;
    private ButtonE m_ButtonScriptE;
    private ButtonF m_ButtonScriptF;

    public const float TIMER_VALUE = 5f;

    void Start()
    {
        m_ButtonScriptA = m_Button_A.GetComponent<ButtonA>();
        m_ButtonScriptB = m_Button_B.GetComponent<ButtonB>();
        m_ButtonScriptC = m_Button_C.GetComponent<ButtonC>();
        m_ButtonScriptD = m_Button_D.GetComponent<ButtonD>();
        m_ButtonScriptE = m_Button_E.GetComponent<ButtonE>();
        m_ButtonScriptF = m_Button_F.GetComponent<ButtonF>();
    }
    private void Update()
    {
        if (m_ButtonScriptA.m_Button_A_Activated == true && m_ButtonScriptB.m_Button_B_Activated == true &&
            m_ButtonScriptC.m_Button_C_Activated == true && m_ButtonScriptD.m_Button_D_Activated == true &&
            m_ButtonScriptE.m_Button_E_Activated == true && m_ButtonScriptF.m_Button_F_Activated == true)
        {
            DestroyGate();
        }
    }

    private void DestroyGate()
    {
        Destroy(m_Gate3, 2f);
    }
}
