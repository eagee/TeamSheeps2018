using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour {

    private DanceManager m_DanceManager;

    void Awake()
    {
        m_DanceManager = FindObjectOfType<DanceManager>();
    }


    void Update()
    {
        int score = m_DanceManager.GetCurrentScore();
        string scoreText = score.ToString();
        GetComponent<TextMesh>().text = scoreText;
    }

}
