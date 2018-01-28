using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbEventScript : MonoBehaviour {

    public int requiredScore = 100;
    public GameObject PrefabToSpawn;
    private DanceManager m_DanceManager;

    void Awake()
    {
        m_DanceManager = FindObjectOfType<DanceManager>();
    }

    void Update()
    {
        if(m_DanceManager.GetCurrentScore() > requiredScore)
        {
            GameObject newTarget = Instantiate(PrefabToSpawn, this.transform.position, Quaternion.identity);
            DestroyObject(this.gameObject);
        }
    }
}
