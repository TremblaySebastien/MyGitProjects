using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text m_dialogueText;
    public GameObject m_dialoguebox;

    void Start()
    {
        m_dialoguebox.SetActive(false);
        m_dialogueText.text = "";
    }

    private void Update()
    {
        if (!OldMan.m_oldManSpoken)
        {
            m_dialogueText.text = "You must speak to the old man first if you want to continue your journey!";
        }
        if (OldMan.m_oldManSpoken && !Zelda.m_zeldaSpoken && !OldWoman.m_oldWomanSpoken)
        {
            m_dialoguebox.SetActive(true);
            m_dialogueText.text = "You must now speak to the old woman if you want to continue your journey!";
        }
        else if (OldMan.m_oldManSpoken && !Zelda.m_zeldaSpoken && OldWoman.m_oldWomanSpoken)
        {
            m_dialogueText.text = "You must now speak to princess Zelda to make appear the incarnate evil!";
        }
        else if (OldMan.m_oldManSpoken && Zelda.m_zeldaSpoken && OldWoman.m_oldWomanSpoken && !CharacterMovement.m_enemyDefeated)
        {
            m_dialogueText.text = "The enemy is there, make sure to grab a bomb and put it below him to get rid of him ... Who knows, maybe he will show you where is the triforce!!!";
        }
        else if (OldMan.m_oldManSpoken && Zelda.m_zeldaSpoken && OldWoman.m_oldWomanSpoken && CharacterMovement.m_enemyDefeated && !Triforce.m_triforceTaken)
        {
            m_dialogueText.text = "You kill the enemy!!! fortunately for you, he showed you the way to the Triforce!! now, go take it to feel its power!";
        }
        else if (OldMan.m_oldManSpoken && Zelda.m_zeldaSpoken && OldWoman.m_oldWomanSpoken && CharacterMovement.m_enemyDefeated && Triforce.m_triforceTaken)
        {
            m_dialogueText.text = "Congratulation! You defeated the evil and you took back the great power!";
        }
    }
}
