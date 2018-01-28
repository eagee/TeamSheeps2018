using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class RandomEmitterMover : MonoBehaviour
{
    public Transform transformFrom;
    public Transform transformTo;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transformTo && transformFrom)
        { 
            float lerpAmount = Random.Range(0.0f, 1.0f);
            this.transform.position = Vector3.Lerp(transformFrom.position, transformTo.position, lerpAmount);
        }
    }
}
