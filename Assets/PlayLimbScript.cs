using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLimbScript : MonoBehaviour
{

    public AudioClip ping;
    // Use this for initialization
    void Start()
    {

    }

    public void PlayOnce()
    {
        GetComponent<AudioSource>().PlayOneShot(ping, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
