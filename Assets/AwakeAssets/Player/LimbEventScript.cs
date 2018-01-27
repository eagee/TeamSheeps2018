using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbEventScript : MonoBehaviour {

    public int requiredScore = 100;

    private DanceManager m_DanceManager;

    void Awake()
    {
        m_DanceManager = FindObjectOfType<DanceManager>();
    }

    void Update()
    {
        if(m_DanceManager.GetCurrentScore() > requiredScore)
        {
            DestroyObject(this.gameObject);
        }
    }
}
