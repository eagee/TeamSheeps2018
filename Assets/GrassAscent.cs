using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAscent : MonoBehaviour {

    public Vector3 initialPosition;
    public Vector3 finalPosition;
    public float timeToRise = 1f;
    public float timeToDuckOut = 1f;
    public float timeForTextFadeIn = 1f;
    private float timeSoFar = 0f;
    public bool UseTardyFromDanceManager = true;
    public bool tardy = false;
    private int state = 0;
    private DanceManager m_DanceManager;

    // 0 - Haven't been marked tardy yet
    // 1 - tardy, grass is rising
    // 2 - grass risen, fading in text 1
    // 3 - text 1 in, fading in text 2
    // 4 - text 2 in
    // 5 - ducking grass out
    public Renderer[] child_rend;
	// Use this for initialization
	void Start () {
        Color tempColor;
  
        transform.position = initialPosition;
        child_rend = new Renderer[3];
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            if (rend.tag == "grass") child_rend[0] = rend;
            if (rend.tag == "text1") child_rend[1] = rend;
            if (rend.tag == "text2") child_rend[2] = rend;
        }
        tempColor = child_rend[1].material.color;
        tempColor.a = 0f;
        child_rend[1].material.color = tempColor;
        tempColor = child_rend[2].material.color;
        tempColor.a = 0f;
        child_rend[2].material.color = tempColor;
    }

    void Awake()
    {
        m_DanceManager = FindObjectOfType<DanceManager>();
    }


    // Update is called once per frame
    void Update () {

        if(UseTardyFromDanceManager)
            tardy = m_DanceManager.PlayerIsTardy();

        Color tempColor;
        switch (state)
        {
            case 0:
                if (tardy)
                {
                    timeSoFar = 0f;
                    state = 1;
                }
                break;
            case 1:
                if (tardy)
                {
                    timeSoFar += Time.deltaTime;
                    if (timeSoFar > timeToRise)
                    {
                        transform.position = finalPosition;
                        timeSoFar = 0f;
                        state = 2;
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(initialPosition, finalPosition, timeSoFar / timeToRise);
                    }
                } else
                {
                    state = 5;
                    timeSoFar = timeToDuckOut * (1f - timeSoFar / timeToRise);
                }
                break;
            case 2:
                if (tardy)
                {
                    timeSoFar += Time.deltaTime;
                    if (timeSoFar > timeForTextFadeIn)
                    {
                        tempColor = child_rend[1].material.color;
                        tempColor.a = 1f;
                        child_rend[1].material.color = tempColor;
                        timeSoFar = 0f;
                        state = 3;
                    }
                    else
                    {
                        tempColor = child_rend[1].material.color;
                        tempColor.a = timeSoFar / timeForTextFadeIn;
                        child_rend[1].material.color = tempColor;
                    }
                } else
                {
                    tempColor = child_rend[1].material.color;
                    tempColor.a = 0f;
                    child_rend[1].material.color = tempColor;
                    state = 5;
                    timeSoFar = 0f;
                }
                break;
            case 3:
                if (tardy)
                {
                    timeSoFar += Time.deltaTime;
                    if (timeSoFar > timeForTextFadeIn)
                    {
                        tempColor = child_rend[2].material.color;
                        tempColor.a = 1f;
                        child_rend[2].material.color = tempColor;
                        timeSoFar = 0f;
                        state = 4;
                    }
                    else
                    {
                        tempColor = child_rend[2].material.color;
                        tempColor.a = timeSoFar / timeForTextFadeIn;
                        child_rend[2].material.color = tempColor;
                    }
                }
                else
                {
                    tempColor = child_rend[1].material.color;
                    tempColor.a = 0f;
                    child_rend[1].material.color = tempColor;
                    tempColor = child_rend[2].material.color;
                    tempColor.a = 0f;
                    child_rend[2].material.color = tempColor;
                    state = 5;
                    timeSoFar = 0f;
                }
                break;
            case 4:
                if (tardy)
                {

                } else
                {
                    tempColor = child_rend[1].material.color;
                    tempColor.a = 0f;
                    child_rend[1].material.color = tempColor;
                    tempColor = child_rend[2].material.color;
                    tempColor.a = 0f;
                    child_rend[2].material.color = tempColor;
                    state = 5;
                    timeSoFar = 0f;
                }
                break;
            case 5:
                timeSoFar += Time.deltaTime;
                if (timeSoFar > timeToDuckOut)
                {
                    timeSoFar = timeToDuckOut;
                    state = 0;
                }
                transform.position = Vector3.Lerp(finalPosition, initialPosition, timeSoFar / timeToDuckOut);
                break;
            default:
                timeSoFar = 0f;
                state = 0;
                break;
        }
	}
}
