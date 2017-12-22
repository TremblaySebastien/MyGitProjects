using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip m_Music1;
    public AudioClip m_Music2;

    public AudioSource m_AudioSource;
    public AudioSource m_AudioSourceMainMenu;
    public AudioSource m_AudioSourceGamePlay;

    public GameObject m_AudioManager;

    private static AudioManager m_Instance;
    public static AudioManager Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        m_AudioSource.Play();

        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            //Debug.LogError("Too many AudioManagers!!!");

            Destroy(this.gameObject);
            //m_AudioManager.gameObject.SetActive(false);
        }

        DontDestroyOnLoad(m_AudioManager);
    }


    public void ChangeMusicBetweenScene(AudioClip music)
    {
        m_AudioSource.Stop();
        //m_AudioSourceMainMenu.clip = music;
        m_AudioSource.clip = music;
        //source.Stop();
        //m_AudioSourceGamePlay.gameObject.SetActive(true);
        //m_AudioSourceMainMenu.gameObject.SetActive(false);
        //source.Play();
        m_AudioSource.Play();

    }

   

}
