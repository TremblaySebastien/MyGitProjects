using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int m_LevelToLoad;
    private int m_aReset;
    public AudioSource m_MusicAudioSource;
    public AudioSource m_SFXAudioSource;
    public Slider m_SFXSlider;
    public Slider m_MusicSlider;
    public GameObject m_Interaction;
    public GameObject m_RiddlerBubble;
    public GameObject m_BatmanBubble;
    public GameObject m_ButtonsSet1;
    public GameObject m_ButtonsSet2;
    public GameObject m_ButtonsSet3;
    public GameObject m_Explosion;
    public GameObject m_Riddler;
    public GameObject m_Evasion;
    public GameObject m_Asylum;

    private void Start()
    {
        m_aReset = -1;
    }
    
    public void BtnReinitializeDialogue_OnClick(int m_aReset)
    {
        if(m_aReset == 0)
        {
            ConversationManager.m_Step = 0;
            ConversationManager.m_DialogueActivated = false;
            ConversationManager.m_aAnswer = -1;
            ConversationManager.m_aAnswer2 = -1;
            ConversationManager.m_aAnswer3 = -1;

            m_Interaction.gameObject.SetActive(false);
            m_RiddlerBubble.gameObject.SetActive(false);
            m_BatmanBubble.gameObject.SetActive(true);
            m_ButtonsSet1.gameObject.SetActive(false);
            m_ButtonsSet2.gameObject.SetActive(false);
            m_ButtonsSet3.gameObject.SetActive(false);
            m_Explosion.gameObject.SetActive(false);
            m_Riddler.gameObject.SetActive(true);
            m_Evasion.gameObject.SetActive(false);
            m_Asylum.gameObject.SetActive(false);

            StopSound();

            StopAllCoroutines();
        }
    }

    public void BtnLoadLevel_OnClick(int aLevel)
    {
        m_LevelToLoad = aLevel;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(m_LevelToLoad);
    }

    public void SliderMusic_OnValueChange(float aValue)
    {
        m_MusicSlider.value = m_MusicAudioSource.volume;
        m_MusicAudioSource.volume = aValue;
    }

    public void SliderSFX_OnValueChange(float aValue)
    {
        m_SFXSlider.value = m_SFXAudioSource.volume;
        m_SFXAudioSource.volume = aValue;
    }

    public void StopSound()
    {
        m_SFXAudioSource.Stop();
    }
}
