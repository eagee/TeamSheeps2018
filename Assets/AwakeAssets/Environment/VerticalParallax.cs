using UnityEngine;
using System.Collections;

public class VerticalParallax : MonoBehaviour
{

    public SmoothCamera2D CameraObject;
    public float CameraOffset;

    private bool m_Enabled = true;
    private float m_InitialY;
    private float m_CameraOffset;
    private Vector3 m_StartingPosition;
    private Vector3 m_CameraStartingPosition;

    // Use this for initialization
    void Start()
    {
        if (CameraObject == null) CameraObject = FindObjectOfType<SmoothCamera2D>();
        m_CameraOffset = CameraOffset;
        m_StartingPosition = this.transform.position;
        m_CameraStartingPosition = CameraObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
      float cameraYOffset = (CameraObject.transform.position.y - m_CameraStartingPosition.y) / CameraOffset;
      Vector3 nuPosition = this.transform.position;
      nuPosition.y = m_StartingPosition.y + cameraYOffset;
      //nuPosition.y = m_StartingPosition.y - ((CameraObject.transform.position.y - m_CameraOffset.) / m_CameraOffset);
      this.transform.position = nuPosition;
    }
}
