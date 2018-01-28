using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool Activated = false;
    public Vector3 TargetPostion = new Vector3();
    private float m_MaxDisabledTime;
    private float m_DisabledTimer;

    public Vector3 wander1;
    public Vector3 wander2;
    public float wanderSpeed;
    public float rotateSpeed;

    public void HandleDestruction()
    {
        GetComponent<Animator>().SetBool("TargetDying", true);
        DestroyObject(this.gameObject, 2.0f);
    }

    void Start()
    {
        m_DisabledTimer = 0f;
        m_MaxDisabledTime = .40f;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = true;
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
            GetComponent<TrailRenderer>().enabled = false;
            Vector3 w1 = this.transform.position + (Mathf.Sin(Time.time * wanderSpeed) * wander1);
            Vector3 w2 = this.transform.position + (Mathf.Cos(Time.time * wanderSpeed) * wander2);
            TargetPostion = Vector3.Lerp(w1, w2, 0.5f);
        }
        
        if (!Activated)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, TargetPostion, 2.5f * Time.deltaTime);
            this.transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }
    }
}


