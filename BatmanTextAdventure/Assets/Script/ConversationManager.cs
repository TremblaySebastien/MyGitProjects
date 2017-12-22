using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    public BatmanDialogue m_BatmanDialogue;
    public RiddlerDialogue m_RiddlerDialogue;
    public Move m_Move;
    public RiddlerAction m_RiddlerAction;
    public GameObject m_RiddlerBubble;
    public GameObject m_BatmanBubble;
    public GameObject m_Explosion;
    public GameObject m_Riddler;
    public GameObject m_Evasion;
    public GameObject m_Asylum;
    public GameObject m_ButtonsSet1;
    public GameObject m_ButtonsSet2;
    public GameObject m_ButtonsSet3;
    public Text m_Answer_1;
    public Text m_Answer_2;
    public Text m_Answer_3;
    public Text m_Answer_4;
    public Text m_Answer_5;
    public Text m_Answer_6;
    public static int m_aAnswer;
    public static int m_aAnswer2;
    public static int m_aAnswer3;
    public static int m_Step = 0;
    public static bool m_DialogueActivated = false;

    private void Start()
    {
        m_aAnswer = -1;
        m_aAnswer2 = -1;
        m_aAnswer3 = -1;
    }

    private void Update()
    {
        if (m_DialogueActivated == true)
        { 
            if(m_Step == 1)
            {
                m_RiddlerBubble.gameObject.SetActive(false);
                StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                m_Step = 2;
            }
            else if (m_Step == 2)
            {
                m_RiddlerBubble.gameObject.SetActive(true);
                m_BatmanBubble.gameObject.SetActive(false);
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 3;
            }
            else if (m_Step == 3)
            {
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 4;
            }
            else if (m_Step == 4)
            {
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 30;
            }
            else if(m_Step == 30)
            {
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 5;
            }
            else if (m_Step == 5)
            {
                m_Answer_1.text = "Earth";
                m_Answer_2.text = "Blueberries";
                m_ButtonsSet1.gameObject.SetActive(true);
                
                if (m_aAnswer == 0)
                {
                    m_Step = 6;
                    if(m_Step == 6)
                    {
                        m_ButtonsSet1.gameObject.SetActive(false);
                        m_RiddlerBubble.gameObject.SetActive(false);
                        m_BatmanBubble.gameObject.SetActive(true);
                        StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                        m_Step = 7;
                    }
                }
                else if (m_aAnswer == 1)
                {
                    m_Step = 13;
                    if (m_Step == 13)
                    {
                        m_ButtonsSet1.gameObject.SetActive(false);
                        m_RiddlerBubble.gameObject.SetActive(false);
                        m_BatmanBubble.gameObject.SetActive(true);
                        StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                        m_Step = 14;
                    }
                }
            }
            else if(m_Step == 7)
            {
                m_ButtonsSet1.gameObject.SetActive(false);
                m_RiddlerBubble.gameObject.SetActive(true);
                m_BatmanBubble.gameObject.SetActive(false);
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 8;
            }
            else if(m_Step == 8)
            {
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 9;
            }
            else if (m_Step == 9)
            {
                m_Answer_3.text = "Me";
                m_Answer_4.text = "You";
                m_ButtonsSet2.gameObject.SetActive(true);

                if (m_aAnswer2 == 0)
                {
                    m_Step = 10;
                    if (m_Step == 10)
                    {
                        m_ButtonsSet2.gameObject.SetActive(false);
                        m_RiddlerBubble.gameObject.SetActive(false);
                        m_BatmanBubble.gameObject.SetActive(true);
                        StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                        m_Step = 11;
                    }
                }
                else if (m_aAnswer2 == 1)
                {
                    m_Step = 25;
                    if (m_Step == 25)
                    {
                        m_ButtonsSet2.gameObject.SetActive(false);
                        m_RiddlerBubble.gameObject.SetActive(false);
                        m_BatmanBubble.gameObject.SetActive(true);
                        StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                        m_Step = 26;
                    }
                }
            }
            else if(m_Step == 11)
            {
                m_RiddlerBubble.gameObject.SetActive(true);
                m_BatmanBubble.gameObject.SetActive(false);
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 12;
            }
            else if(m_Step == 12)
            {
                m_RiddlerBubble.gameObject.SetActive(false);
                m_Riddler.gameObject.SetActive(false);
                m_Evasion.gameObject.SetActive(true);
            }
            else if(m_Step == 14)
            {
                m_RiddlerBubble.gameObject.SetActive(true);
                m_BatmanBubble.gameObject.SetActive(false);
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 15;
            }
            else if (m_Step == 15)
            {
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 16;
            }
            else if (m_Step == 16)
            {
                m_Answer_5.text = "The Batman";
                m_Answer_6.text = "The Riddler";
                m_ButtonsSet3.gameObject.SetActive(true);

                if (m_aAnswer3 == 0)
                {
                    m_Step = 17;
                    if (m_Step == 17)
                    { 
                        m_ButtonsSet3.gameObject.SetActive(false);
                        m_RiddlerBubble.gameObject.SetActive(false);
                        m_BatmanBubble.gameObject.SetActive(true);
                        StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                        m_Step = 18;
                    }
                }
                else if (m_aAnswer3 == 1)
                {
                    m_Step = 21;
                    if (m_Step == 21)
                    {
                        m_ButtonsSet3.gameObject.SetActive(false);
                        m_RiddlerBubble.gameObject.SetActive(false);
                        m_BatmanBubble.gameObject.SetActive(true);
                        StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                        m_Step = 22;
                    }
                }
            }
            else if (m_Step == 18)
            {
                m_RiddlerBubble.gameObject.SetActive(true);
                m_BatmanBubble.gameObject.SetActive(false);
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 19;
            }
            else if(m_Step == 19)
            {
                m_RiddlerBubble.gameObject.SetActive(false);
                m_BatmanBubble.gameObject.SetActive(true);
                StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                m_Step = 20;
            }
            else if (m_Step == 20)
            {
                m_BatmanBubble.gameObject.SetActive(false);
                m_Evasion.gameObject.SetActive(true);
                m_Riddler.gameObject.SetActive(false);
            }
            else if (m_Step == 22)
            {
                m_RiddlerBubble.gameObject.SetActive(true);
                m_BatmanBubble.gameObject.SetActive(false);
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 23;
            }
            else if (m_Step == 23)
            {
                m_RiddlerBubble.gameObject.SetActive(false);
                m_BatmanBubble.gameObject.SetActive(true);
                StartCoroutine(m_BatmanDialogue.BatmanShowText(m_Step));
                m_Step = 24;
            }
            else if (m_Step == 24)
            {
                m_BatmanBubble.gameObject.SetActive(false);
                m_Asylum.gameObject.SetActive(true);
            }
            else if (m_Step == 26)
            {
                m_RiddlerBubble.gameObject.SetActive(true);
                m_BatmanBubble.gameObject.SetActive(false);
                StartCoroutine(m_RiddlerDialogue.RiddlerShowText(m_Step));
                m_Step = 27;
            }
            else if (m_Step == 27)
            {
                m_RiddlerBubble.gameObject.SetActive(false);
                m_Explosion.gameObject.SetActive(true);
            }
        }
    }

    public void Btn_LoadAnswer(int aAnswer)
    {
        m_aAnswer = aAnswer;
    }

    public void Btn_LoadAnswer2(int aAnswer2)
    {
        m_aAnswer2 = aAnswer2;
    }

    public void Btn_LoadAnswer3(int aAnswer3)
    {
        m_aAnswer3 = aAnswer3;
    }
}


