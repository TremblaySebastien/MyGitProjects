using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int m_score = 0;
    public static int m_life = 2;
    public static int m_highestScore;

    public static bool m_gameOver = false;
    public static bool m_gameReallyOver = false;
    public static bool m_gameIsPaused = false;

    public Text m_scoreText;
    public Text m_highscoreText;
    public Text m_lifeText;
    public Text m_CurrentPlayer;
    public Text m_Player1Name;
    public Text m_Player1Score;
    public Text m_player1Name;
    public Text m_player1Score;
    public Text m_player2Name;
    public Text m_player2Score;
    public Text m_player3Name;
    public Text m_player3Score;
    public Text m_player4Name;
    public Text m_player4Score;
    public Text m_player5Name;
    public Text m_player5Score;
    public Text m_player6Name;
    public Text m_player6Score;
    public Text m_player7Name;
    public Text m_player7Score;
    public Text m_player8Name;
    public Text m_player8Score;
    public Text m_player9Name;
    public Text m_player9Score;
    public Text m_player10Name;
    public Text m_player10Score;

    public Button m_leaderboardBtn;
    public Button m_exitleaderboardBtn;
    public Button m_leaderBoardBtn;

    public GameObject m_Leaderboard;

    private HttpHandler connexion;

    public int currentScore;

    

    void Start() {
        m_CurrentPlayer.text = MenuScript.m_playerName;
        m_leaderboardBtn.onClick.AddListener(SpawnLeaderboard);
        m_exitleaderboardBtn.onClick.AddListener(DeSpawnLeaderboard);
        m_Leaderboard.SetActive(false);
        m_score = 0;
        m_life = 2;
    }

    void Update() {
        m_scoreText.text = m_score.ToString();
        m_lifeText.text = m_life.ToString();
        m_highscoreText.text = m_highestScore.ToString();

        if (m_life <= 0) {
            m_gameReallyOver = true;
        }

        if (m_score == 600) {
            m_gameReallyOver = true;
            m_gameIsPaused = true;
        }
    }

    public void SpawnLeaderboard() {
        m_gameIsPaused = true;
        AudioManager.Instance.m_AudioSource.Play();
        SelectActualPlayerScore();
        UpdatePlayers();
        SpawnPlayers();
    }

    public void DeSpawnLeaderboard() {
        m_Leaderboard.SetActive(false);
        m_gameIsPaused = false;
        AudioManager.Instance.m_AudioSource.Stop();
    }

    public void SpawnPlayers() {
        connexion = HttpHandler.Instance;
        WWWForm form = new WWWForm();
        connexion.HttpRequest(this, form, "http://localhost/api/leaderboard.php?count=10", DebugSpawnPlayer);
    }

    public void UpdatePlayers() {
        connexion = HttpHandler.Instance;
        WWWForm form = new WWWForm();
        form.AddField("highscoreCurrentPlayer", m_highestScore);
        form.AddField("usernameCurrentPlayer", ConnexionManager.m_userName);
        connexion.HttpRequest(this, form, "http://localhost/api/update_leaderboard.php", DebugUpdatePlayer);
    }

    public void SelectActualPlayerScore() {
        connexion = HttpHandler.Instance;
        WWWForm form = new WWWForm();
        form.AddField("usernameCurrentPlayer", ConnexionManager.m_userName);
        connexion.HttpRequest(this, form, "http://localhost/api/selectActualPlayer.php", DebugSelectActualPlayerScore);
    }

    public void DebugSpawnPlayer(object result) {
        string score = result.ToString();
        string[] words = score.Split('"');

        m_player1Name.text = words[3];
        m_player1Score.text = words[7];
        m_player2Name.text = words[11];
        m_player2Score.text = words[15];
        m_player3Name.text = words[19];
        m_player3Score.text = words[23];
        m_player4Name.text = words[27];
        m_player4Score.text = words[31];
        m_player5Name.text = words[35];
        m_player5Score.text = words[39];
        m_player6Name.text = words[43];
        m_player6Score.text = words[47];
        m_player7Name.text = words[51];
        m_player7Score.text = words[55];
        m_player8Name.text = words[59];
        m_player8Score.text = words[63];
        m_player9Name.text = words[67];
        m_player9Score.text = words[71];
        m_player10Name.text = words[75];
        m_player10Score.text = words[79];

        m_Leaderboard.SetActive(true);
    }

    public void DebugUpdatePlayer(object result2) {
    }

    public void DebugSelectActualPlayerScore(object result3) {
        string highscore = result3.ToString();
        string[] words = highscore.Split('"');
        int j;

        if (System.Int32.TryParse(words[3], out j)) {
            m_highestScore = j;
        }

        if (m_score > m_highestScore) {
            m_highestScore = m_score;
        }
    }
}
