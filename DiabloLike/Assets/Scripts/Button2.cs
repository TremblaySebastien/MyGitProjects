using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour
{

    public Renderer rend;
    public GameObject m_Button_2;


    public Material m_ActivatedColor;
    public Material m_NotActivatedColor;
    public Material m_BadActivatedColor;
    public bool m_Button_2_Activated = false;


    void Start()
    {
        rend = GetComponent<Renderer>();


    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            rend.material = m_ActivatedColor;
            m_Button_2_Activated = true;

        }

    }

    private void Update()
    {



    }
}
