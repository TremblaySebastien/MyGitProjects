using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatmanDialogue : MonoBehaviour
{
    public Move m_Move;
    public RiddlerAction m_RiddlerAction;
    public AudioSource m_AudioSource;
    public ConversationManager m_ConversationManager;
    public string m_StringText;
    public string Text1;
    public string Text2;
    public string Text3;
    public string Text4;
    public string Text5;
    public string Text6;
    public string Text7;
    public string Text8;
    public string Text9;
    public float delay = 0.1f;

    public void Start()
    {
        Text1 = "What are you up to Riddler ?!";
        Text2 = "hum ... the earth?";
        Text3 = "Obviously, blueberries!";
        Text4 = "hum ... Me?";
        Text5 = "it's Me";
        Text6 = "it's you?";
        Text7 = "you're done Nygma, let's get back to Arkham!";
        Text8 = "Next time i'm gonna get you Riddler";
        Text9 = "Hum ... You?";
    }

    public IEnumerator BatmanShowText(int m_Step)
    {
        m_RiddlerAction.m_RiddlerAnimation.SetBool("IsChatting", false);
        m_Move.m_Animation.SetBool("IsTalking", true);
        ConversationManager.m_DialogueActivated = false;
        if (m_Step == 1)
        {
            m_StringText = Text1;
        }
        else if (m_Step == 6)
        {
            m_StringText = Text2;
        }
        else if (m_Step == 10)
        {
            m_StringText = Text4;
        }
        else if (m_Step == 13)
        {
            m_StringText = Text3;
        }
        else if (m_Step == 17)
        {
            m_StringText = Text5;
        }
        else if (m_Step == 19)
        {
            m_StringText = Text8;
        }
        else if (m_Step == 21)
        {
            m_StringText = Text6;
        }
        else if (m_Step == 23)
        {
            m_StringText = Text7;
        }
        else if (m_Step == 25)
        {
            m_StringText = Text9;
        }

        for (int i = 0; i < m_StringText.Length; i++)
        {
            this.GetComponent<Text>().text = m_StringText.Substring(0, i);
            yield return new WaitForSeconds(delay);
            PlaySound();
        }
        this.GetComponent<Text>().text = m_StringText;
        StopSound();
        ConversationManager.m_DialogueActivated = true;
        StopCoroutine(BatmanShowText(m_Step));
    }

    public void PlaySound()
    {
        m_AudioSource.Play();
    }

    public void StopSound()
    {
        m_AudioSource.Stop();
    }
}
