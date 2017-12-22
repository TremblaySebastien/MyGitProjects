using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip m_ballHitThePlayer;
    public AudioClip m_ballHitABrick;
    public AudioClip m_playerisDead;
    public AudioClip m_Menumusic;

    public AudioSource m_AudioSource;
    public GameObject m_AudioManager;

    private static AudioManager m_Instance;
    public static AudioManager Instance {
        get { return m_Instance; }
    }

    private void Awake() {
        m_AudioSource.Play();

        if (m_Instance == null) {
            m_Instance = this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(m_AudioManager);
    }
}
