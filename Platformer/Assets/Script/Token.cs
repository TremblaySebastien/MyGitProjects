using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{

    public float TIMER_VALUE = 0.1f;
    public float m_TokenRotateSpeed = 5f;

    private void Start ()
    {
		
	}
	
	
	private void Update ()
    {
        Invoke("RotateToken", TIMER_VALUE);

    }

    private void OnTriggerEnter(Collider aCol)
    {
        PlayerController player = aCol.gameObject.GetComponent<PlayerController>();
        if (aCol.gameObject.tag == "Player")
        {
            //Destroy(m_token);
            player.ChangeColor(gameObject.GetComponent <Renderer>().material);
            player.PowerUpSpeed();
            gameObject.SetActive(false);
        } 
    }

    private void RotateToken()
    {
        transform.Rotate(Vector3.forward, m_TokenRotateSpeed);
    }
}

//Renderer rend;
//public GameObject m_Button_1;


//public Material m_ActivatedColor;
//public Material m_NotActivatedColor;
//public Material m_BadActivatedColor;
//public bool m_Button_1_Activated = false;


//void Start()
//{
//    rend = GetComponent<Renderer>();


//}

//private void OnCollisionEnter(Collision col)
//{
//    if (col.gameObject.tag == "Projectile")
//    {
//        rend.material = m_ActivatedColor;
//        m_Button_1_Activated = true;

//    }

//}
