using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D m_RigidBody;
    private float m_Speed = 0.1f;
    private Vector3 m_MoveDir = new Vector3();
    private bool outofboundleft;
    private bool outofboundright;
    public Vector2 PlayerInitPos = new Vector2();

    void Start () {
        PlayerInitPos = transform.position;
        m_RigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        Move();

        if (GameManager.m_gameOver) {
            ResetPosition();
        }

        if (GameManager.m_gameIsPaused) {
            m_Speed = 0;
        } else {
            m_Speed = 0.1f;
        }
    }

    public void Move() {
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
            transform.Translate(Vector3.right * m_Speed);
        } else if ((askMoveHorizontal) < 0 && (outofboundleft == false)) {
            transform.Translate(Vector3.right * -m_Speed);
        }
    }

    public void ResetPosition() {
        transform.position = PlayerInitPos;
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "ball") {
            AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_ballHitThePlayer);
        }
    }
}
