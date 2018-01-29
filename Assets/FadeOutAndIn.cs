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
    float timeSoFar = 0f;
    
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        // kludge
        Color temp_color;
        temp_color = rend.material.color;
        temp_color.a = 1f;
        rend.material.color = temp_color;
        timeSoFar = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        timeSoFar += Time.deltaTime;
        if (timeSoFar > fade_in_time + fade_in_duration)
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
        Color temp_color;
        temp_color = rend.material.color;
		if (timeSoFar < fade_out_time || timeSoFar > fade_in_time + fade_in_duration)
        {
            intended_alpha = 1f;
        } else if (fade_out_time < timeSoFar && timeSoFar < fade_out_time + fade_out_duration)
        {
            intended_alpha = Mathf.Lerp(1f, 0f, (timeSoFar - fade_out_time) / fade_out_duration);
        }
        else if (fade_in_time < timeSoFar && timeSoFar < fade_in_time + fade_in_duration)
        {
            intended_alpha = Mathf.Lerp(0f, 1f, (timeSoFar - fade_in_time) / fade_in_duration);
        } else
        {
            intended_alpha = 0f;
        }
        temp_color.a = intended_alpha;
        rend.material.color = temp_color;
    }
}
