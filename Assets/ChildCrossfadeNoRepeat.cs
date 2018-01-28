﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NOTE: child objects must have the "body_image" tag to be faded.

public class ChildCrossfadeNoRepeat : MonoBehaviour {
    public float initial_delay = 0f;
    public float fade_speed = 1f;
    public Renderer[] child_rend;
    public float[] countdown;
    public int num_children;

	// Use this for initialization
	void Start () {
        num_children = 0;
        foreach (Renderer cr in GetComponentsInChildren<Renderer>())
        {
            if (cr.tag == "body_image") {
                num_children++;
            }
        }
        child_rend = new Renderer[num_children];
        countdown = new float[num_children];
        int tindex = 0;
        foreach (Renderer cr in GetComponentsInChildren<Renderer>())
        {
            if (cr.tag == "body_image")
            {
                child_rend[tindex] = cr;
                if (tindex == 0)
                {
                    // weird kludge so the first child doesn't pop in oddly
                    Color temp_color = cr.material.color;
                    temp_color.a = 1f;
                    cr.material.color = temp_color;
                }
                countdown[tindex] = initial_delay + 1.0f + (1.0f * tindex);
                tindex++;
            }
        }

    }

    // Update is called once per frame
    void Update () {
        Color temp_color;
        for (int i = 0; i < num_children; i++)
        {
            countdown[i] -= Time.deltaTime * fade_speed;
            if (countdown[i] < 0f) countdown[i] = 0f;
            temp_color = child_rend[i].material.color;
            if (countdown[i] < 1f && i < num_children - 1) // don't fade out last material
            {
                temp_color.a = countdown[i];
            } else if (countdown[i] < 2f)
            {
                temp_color.a = 1.0f - (countdown[i] - 1f);
            } else
            {
                temp_color.a = 0f;
            }
            child_rend[i].material.color = temp_color;
        }
	}
}
