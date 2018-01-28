using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_zig : MonoBehaviour {

    public Vector3 drift_vector = Vector3.forward;
    public Vector3 spin_vector = Vector3.zero;
    public int ScoreThreshold = 162;
    Vector3 original_position;
    private DanceManager m_DanceManager;

    // Use this for initialization
    void Start () {
        original_position = transform.position;	
	}

    // Use this for initialization
    void Awake()
    {
        m_DanceManager = FindObjectOfType<DanceManager>();
    }

    // Update is called once per frame
    void Update () {
        float currentScore = m_DanceManager.GetCurrentScore();
        if(currentScore < ScoreThreshold)
        { 
            transform.position = original_position;
            transform.Translate(drift_vector * Mathf.Sin(Time.time));
            transform.Rotate(spin_vector * Mathf.Cos(Time.time));
        }
    }
}
