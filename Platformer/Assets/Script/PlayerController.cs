using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Renderer rend;
    private Token m_TokenScript;
    public GameObject m_Token;
    public float m_SpeedMax = 60f;

    public float m_MoveSpeed = 10f;
    private Rigidbody m_RigidBody;
    private Vector3 m_MoveDir = new Vector3();
    public float rotateSpeed = 5f;
    public bool reverseRotation = false;
    public float m_JumpForce = 10f;
    public bool m_CanJump;

 

    // Use this for initialization
    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        if (m_RigidBody == null)
        {
            Debug.LogError("The GameObject has no RigidBody, This script needs a rigidbody to work, please add one.");
        }
        m_CanJump = true;

        m_TokenScript = m_Token.GetComponent<Token>();
    }
    // forward correspond à l'axe des z
    // Update is called once per frame
    private void Update()
    {
        UpdateMovementInput();
        FixeUpdate();
        UpdateRotationInput();
        JumpInput();


    }

    private void UpdateRotationInput()
    {
        
    }

    private void FixeUpdate()
    {
        m_MoveDir.y = m_RigidBody.velocity.y;
        m_RigidBody.velocity = m_MoveDir;

    }

    private void UpdateMovementInput()
    {
        
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {

                m_MoveDir = -transform.right * m_MoveSpeed;
                transform.localEulerAngles = new Vector3(0f, -90f, 0f);

            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {

                m_MoveDir = -transform.right * m_MoveSpeed;
                transform.localEulerAngles = new Vector3(0f, 90f, 0f);

            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                m_MoveDir = new Vector3(0f, m_RigidBody.velocity.y, 0f);


            }
        
        

    }

    private void Jump()
    {
        m_RigidBody.AddForce(transform.up * m_JumpForce);


    }

    private void OnTriggerEnter(Collider aCol)
    {
        if (aCol.gameObject.tag == "Ground")
        {
            m_CanJump = true;
           
        }

      

    }

    private void OnTriggerExit(Collider aCol)
    {
        if (aCol.gameObject.tag == "Ground")
        {
            m_CanJump = false;
           
        }

    }

    private void JumpInput()
    {
        if (m_CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void ChangeColor(Material aMat)
    {
        rend.material = aMat;
        
    }

    public void PowerUpSpeed()
    {
        if(m_MoveSpeed <= m_SpeedMax)
        {
            m_MoveSpeed = m_MoveSpeed * 1.5f;
        }
        else
        {
            m_MoveSpeed = m_SpeedMax;
        }
    }

}


// transform.position = la propre position
// tranform.localposition = position par rapport au parent
//tranform.localScale = pour la e scale de l'objet
//transform.eulerAngle;
//transform.LocalEulerAngles;