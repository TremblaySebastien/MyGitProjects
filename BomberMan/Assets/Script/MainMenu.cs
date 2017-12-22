using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip m_AudioClip1;
    public AudioClip m_AudioClip2;

    public void BtnLoadLevel_OnClick(int aLevel)
    {
        LoadLevel();
        if(aLevel == 0)
        {
            Time.timeScale = 1;
            AudioManager.Instance.ChangeMusicBetweenScene(m_AudioClip2);
        }
        else if(aLevel == 1)
        {
            Time.timeScale = 1;
            AudioManager.Instance.ChangeMusicBetweenScene(m_AudioClip1);
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("GamePlay");  
    }

    public void BtnLoadLevel_OnClick2(int aLevel)
    {
        if (aLevel == 0)
        {
            AudioManager.Instance.ChangeMusicBetweenScene(m_AudioClip1);
        }
        else if (aLevel == 1)
        {
            //AudioManager.Instance.ChangeMusicBetweenScene(m_AudioClip2);
            LoadLevel();
        }
    }
}
