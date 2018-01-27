using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCycle : MonoBehaviour {

    public Material[] cycle;
    public Renderer my_renderer;
    public float time_left;
    public float cycle_time = 1.0f;
    public int next_material;

	// Use this for initialization
	void Start () {
        time_left = 0f;
        my_renderer = GetComponent<Renderer>();
        my_renderer.enabled = true;
        next_material = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time_left -= Time.deltaTime;
        if (time_left < 0f)
        {
            time_left = cycle_time;
            my_renderer.sharedMaterial = cycle[next_material];
            next_material++;
            if (next_material >= cycle.Length) next_material = 0;
        }
	}
}
