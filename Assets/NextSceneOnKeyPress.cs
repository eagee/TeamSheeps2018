using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneOnKeyPress : MonoBehaviour {

    public string nextScene;
    public float fadeOutTime = 1f;
    float fadeOutTimeLeft;
    bool fadingOut = false;
    Renderer rend;
    Color temp_color;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        fadeOutTimeLeft = fadeOutTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            fadingOut = true;
        }
        if (fadingOut)
        {
            fadeOutTimeLeft -= Time.deltaTime;
            if (fadeOutTimeLeft < 0f) SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            temp_color = rend.material.color;
            temp_color.r = fadeOutTimeLeft / fadeOutTime;
            temp_color.g = fadeOutTimeLeft / fadeOutTime;
            temp_color.b = fadeOutTimeLeft / fadeOutTime;
            rend.material.color = temp_color;
        }
    }
}
