using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoeMovement : MonoBehaviour
{

    private const float TILE_SIZE = 0.38f;
    public Vector2 m_Xvelocity = new Vector2(1, 0);
    public Vector2 m_Yvelocity = new Vector2(0, 1);
    public float m_Speed;
    public Rigidbody2D m_RigidBody;
    public bool m_IsMoving;
    private Vector2 m_InitialPos;
    private Vector2 m_WantedPos;
    private float m_PercentageCompletion;
    public Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    public LayerMask m_LayerMask;
    public LayerMask m_LayerMask2;
    public Vector3 m_XvelocityRight = new Vector3(TILE_SIZE, 0, 0);
    public Vector3 m_XvelocityLeft = new Vector3(-TILE_SIZE, 0, 0);
    public Vector3 m_YvelocityUp = new Vector3(0, TILE_SIZE, 0);
    public Vector3 m_YvelocityDown = new Vector3(0, -TILE_SIZE, 0);

    public void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate()
    {
        ToMove();
        FoeDie();
    }

    public void ToMove()
    {
        int rand = Random.Range(-1, 2);
        int rand2 = Random.Range(-1, 2);
        if (!m_IsMoving)
        {
            
            int askMoveHorizontal = rand;
            int askMoveVertical = rand2;

            m_Animator.SetBool("IsGoingLR", false);
            m_Animator.SetBool("IsIdle", true);
            m_Animator.SetBool("IsGoingUp", false);
            m_Animator.SetBool("IsGoingDown", false);
            m_Animator.SetBool("IsDead", false);

            if (askMoveHorizontal != 0)
            {
                m_PercentageCompletion = 0;
                m_Animator.SetBool("IsIdle", false);
                m_IsMoving = true;

                RaycastHit2D hit = Physics2D.Raycast(m_RigidBody.transform.position, askMoveHorizontal * m_Xvelocity, TILE_SIZE, m_LayerMask);

                if (hit.collider != null)
                {
                    m_IsMoving = false;
                    askMoveHorizontal = rand;
                    askMoveVertical = rand2;
                }
                else if (hit.collider == null)
                {
                    //m_IsMoving = true;
                    if (askMoveHorizontal <= 0)
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Xvelocity * TILE_SIZE) * askMoveHorizontal;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("IsGoingLR", true);
                        m_SpriteRenderer.flipX = true;
                    }
                    else if (askMoveHorizontal >= 0)
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Xvelocity * TILE_SIZE) * askMoveHorizontal;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("IsGoingLR", true);
                        m_SpriteRenderer.flipX = false;
                    }
                    else if (askMoveHorizontal == 0)
                    {
                        m_IsMoving = false;
                        m_Animator.SetBool("IsGoingLR", false);
                    }
                }
            }
            else if (askMoveVertical != 0)
            {
                m_PercentageCompletion = 0;
                m_Animator.SetBool("IsIdle", false);
                m_IsMoving = true;

                RaycastHit2D hit = Physics2D.Raycast(m_RigidBody.transform.position, askMoveVertical * m_Yvelocity, TILE_SIZE, m_LayerMask);

                if (hit.collider != null)
                {
                    m_IsMoving = false;
                    askMoveHorizontal = rand;
                    askMoveVertical = rand2;
                }
                else if (hit.collider == null)
                {
                    //m_IsMoving = true;
                    if (askMoveVertical <= 0)
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Yvelocity * TILE_SIZE) * askMoveVertical;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("IsGoingDown", true);
                    }
                    else if (askMoveVertical >= 0)
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Yvelocity * TILE_SIZE) * askMoveVertical;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("IsGoingUp", true);
                    }
                    else if (askMoveVertical == 0)
                    {
                        m_IsMoving = false;
                        m_Animator.SetBool("IsGoingUp", false);
                        m_Animator.SetBool("IsGoingDown", false);
                    }
                }
            }
        }
        else
        {
            m_PercentageCompletion += Time.fixedDeltaTime * m_Speed;
            m_PercentageCompletion = Mathf.Clamp(m_PercentageCompletion, 0, 1);

            m_RigidBody.transform.position = Vector2.Lerp(m_InitialPos, m_WantedPos, m_PercentageCompletion);
            if (Vector2.Distance(m_RigidBody.transform.position, m_WantedPos) == 0f)
            {
                m_IsMoving = false;
            }
        }

        

    }

    public void FoeDie()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position, 1f, m_LayerMask2);

        if (hit.collider != null)
        {
            StartCoroutine(FoeIsDying());
        }
    }

    public IEnumerator FoeIsDying()
    {
        m_IsMoving = false;
        m_Animator.SetBool("IsGoingLR", false);
        m_Animator.SetBool("IsIdle", false);
        m_Animator.SetBool("IsGoingUp", false);
        m_Animator.SetBool("IsGoingDown", false);
        m_Animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(1.15f);
        gameObject.SetActive(false);
    }

    

}
