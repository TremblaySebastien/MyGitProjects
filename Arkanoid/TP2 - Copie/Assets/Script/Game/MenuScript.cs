using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Text PlayerName;
    public Text ErrorMsg;
    public Button m_PlayBtn;
    public static string m_playerName;
    public GameObject ErrorBox;
    private HttpHandler connexion;
    public bool m_connectionState = false;

    public void OnPlayPressed() {
        GameManager.m_life = 2;
        GameManager.m_gameOver = false;
        GameManager.m_gameReallyOver = false;
        GameManager.m_gameIsPaused = false;
        

        VerifyConnection();
    }

    private void Update() {
        if (m_connectionState) {
            SelectActualPlayerScore();
        }
    }

    public void VerifyConnection() {
        connexion = HttpHandler.Instance;
        WWWForm form = new WWWForm();
        form.AddField("username", ConnexionManager.m_userName);
        form.AddField("password", ConnexionManager.m_password);
        connexion.HttpRequest(this, form, "http://localhost/api/login.php", DebugVerifyConnection);
    }

    void DebugVerifyConnection(object result) {
        if ((string)result == " Password or username not valid." || (string)result == "Unautorize access") {
            ConnexionManager.m_connexionIsWrong = true;
            ErrorMsg.text = "Password or username not valid.";
            ErrorBox.SetActive(true);
            m_connectionState = false;
        } else {
            m_playerName = PlayerName.text;
            m_connectionState = true;
            AudioManager.Instance.m_AudioSource.Stop();
        }
    }

    public void SelectActualPlayerScore() {
        connexion = HttpHandler.Instance;
        WWWForm form = new WWWForm();
        form.AddField("usernameCurrentPlayer", ConnexionManager.m_userName);
        connexion.HttpRequest(this, form, "http://localhost/api/selectActualPlayer.php", DebugSelectActualPlayerScore);
    }

    void DebugSelectActualPlayerScore(object result3) {
        string highscore = result3.ToString();
        string[] words = highscore.Split('"');
        int j;

        if (System.Int32.TryParse(words[3], out j)) {
            GameManager.m_highestScore = j;
        }

        if (GameManager.m_score > GameManager.m_highestScore) {

            GameManager.m_highestScore = GameManager.m_score;
        }

        SceneManager.LoadScene("Game");
    }
}
