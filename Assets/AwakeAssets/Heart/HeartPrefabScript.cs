using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPrefabScript : MonoBehaviour {

    public StarfieldMover offsetObject;
    public float HeartOffset = 4f;
    public Vector3 wander1;
    public Vector3 wander2;
    public float wanderSpeed;
    public float rotateSpeed;

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

        Vector3 w1 = this.transform.position + (Mathf.Sin(Time.time * wanderSpeed) * wander1);
        Vector3 w2 = this.transform.position + (Mathf.Cos(Time.time * wanderSpeed) * wander2);
        Vector3 targetPostion2 = Vector3.Lerp(w1, w2, 0.5f);
        //this.transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        targetPostion2.z = 0f;
        this.transform.position = targetPostion2;


    }
}
