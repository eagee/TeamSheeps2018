using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool Activated = false;
    public Sprite InactiveSprite;
    public Sprite ActiveSprite;
    public Vector3 TargetPostion = new Vector3();
    private float m_MaxDisabledTime;
    private float m_DisabledTimer;
    
    public void HandleDestruction()
    {
        DestroyObject(this.gameObject);
    }

    void Start()
    {
        m_DisabledTimer = 0f;
        m_MaxDisabledTime = .40f;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Activated = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Activated = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Activated = false;
        }
    }

    void Update()
    {
        m_DisabledTimer += Time.deltaTime;
        if (m_DisabledTimer > m_MaxDisabledTime)
        {
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        
        if (Activated)
        {
            GetComponent<SpriteRenderer>().sprite = ActiveSprite;
        }
        else
        {
            this.transform.position = Vector3.Slerp(this.transform.position, TargetPostion, 2.5f * Time.deltaTime);
            GetComponent<SpriteRenderer>().sprite = InactiveSprite;
        }
    }
}


