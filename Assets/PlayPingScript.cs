using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPingScript : MonoBehaviour {

    public AudioClip ping;
	// Use this for initialization
	void Start () {
		
	}

    public void PlayOnce()
    {
        GetComponent<AudioSource>().PlayOneShot(ping, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
