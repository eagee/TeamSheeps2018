using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System.IO;

public class KinectKeyFrameRecorder : MonoBehaviour {

    private Dictionary<JointType, GameObject> m_jointObjects;
    private KeyframeData m_keyframeData;

    // Use this for initialization
    void Start () {
        m_jointObjects = new Dictionary<JointType, GameObject>();
        m_keyframeData = new KeyframeData();
        m_keyframeData.Name = "TestDance";
        var joints = this.GetComponentsInChildren<JointTracker>();
        foreach(JointTracker j in joints)
        {
            m_jointObjects[j.JointToUse] = j.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Adding new keyframe!");
            Vec3ListWrapper newKeyFrame = new Vec3ListWrapper();

            // Iterate through each joint and output code to console with initialized vector3 list
            foreach (KeyValuePair<JointType, GameObject> jointData in m_jointObjects)
            {
                Debug.Log("Adding joint!");
                newKeyFrame.vec3List.Add(jointData.Value.transform.position);
            }

            m_keyframeData.KeyFrames.Add(newKeyFrame);
            Debug.Log("Added new keyframe!");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            string filePath = "NewKeyFrameFile.json";
            Debug.Log("Writing keyframe file to: " + filePath);
            string json = JsonUtility.ToJson(m_keyframeData);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, json);
            }
            
        }
    }
}
