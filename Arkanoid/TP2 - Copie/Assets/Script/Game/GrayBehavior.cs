using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayBehavior : MonoBehaviour {
    public GameObject m_parent;
    public static int nCollision = 0;
    public static bool m_brickRemoved;

    public void OnCollisionEnter2D(Collision2D col) { 
        if(col.gameObject.tag == "ball" && m_brickRemoved == false) {
            m_parent.SetActive(false);   
        }

        if (m_parent.activeInHierarchy == false) {
            m_brickRemoved = true;
        }
    }
}
