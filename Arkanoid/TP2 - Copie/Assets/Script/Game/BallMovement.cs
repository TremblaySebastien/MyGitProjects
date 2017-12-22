using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public Rigidbody2D m_RigidBody;
    private float m_Speed = 0.1f;
    private Vector3 m_MoveDir = new Vector3(1, 0.5f);
    private Vector2 BallInitPos = new Vector2();
    private bool limitleft;
    private bool limitright;
    private bool limittop;
    private bool limitbottom;
    public bool ballHitPlayer = false;
    public bool m_initBall;
    public bool m_ballAfectedByPlayerMove;
    private float m_SpeedMove = 0.1f;
    private bool outofboundleft;
    private bool outofboundright;
    public GameObject m_ball;
    public Button m_replayGame;
    public GameObject m_widget;
    public GameObject m_winwidget;
    public GameObject m_losewidget;

    void Start() {
        BallInitPos = transform.position;
        m_RigidBody = GetComponent<Rigidbody2D>();
        ballHitPlayer = false;
        m_ballAfectedByPlayerMove = true;
        Button btn = m_replayGame.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        m_widget.SetActive(false);
        m_winwidget.SetActive(false);
        m_losewidget.SetActive(false);
        GameManager.m_gameIsPaused = false;
        GameManager.m_gameOver = false;
        GameManager.m_gameReallyOver = false;
        GrayBehavior.nCollision = 0;
    }

    void Update() {
        Move();

        if (Input.GetKey(KeyCode.Space)) {
            m_initBall = true;
            m_ballAfectedByPlayerMove = false;
        }

        if (m_initBall) {
            m_RigidBody.transform.Translate(m_MoveDir * m_Speed);
        }

        if (m_ballAfectedByPlayerMove) {
            MoveWithPlayer();
        }

        if (GameManager.m_gameOver) {
            ResetPosition();

            m_widget.SetActive(true);
        }

        if(GameManager.m_gameReallyOver) {

            if(GameManager.m_life == 0)
            {
                m_losewidget.SetActive(true);
            }
            else
            {
                m_winwidget.SetActive(true);
            }

            GameManager.m_gameReallyOver = false;
        }

        if (GameManager.m_gameIsPaused) {
            m_Speed = 0;
            m_SpeedMove = 0;
        } else {
            m_Speed = 0.1f;
            m_SpeedMove = 0.1f;
        }

        if(GrayBehavior.m_brickRemoved) {
            GameManager.m_score += 10;
            GrayBehavior.nCollision++;
            GrayBehavior.m_brickRemoved = false;
            AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_ballHitABrick);
        }
    }

    public void Move() {
        if (transform.position.x > 8.07f) {
            m_MoveDir.x = -m_MoveDir.x;
        }
        if (transform.position.x < -8.08f) {
            m_MoveDir.x = -m_MoveDir.x;
        }
        if (transform.position.y > 3.54) {
            m_MoveDir.y = -m_MoveDir.y;
        }
        if (transform.position.y < -5.17f) {
            AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_playerisDead);
            GameManager.m_life--;
            GameManager.m_gameOver = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "playerRight") {
            if (m_MoveDir.x > 0) {
                m_MoveDir.y = -m_MoveDir.y;
            }
            else if (m_MoveDir.x < 0) {
                m_MoveDir.y = -m_MoveDir.y;
                m_MoveDir.x = -m_MoveDir.x;
            }
        }
        else if (col.gameObject.tag == "playerLeft") {
            if (m_MoveDir.x > 0) {
                m_MoveDir.y = -m_MoveDir.y;
                m_MoveDir.x = -m_MoveDir.x;
            }
            else if (m_MoveDir.x < 0) {
                m_MoveDir.y = -m_MoveDir.y;
            }
        }
        else if (col.gameObject.tag == "playerMiddle") {
            m_MoveDir.y = -m_MoveDir.y;
        }
        else if (col.gameObject.tag == "grayBottom") {
            m_MoveDir.y = -m_MoveDir.y;
        } else if (col.gameObject.tag == "grayTop") {
            m_MoveDir.y = -m_MoveDir.y;
        } else if (col.gameObject.tag == "grayLeft") {
            m_MoveDir.x = -m_MoveDir.x;
        } else if (col.gameObject.tag == "grayright") {
            m_MoveDir.x = -m_MoveDir.x;
        }
    }

    public void MoveWithPlayer() {
        float askMoveHorizontal = Input.GetAxisRaw("Horizontal");

        if (transform.position.x > 7.0f) {
            outofboundright = true;
            outofboundleft = false;
        } else if (transform.position.x < -6.83f) {
            outofboundright = false;
            outofboundleft = true;
        } else {
            outofboundright = false;
            outofboundleft = false;
        }

        if ((askMoveHorizontal > 0) && (outofboundright == false)) {
            transform.Translate(Vector3.right * m_SpeedMove);
        } else if ((askMoveHorizontal) < 0 && (outofboundleft == false)) {
            transform.Translate(Vector3.right * -m_SpeedMove);
        }
    }

    public void ResetPosition() {
        m_ball.transform.position = BallInitPos;
        m_ballAfectedByPlayerMove = true;
        m_Speed = 0;
        m_MoveDir = new Vector3(1, 0.5f);
    }

    public void TaskOnClick() {
        m_widget.SetActive(false);
        GameManager.m_gameOver = false;
        m_initBall = false;
    }
}
