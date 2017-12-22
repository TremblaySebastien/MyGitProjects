using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public Transform PauseCanvas;
    public Transform SoundCanvas;

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
	}

    public void Pause()
    {
        if (PauseCanvas.gameObject.activeInHierarchy == false)
        {
            PauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        if (PauseCanvas.gameObject.activeInHierarchy == false)
        {
            PauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OpenOptionMenu()
    {
        
        SoundCanvas.gameObject.SetActive(true);
        PauseCanvas.gameObject.SetActive(false);

    }

    public void CloseOptionMenu()
    {

        SoundCanvas.gameObject.SetActive(false);
        PauseCanvas.gameObject.SetActive(true);

    }
}
