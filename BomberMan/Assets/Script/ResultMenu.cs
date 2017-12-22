using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultMenu : MonoBehaviour
{
    
    public Text m_ResultMenuText;

    public void Awake()
    {


        if (GameManager.m_PlayerHasWon == true)
        {
            m_ResultMenuText.text = "YOU WIN!";
        }
        else if (GameManager.m_PlayerHasWon == false)
        {
            m_ResultMenuText.text = "YOU LOSE!";
        }
    }

}
