using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Projectiles;
    public Rigidbody m_Rigidbody;
    public float m_Speed;

    public void Init(float aSpeed)
    {
        m_Speed = aSpeed;
    }

    private void fixedUpdate()
    {
        m_Rigidbody.velocity = transform.forward * m_Speed;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Projectile" || col.gameObject.tag == "Wall")
        {
            DestroyObject(Projectiles, 1.5f);
        }
    } 
}
