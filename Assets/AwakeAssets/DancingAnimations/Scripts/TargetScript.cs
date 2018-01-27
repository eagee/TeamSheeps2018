using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool Activated = false;
    public Sprite InactiveSprite;
    public Sprite ActiveSprite;

    public void HandleDestruction()
    {
        DestroyObject(this.gameObject);
    }

    void Start()
    {
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
        if (Activated)
        {
            GetComponent<SpriteRenderer>().sprite = ActiveSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = InactiveSprite;
        }
    }
}


