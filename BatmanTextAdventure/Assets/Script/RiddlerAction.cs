using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiddlerAction : MonoBehaviour
{
    public Animator m_RiddlerAnimation;
    public string Text1 = "Oh! There you are Cape Crusader!";
    public float delay = 0.1f;
    private string currentText = "";

    void Start()
    {
        m_RiddlerAnimation = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D aCol)
    {
        if (aCol.gameObject.tag == "Batman")
        {
            m_RiddlerAnimation.SetBool("IsChatting", true);
        }
    }
}
