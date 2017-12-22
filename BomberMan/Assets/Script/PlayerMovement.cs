using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
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
    public LayerMask m_LayerMask3;
    public LayerMask m_LayerMask4;
    public GameObject m_Bomb;
    public GameObject m_MiddleFire;
    public GameObject m_HorizontalFire;
    public GameObject m_VerticalFire;
    public GameObject m_TopFire;
    public GameObject m_BottomFire;
    public GameObject m_LeftFire;
    public GameObject m_RightFire;
    public int m_BombCount = 0;
    public int m_BombMaxQty = 1;
    public bool m_BombDestroyed = false;
    public float m_HurtIntervalTime = 1f;
    public float m_BombDuration = 5f;
    public float m_FireDuration = 0.05f;
    public float m_PowerUpDuration;
    public Vector3 m_XvelocityRight = new Vector3(TILE_SIZE, 0, 0);
    public Vector3 m_XvelocityLeft = new Vector3(-TILE_SIZE, 0, 0);
    public Vector3 m_YvelocityUp = new Vector3(0, TILE_SIZE, 0);
    public Vector3 m_YvelocityDown = new Vector3(0, -TILE_SIZE, 0);
    public GameManager m_GameManager;
    public bool IsDead = false;
    public int m_Health;
    public LevelData m_LevelData;
    public Slider m_HealthBar;
    public bool m_DamageIsDone;
    public GameObject m_SpeedCollectible;
    public GameObject m_BombCollectible;
    public GameObject m_SpeedCollectibleUI;
    public GameObject m_BombCollectibleUI;

    public void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_SpeedCollectibleUI.gameObject.SetActive(false);
        m_BombCollectibleUI.gameObject.SetActive(false);

    }

    private void Start()
    {
        m_Health = m_LevelData.m_Hp;
        m_Speed = m_LevelData.m_PlayerSpeed;
        m_BombDuration = m_LevelData.m_BombDuration;
        m_PowerUpDuration = m_LevelData.m_PowerUpDuration;
        m_BombCount = m_LevelData.m_BombCount;
        m_BombMaxQty = m_LevelData.m_BombMaxQty;
}

    public void FixedUpdate()
    {
        ToMove();
        PutBomb();
        TrapFoeFireDamage();
        PickSpeedCollectible();
        PickBombCollectible();

    }

    public void ToMove()
    {
        if (!m_IsMoving)
        {
            float askMoveHorizontal = Input.GetAxisRaw("Horizontal");
            float askMoveVertical = Input.GetAxisRaw("Vertical");

            m_Animator.SetBool("IsGoingLR", false);
            m_Animator.SetBool("IsIdle", true);
            m_Animator.SetBool("IsGoingUp", false);
            m_Animator.SetBool("IsGoingDown", false);

            if (askMoveHorizontal != 0)
            {
                m_PercentageCompletion = 0;
                m_Animator.SetBool("IsIdle", false);
                m_IsMoving = true;

                RaycastHit2D hit = Physics2D.Raycast(m_RigidBody.transform.position, askMoveHorizontal * m_Xvelocity, TILE_SIZE, m_LayerMask);

                if(hit.collider != null)
                {
                    m_IsMoving = false;
                }
                else if (hit.collider == null)
                {
                    m_IsMoving = true;
                    if (askMoveHorizontal < 0)
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Xvelocity * TILE_SIZE) * askMoveHorizontal;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("IsGoingLR", true);
                        m_SpriteRenderer.flipX = true;
                    }
                    else if (askMoveHorizontal > 0)
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
                }
                else if (hit.collider == null)
                {
                    m_IsMoving = true;
                    if (askMoveVertical < 0)
                    {
                        m_IsMoving = true;
                        m_InitialPos = m_RigidBody.transform.position;
                        Vector2 offset = (m_Yvelocity * TILE_SIZE) * askMoveVertical;
                        m_WantedPos = m_InitialPos + offset;
                        m_Animator.SetBool("IsGoingDown", true);
                    }
                    else if (askMoveVertical > 0)
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

    public void PutBomb()
    {
        if (m_BombCount < m_BombMaxQty && m_IsMoving == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(BombTimer());
            }    
        }
    }

    public IEnumerator BombTimer()
    {
        GameObject Bomb = Instantiate(m_Bomb);
        Bomb.transform.position = transform.position;

        m_BombCount++;

        yield return new WaitForSeconds(m_BombDuration);

        
        m_BombCount--;

        GameObject MiddleFire = Instantiate(m_MiddleFire);
        MiddleFire.transform.position = Bomb.transform.position;

        RaycastHit2D hitEdgeRight = Physics2D.Raycast(Bomb.transform.position, m_XvelocityRight, TILE_SIZE * 2, m_LayerMask);
        if (hitEdgeRight.collider != null)
        {
            if (hitEdgeRight.collider.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
            {
                GameManager.Instance.DestroyWall(hitEdgeRight.collider.gameObject);
            }
        }
        else if (hitEdgeRight.collider == null)
        {

            GameObject RightEdgeFire = Instantiate(m_RightFire);
            RightEdgeFire.transform.position = (MiddleFire.transform.position + m_XvelocityRight * 2);
            yield return new WaitForSeconds(m_FireDuration);
            Destroy(RightEdgeFire);
        }

        RaycastHit2D hitRight = Physics2D.Raycast(Bomb.transform.position, m_XvelocityRight, TILE_SIZE, m_LayerMask);
        if (hitRight.collider != null)
        {
            if (hitRight.collider.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
            {
                GameManager.Instance.DestroyWall(hitRight.collider.gameObject);
                
            }
        }
        else if (hitRight.collider == null)
        {
            GameObject RightFire = Instantiate(m_HorizontalFire);
            RightFire.transform.position = (MiddleFire.transform.position + m_XvelocityRight);
            yield return new WaitForSeconds(m_FireDuration);
            Destroy(RightFire);
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(Bomb.transform.position, m_XvelocityLeft, TILE_SIZE, m_LayerMask);
        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
            {
                GameManager.Instance.DestroyWall(hitLeft.collider.gameObject);
            }
        }
        else if (hitLeft.collider == null)
        {
            GameObject LeftFire = Instantiate(m_HorizontalFire);
            LeftFire.transform.position = (MiddleFire.transform.position + m_XvelocityLeft);
            yield return new WaitForSeconds(m_FireDuration);
            Destroy(LeftFire);
        }

        RaycastHit2D hitUp = Physics2D.Raycast(Bomb.transform.position, m_YvelocityUp, TILE_SIZE, m_LayerMask);
        if (hitUp.collider != null)
        {
            if (hitUp.collider.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
            {
                GameManager.Instance.DestroyWall(hitUp.collider.gameObject);
            }
        }
        else if (hitUp.collider == null)
        {
            GameObject UpFire = Instantiate(m_VerticalFire);
            UpFire.transform.position = (MiddleFire.transform.position + m_YvelocityUp);
            yield return new WaitForSeconds(m_FireDuration);
            Destroy(UpFire);
        }

        RaycastHit2D hitDown = Physics2D.Raycast(Bomb.transform.position, m_YvelocityDown, TILE_SIZE, m_LayerMask);
        if (hitDown.collider != null)
        {
            if (hitDown.collider.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
            {
                GameManager.Instance.DestroyWall(hitDown.collider.gameObject);
            }
        }
        else if (hitDown.collider == null)
        {

            GameObject DownFire = Instantiate(m_VerticalFire);
            DownFire.transform.position = (MiddleFire.transform.position + m_YvelocityDown);
            yield return new WaitForSeconds(m_FireDuration);
            Destroy(DownFire);
        }

        RaycastHit2D hitTopUp = Physics2D.Raycast(Bomb.transform.position, m_YvelocityUp, TILE_SIZE*2, m_LayerMask);
        if (hitTopUp.collider != null)
        {
            if (hitTopUp.collider.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
            {
                GameManager.Instance.DestroyWall(hitTopUp.collider.gameObject);
            }
        }
        else if (hitTopUp.collider == null)
        {

            GameObject TopUpFire = Instantiate(m_TopFire);
            TopUpFire.transform.position = (MiddleFire.transform.position + m_YvelocityUp*2);
            yield return new WaitForSeconds(m_FireDuration);
            Destroy(TopUpFire);
        }

        RaycastHit2D hitBottomDown = Physics2D.Raycast(Bomb.transform.position, m_YvelocityDown, TILE_SIZE * 2, m_LayerMask);
        if (hitBottomDown.collider != null)
        {
            if (hitBottomDown.collider.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
            {
                GameManager.Instance.DestroyWall(hitBottomDown.collider.gameObject);
            }
        }
        else if (hitBottomDown.collider == null)
        {

            GameObject BottomDownFire = Instantiate(m_BottomFire);
            BottomDownFire.transform.position = (MiddleFire.transform.position + m_YvelocityDown * 2);
            yield return new WaitForSeconds(m_FireDuration);
            Destroy(BottomDownFire);
        }

        RaycastHit2D hitEdgeLeft = Physics2D.Raycast(Bomb.transform.position, m_XvelocityLeft, TILE_SIZE * 2, m_LayerMask);
        if (hitEdgeLeft.collider != null)
        {
            if (hitEdgeLeft.collider.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
            {
                GameManager.Instance.DestroyWall(hitEdgeLeft.collider.gameObject);
            }
        }
        else if (hitEdgeLeft.collider == null)
        {

            GameObject LeftEdgeFire = Instantiate(m_LeftFire);
            LeftEdgeFire.transform.position = (MiddleFire.transform.position + m_XvelocityLeft * 2);
            yield return new WaitForSeconds(m_FireDuration);
            Destroy(LeftEdgeFire);
        }

        Destroy(MiddleFire);
        Destroy(Bomb); 
    }

    public void TrapFoeFireDamage()
    {
        RaycastHit2D DamageHit = Physics2D.Raycast(transform.position, transform.position, 0.01f, m_LayerMask3);
        if (DamageHit.collider != null && m_DamageIsDone == false)
        {
            m_HealthBar.value -= 1f;
            m_DamageIsDone = true;
            StartCoroutine(HpDecreasingDuration());
        }
    }

    public void PickSpeedCollectible()
    {
        RaycastHit2D SpeedCollectibleHit = Physics2D.Raycast(transform.position, transform.position, 0.01f, m_LayerMask4);
        if (SpeedCollectibleHit.collider != null)
        {
            m_SpeedCollectible.gameObject.SetActive(false);
            m_SpeedCollectibleUI.gameObject.SetActive(true);
            m_Speed = 10;
            StartCoroutine(SpeedCollectibleDuration());
        }
    }

    public void PickBombCollectible()
    {
        RaycastHit2D BombCollectibleHit = Physics2D.Raycast(transform.position, transform.position, 0.01f, m_LayerMask4);
        if (BombCollectibleHit.collider != null)
        {
            m_BombCollectible.gameObject.SetActive(false);
            m_BombCollectibleUI.gameObject.SetActive(true);
            m_BombMaxQty = 3;
            StartCoroutine(BombCollectibleDuration());
        }
    }

    public IEnumerator HpDecreasingDuration()
    {
        yield return new WaitForSeconds(m_HurtIntervalTime);
        m_DamageIsDone = false;
    }

    public IEnumerator BombCollectibleDuration()
    {
        yield return new WaitForSeconds(m_PowerUpDuration);
        m_BombMaxQty = 2;
        m_BombCollectibleUI.gameObject.SetActive(false);
    }

    public IEnumerator SpeedCollectibleDuration()
    {
        yield return new WaitForSeconds(m_PowerUpDuration);
        m_Speed = 5;
        m_SpeedCollectibleUI.gameObject.SetActive(false);
    }
    //public void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, .25f);
    //    Gizmos.DrawLine(transform.position, transform.position + m_XvelocityLeft);
    //    Gizmos.DrawWireSphere(transform.position + m_XvelocityLeft, .25f);
    //}
}
