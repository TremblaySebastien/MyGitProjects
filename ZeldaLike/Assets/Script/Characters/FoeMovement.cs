using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeMovement : MonoBehaviour, ISauvegardable
{
    public GameObject m_Foe1;
    public Animator m_Link;
    private const float TILE_SIZE = 1f;
    private Vector2 m_Xvelocity = new Vector2(1, 0);
    private Vector2 m_Yvelocity = new Vector2(0, 1);
    private float m_FoeSpeed = 0f;
    public Rigidbody2D m_RigidBody;
    public bool m_IsMoving;
    private Vector2 m_InitialPos;
    private Vector2 m_WantedPos;
    private float m_PercentageCompletion;
    public Animator m_Animator;
    public SpriteRenderer m_SpriteRenderer;
    public MapGenerator m_MapGenerator;
    public MapGenerator.Coord ActualFoePosition;

    public void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        ApplySauvegarde();
        SauvegardeManager.ElementsToRefresh.Add(this);
    }

    public void OnDestroy()
    {
        SauvegardeManager.ElementsToRefresh.Remove(this);
    }

    public void ApplySauvegarde()
    {
        this.m_IsMoving = SauvegardeManager.sauvegarde.m_IsMoving;
    }

    public void FixedUpdate()
    {
        ActualFoePosition = MapGenerator.ConvertWorldToIndex(transform.position);
        ToMove();
    }

    public void ToMove()
    {
        MapGenerator.Coord FoePosition = MapGenerator.ConvertWorldToIndex(transform.position);
        int rand = Random.Range(-1, 2);
        int rand2 = Random.Range(-1, 2);
        float askMoveHorizontal = rand;
        float askMoveVertical = rand2;
        int indexvalueleft = GetTile(FoePosition.i - 1, FoePosition.j);
        int indexvalueright = GetTile(FoePosition.i + 1, FoePosition.j);
        int indexvaluetop = GetTile(FoePosition.i, FoePosition.j - 1);
        int indexvaluedown = GetTile(FoePosition.i, FoePosition.j + 1);
       

        if (!m_IsMoving)
        {
            m_Animator.SetFloat("x", askMoveHorizontal);
            m_Animator.SetFloat("y", askMoveVertical);
            m_Animator.SetBool("isWalking", false);

            if (askMoveHorizontal != 0)
            {
                m_PercentageCompletion = 0;
                m_IsMoving = true;
                if (askMoveHorizontal < 0)
                {
                    if (indexvalueleft == (int)MapGenerator.WalkableTile || indexvalueleft == ((int)MapGenerator.KeyTile) || indexvalueleft == ((int)MapGenerator.RupeeTile) || indexvalueleft == ((int)MapGenerator.BombTile))
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Xvelocity * TILE_SIZE) * askMoveHorizontal;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("isWalking", true);
                        m_SpriteRenderer.flipX = false;
                    }
                    else
                    {
                        m_IsMoving = false;
                    }
                }
                else if (askMoveHorizontal > 0)
                {
                    if (indexvalueright == ((int)MapGenerator.WalkableTile) || indexvalueright == ((int)MapGenerator.KeyTile) || indexvalueright == ((int)MapGenerator.RupeeTile) || indexvalueright == ((int)MapGenerator.BombTile))
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Xvelocity * TILE_SIZE) * askMoveHorizontal;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("isWalking", true);
                        m_SpriteRenderer.flipX = true;
                    }
                    else
                    {
                        m_IsMoving = false;
                    }
                }
                else if (askMoveHorizontal == 0)
                {
                    m_IsMoving = false;
                    m_Animator.SetBool("isWalking", false);
                }
            }
            else if (askMoveVertical != 0)
            {
                m_PercentageCompletion = 0;
                m_IsMoving = true;
                if (askMoveVertical < 0)
                {
                    if (indexvaluedown == (int)MapGenerator.WalkableTile || indexvaluedown == ((int)MapGenerator.KeyTile) || indexvaluedown == ((int)MapGenerator.RupeeTile) || indexvaluedown == ((int)MapGenerator.BombTile))
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Yvelocity * TILE_SIZE) * askMoveVertical;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("isWalking", true);
                    }
                    else
                    {
                        m_IsMoving = false;
                    }
                }
                else if (askMoveVertical > 0)
                {
                    if (indexvaluetop == (int)MapGenerator.WalkableTile || indexvaluetop == ((int)MapGenerator.KeyTile) || indexvaluetop == ((int)MapGenerator.RupeeTile) || indexvaluetop == ((int)MapGenerator.BombTile))
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Yvelocity * TILE_SIZE) * askMoveVertical;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("isWalking", true);
                    }
                    else
                    {
                        m_IsMoving = false;
                    }
                }
                else if (askMoveVertical == 0)
                {
                    m_IsMoving = false;
                    m_Animator.SetBool("isWalking", false);
                }
            }
        }
        else
        {
            m_PercentageCompletion += Time.fixedDeltaTime * m_FoeSpeed;
            m_PercentageCompletion = Mathf.Clamp(m_PercentageCompletion, 0, 1);
            m_RigidBody.transform.position = Vector2.Lerp(m_InitialPos, m_WantedPos, m_PercentageCompletion);
            if (Vector2.Distance(m_RigidBody.transform.position, m_WantedPos) == 0f)
            {
                m_IsMoving = false;
            }
        }
    }

    private int GetTile(int i, int j)
    {
        if (i < 0 || i > MapGenerator.m_PlayerPos.GetLength(0) || j < 0 || j > MapGenerator.m_PlayerPos.GetLength(1))
        {
            return 2;
        }
        else
        {
            return MapGenerator.m_PlayerPos[i, j];
        }
    }
}
