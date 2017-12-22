using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float m_Speed = 10f;
    public const float MAX_DISTANCE = 1000f;
    public Vector3 m_Position;
    private Rigidbody m_Rigidbody;
    public GameObject m_Projectile;
    public Transform m_ProjectileSpawnPoint;
    public float m_ProjectileSpeed = 10f;
    private bool m_CanMove = false; 

    private void Start()
    {
        m_Position = transform.position;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            locatePosition();  
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void FixedUpdate()
    {
        if(m_CanMove == true)
        {
            moveToPosition();

            if (Vector3.Distance(m_Position, transform.position) < 1f)
            {
                m_CanMove = false;
            }
        }
    }

    void locatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, MAX_DISTANCE))
        {
            if(hit.transform.tag == "Ground")
            {
                m_Position = hit.point;
                Debug.Log(m_Position);
                m_CanMove = true;
            }
           
            if(hit.transform.tag == "Projectile")
            {
                GameObject projectile = GameObject.Instantiate(m_Projectile, m_ProjectileSpawnPoint.position, Quaternion.identity);
                Projectile script = projectile.GetComponent<Projectile>();
                script.Init(m_ProjectileSpeed);
                m_CanMove = true;
            }
            Debug.Log(hit.transform.gameObject.name);
        }
    }

    private void moveToPosition()
    {
        Vector3 movement = m_Position - transform.position;
        movement.Normalize();
        m_Rigidbody.velocity = movement * m_Speed;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "TreasureChest")
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
