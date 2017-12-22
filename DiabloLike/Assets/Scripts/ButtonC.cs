using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonC : MonoBehaviour
{
    Renderer rend;
    public GameObject m_Button_C;
    public Material m_ActivatedColor;
    public Material m_NotActivatedColor;
    public bool m_Button_C_Activated = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            rend.material = m_ActivatedColor;
            m_Button_C_Activated = true;
        }
    }
}