using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDescent : MonoBehaviour {

    public Vector3 initialPosition;
    public Vector3 winningPosition;
    int score_was = 0;
    DanceManager myDanceManager;

	// Use this for initialization
	void Start () {
        transform.position = initialPosition;
	}
	
	// Update is called once per frame
	void Update () {
	    if (!myDanceManager)
        {
            myDanceManager = FindObjectOfType<DanceManager>();
        } else
        {
            int curr_score = myDanceManager.GetCurrentScore();
            if (curr_score != score_was) {
                score_was = curr_score;
                transform.position = Vector3.Lerp(initialPosition, winningPosition, 
                    (((float) curr_score) / myDanceManager.winningScore));
            }
        }   	
	}
}
