using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button1 : MonoBehaviour
{


    Renderer rend;
    public GameObject m_Button_1;


    public Material m_ActivatedColor;
    public Material m_NotActivatedColor;
    public Material m_BadActivatedColor;
    public bool m_Button_1_Activated = false;


    void Start()
    {
        rend = GetComponent<Renderer>();


    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            rend.material = m_ActivatedColor;
            m_Button_1_Activated = true;

        }
        
    }


}
