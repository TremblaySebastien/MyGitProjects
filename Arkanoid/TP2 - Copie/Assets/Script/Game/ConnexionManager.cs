using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnexionManager : MonoBehaviour {

    public GameObject ErrorBox;

    public Button Retry;
    public Text UserName;
    public static string m_userName;
    public Text Password;
    public static string m_password;
    public Text ErrorMessage;
    public static bool m_connexionIsWrong;

    public void Start() {
        UserName.text = "";
        Password.text = "";
    }

    public void Update() {
        m_userName = UserName.text;
        m_password = Password.text;

        if (m_connexionIsWrong) {

            ErrorBox.SetActive(true);

            if (UserName.text == "" && Password.text == "") {
                ErrorMessage.text = "empty username & empty password";
            } else if (UserName.text == "") {
                ErrorMessage.text = "empty username";
            } else if (Password.text == "") {
                ErrorMessage.text = "empty Password";
            }
        } else {
            ErrorBox.SetActive(false);
        }
    }

    public void RetryButton() {
        m_connexionIsWrong = false;
        ErrorBox.SetActive(false);
    }
}
