using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
   
    Renderer rend;
    public GameObject m_Button_1;
    public GameObject m_Button_2;
    public GameObject m_Gate;
    public Material m_ActivatedColor;
    public Material m_NotActivatedColor;
    public Material m_BadActivatedColor;
    //private Vector3 m_GatePosition;
    //private float m_GateDown = -30f;
    //public bool GateDown = false;
    public bool m_Button_Activated;
    //public bool m_Button2_Activated = false;
    //Transform FirstHit;
    //Transform SecondHit;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            rend.material = m_ActivatedColor;
            m_Button_Activated = true;
            
        }
        
    }

    private void Update()
    {
        if (m_Button_Activated == true)
        {
            Destroy(m_Gate);
        }


    }

    
    
    
}