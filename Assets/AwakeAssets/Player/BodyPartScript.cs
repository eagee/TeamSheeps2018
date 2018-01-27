using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BodyPartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().simulationSpace = ParticleSystemSimulationSpace.World;
	}
}
