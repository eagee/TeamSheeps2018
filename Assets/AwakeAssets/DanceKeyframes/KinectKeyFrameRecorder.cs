using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System.IO;

public class KinectKeyFrameRecorder : MonoBehaviour {
    public string AnimationName = "";
    private Dictionary<JointType, GameObject> m_jointObjects;
    private KeyframeData m_keyframeData;
    private bool m_Recording;
    private float m_frameTimer;

    // Use this for initialization
    void Start()
    {
        m_Recording = false;
        m_jointObjects = new Dictionary<JointType, GameObject>();
        m_keyframeData = new KeyframeData();
        m_keyframeData.Name = "TestDance";
        var joints = this.GetComponentsInChildren<JointTracker>(false);
        foreach (JointTracker j in joints)
        {
            m_jointObjects[j.JointToUse] = j.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_frameTimer += Time.deltaTime;
        if (m_Recording && m_frameTimer >= 1.0f / 16.0f)
        {
            m_frameTimer = 0;
            RecordKeyframe();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            m_Recording = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            m_Recording = false;
            SaveKeyframesToJson();
        }
    }

    private void RecordKeyframe()
    {
        Vec3ListWrapper newKeyFrame = new Vec3ListWrapper();

        // Iterate through each joint and output code to console with initialized vector3 list
        foreach (KeyValuePair<JointType, GameObject> jointData in m_jointObjects)
        {
            Vec3Wrapper vec3 = new Vec3Wrapper();
            vec3.vector = jointData.Value.transform.position;
            newKeyFrame.vec3List.Add(vec3);
        }

        m_keyframeData.KeyFrames.Add(newKeyFrame);
        Debug.Log("Added new keyframe!");
    }

    private void SaveKeyframesToJson()
    {
        string filePath = "C:\\Temp\\" + AnimationName + ".json";
        Debug.Log("Writing keyframe file to: " + filePath);
        string json = JsonUtility.ToJson(m_keyframeData);

        while(File.Exists(filePath))
        {
            filePath = "C:\\Temp\\" + AnimationName + Random.Range(0, 10000).ToString() + ".json";
        }

        File.WriteAllText(filePath, json);
    }
}
