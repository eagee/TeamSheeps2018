using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPrefabScript : MonoBehaviour {

    public StarfieldMover offsetObject;
    public float HeartOffset = 4f;

    // Use this for initialization
    void Start () {
        offsetObject = FindObjectOfType<StarfieldMover>();

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = new Vector3();
        targetPosition.y += offsetObject.transform.position.y;
        targetPosition.y += HeartOffset;
        this.transform.position = Vector3.Slerp(this.transform.position, targetPosition, Time.deltaTime);
	}
}
