using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
 //   public Text m_counterText;

 //   public float m_Seconds;
 //   public float m_Minutes;

 //   void Start()
 //   {
 //       m_counterText = GetComponent<Text>() as Text;
	//}
	
	
	//void Update ()
 //   {
 //       m_Minutes = (int)(Time.timeSinceLevelLoad  / 60f);
 //       m_Seconds = (int)(Time.timeSinceLevelLoad % 60f);
 //       m_counterText.text = m_Minutes.ToString("03") + ":" + m_Seconds.ToString("00");
	//}

    public float timeLeft = 500.0f;
    public bool m_TimeIsUp = false;

    public Text m_counterText;

    

    void Update()
    {
        timeLeft -= Time.deltaTime;
        m_counterText.text = "Time Left : " + (int)timeLeft;
        if (timeLeft < 0)
        {
            m_TimeIsUp = true;
            
        }
        else
        {
            m_TimeIsUp = false;
        }
        
    }

}
