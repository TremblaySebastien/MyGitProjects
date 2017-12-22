using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource m_AudioSource;

	private void Awake ()
    {
        m_AudioSource.Play();
        //DontDestroyOnLoad(m_AudioSource);
    }
}
