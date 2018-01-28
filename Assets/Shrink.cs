using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour {

    public Vector3 initialScale = Vector3.one;
    public Vector3 finalScale = Vector3.one;
    public float scalingTime = 1f;
    public float initial_delay = 0f;
    float timeSoFar = 0f;

	// Use this for initialization
	void Start () {
        transform.localScale = initialScale;
	}
	
	// Update is called once per frame
	void Update () {
        initial_delay -= Time.deltaTime;
        if (initial_delay < 0f)
        {
            timeSoFar += Time.deltaTime;
            if (timeSoFar > scalingTime) timeSoFar = scalingTime;
            transform.localScale = Vector3.Lerp(initialScale, finalScale, timeSoFar / scalingTime);
        }
	}
}
