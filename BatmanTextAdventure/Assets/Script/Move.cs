using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float m_Speed = 20f;
    public Rigidbody2D m_RigidBody;
    public Vector3 m_MoveDir = new Vector3();
    public Animator m_Animation;
    private SpriteRenderer mySpriteRenderer;
    public GameObject m_Interaction;
    public ConversationManager m_ConversationManager;

    void Start ()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animation = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
        ToMove();
	}

    public void ToMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.right * -m_Speed);
            mySpriteRenderer.flipX = true;
            m_Animation.SetBool("IsRunning", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * m_Speed);
            mySpriteRenderer.flipX = false;
            m_Animation.SetBool("IsRunning", true);
        }
        else
        {
            m_Animation.SetBool("IsRunning", false);
        }
    }

    public void OnTriggerEnter2D(Collider2D aCol)
    {
        if(aCol.gameObject.tag == "Riddler")
        {
            m_Animation.SetBool("IsTalking", true);
            m_Interaction.gameObject.SetActive(true);
            ConversationManager.m_Step = 1;
            ConversationManager.m_DialogueActivated = true;
        }
        
    }

    public void OnTriggerExit2D(Collider2D aCol)
    {
        if (aCol.gameObject.tag == "Riddler")
        {
            m_Animation.SetBool("IsTalking", false);
            m_Interaction.gameObject.SetActive(false);
            ConversationManager.m_Step = 0;
            ConversationManager.m_DialogueActivated = false;
        }
    }
}
