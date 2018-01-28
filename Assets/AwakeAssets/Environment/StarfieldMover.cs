using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldMover : MonoBehaviour {
    public Transform StartingPoint;
    public Transform EndingPoint;
    public float Speed = 1.0f;
    public int ScoreThreshold = 50;
    private DanceManager m_DanceManager;

	// Use this for initialization
	void Awake() {
        m_DanceManager = FindObjectOfType<DanceManager>();
        //this.transform.position = StartingPoint.position;
    }
	
	// Update is called once per frame
	void Update () {
        float currentScore = m_DanceManager.GetCurrentScore();
        if(currentScore >= ScoreThreshold) // time to start scrolling down - what fun!
        {
            Vector3 currentPostion = this.transform.position;
            if(currentPostion.y < EndingPoint.position.y)
            {
                currentPostion.y += Speed * Time.deltaTime;
            }
            this.transform.position = currentPostion;
        }
	}
}
