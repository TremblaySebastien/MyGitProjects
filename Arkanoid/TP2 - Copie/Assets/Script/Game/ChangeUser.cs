using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeUser : MonoBehaviour {
    public Button m_changeUserBtn;
    public Button m_ok;
    public Button m_cancel;
    public Button m_Quit;
    public GameObject m_ConfirmationWidget;
    private HttpHandler connexion;

    public void SpawnConfirmation() {
        GameManager.m_gameIsPaused = true;
        m_ConfirmationWidget.SetActive(true);
    }

    public void ReturnToMainMenu() {
        m_ConfirmationWidget.SetActive(false);
        GameManager.m_score = 0;
        AudioManager.Instance.m_AudioSource.Play();
        SceneManager.LoadScene("Menu");
    }

    public void ReturnToGame() {
        m_ConfirmationWidget.SetActive(false);
        GameManager.m_gameIsPaused = false;
    }

    public void QuitGame() {
        UpdatePlayers();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void UpdatePlayers() {
        connexion = HttpHandler.Instance;
        WWWForm form = new WWWForm();
        form.AddField("highscoreCurrentPlayer", GameManager.m_score);
        form.AddField("usernameCurrentPlayer", ConnexionManager.m_userName);
        connexion.HttpRequest(this, form, "http://localhost/api/update_leaderboard.php", DebugUpdatePlayers);
    }

    void DebugUpdatePlayers(object result) {
    }
}
