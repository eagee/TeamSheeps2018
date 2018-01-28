using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutAndIn : MonoBehaviour {

    public float fade_out_time = 0f;
    public float fade_out_duration = 1f;
    public float fade_in_time = 2f;
    public float fade_in_duration = 1f;
    public string nextScene;
    float intended_alpha;
    Renderer rend;
    
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > fade_in_time + fade_in_duration)
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
        Color temp_color;
        temp_color = rend.material.color;
		if (Time.time < fade_out_time || Time.time > fade_in_time + fade_in_duration)
        {
            intended_alpha = 1f;
        } else if (fade_out_time < Time.time && Time.time < fade_out_time + fade_out_duration)
        {
            intended_alpha = Mathf.Lerp(1f, 0f, (Time.time - fade_out_time) / fade_out_duration);
        }
        else if (fade_in_time < Time.time && Time.time < fade_in_time + fade_in_duration)
        {
            intended_alpha = Mathf.Lerp(0f, 1f, (Time.time - fade_in_time) / fade_in_duration);
        } else
        {
            intended_alpha = 0f;
        }
        temp_color.a = intended_alpha;
        rend.material.color = temp_color;
    }
}
