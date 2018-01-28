using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTransformation : MonoBehaviour {

    public Vector3 initialScale = Vector3.one;
    public Vector3 initialPosition = Vector3.zero;
    public Vector3 finalScale = Vector3.one;
    public Vector3 finalPosition = Vector3.zero;
    public float transitionTime = 1f;
    public float initial_delay = 0f;
    float timeSoFar = 0f;

	// Use this for initialization
	void Start () {
        transform.localScale = initialScale;
        transform.localPosition = initialPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (initial_delay > 0f)
        {
            initial_delay -= Time.deltaTime;
        } else
        {
            timeSoFar += Time.deltaTime;
            if (timeSoFar > transitionTime) timeSoFar = transitionTime;
            transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, timeSoFar / transitionTime);
            transform.localScale = Vector3.Lerp(initialScale, finalScale, timeSoFar / transitionTime);
        }
	}
}
