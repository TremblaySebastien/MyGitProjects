using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiddlerDialogue : MonoBehaviour
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
    public string Text10;
    public string Text11;
    public string Text12;
    public float delay = 0.1f;

    public void Start()
    {
        Text1 = "Oh! There you are Cape Crusader!!!";
        Text2 = "You have to answer to my little puzzle to save your city!";
        Text3 = "what's blue, round and delicious?";
        Text4 = "you're stupid Batman, let's try this one";
        Text5 = "I cannot fly, i am the night, who am i?";
        Text6 = "Not bad batman ... still i win! So long old chum!";
        Text7 = "That was an easy one, prepare for worst!";
        Text8 = "i'm full of question ... who am i?";
        Text9 = "Good one but it's not enought so ... i'm gone!";
        Text10 = "You didn't succeed both of them so i'm free to go";
        Text11 = "Impossible, you got Me Batman!";
        Text12 = "You're totally Wrong! you're city will burn!!! hahahaha";
    }

    public IEnumerator RiddlerShowText(int m_Step)
    {
        m_RiddlerAction.m_RiddlerAnimation.SetBool("IsChatting", true);
        m_Move.m_Animation.SetBool("IsTalking", false);
        ConversationManager.m_DialogueActivated = false;
        if (m_Step == 3)
        { 
            m_StringText = Text1;
        }
        else if (m_Step == 4)
        {
            m_StringText = Text2;
        }
        else if (m_Step == 30)
        {
            m_StringText = Text3;
        }
        else if (m_Step == 7)
        {
            m_StringText = Text4;
        }
        else if (m_Step == 8)
        {
            m_StringText = Text5;
        }
        else if (m_Step == 11)
        {
            m_StringText = Text6;
        }
        else if (m_Step == 14)
        {
            m_StringText = Text7;
        }
        else if (m_Step == 15)
        {
            m_StringText = Text8;
        }
        else if (m_Step == 16)
        {
            m_StringText = Text9;
        }
        else if (m_Step == 18)
        {
            m_StringText = Text10;
        }
        else if(m_Step == 22)
        {
            m_StringText = Text11;
        }
        else if (m_Step == 26)
        {
            m_StringText = Text12;
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
        StopCoroutine(RiddlerShowText(m_Step));
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
