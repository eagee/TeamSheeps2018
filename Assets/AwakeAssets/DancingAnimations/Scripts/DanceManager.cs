using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DanceManager : MonoBehaviour {
    public GameObject targetPrefab;
    public string AnimationDirectory = "Q:\\TeamSheeps\\TeamSheeps2018\\TeamSheeps2018\\Assets\\AwakeAssets\\DancingAnimations\\Dances\\";
    public List<string> KeyframeAnimations;
    private KeyframeData m_ActiveData = new KeyframeData();
    private int m_listIndex = 0;
    private int m_keyFrameIndex = 0;
    private List<GameObject> m_ActiveTargets;
    
    // Use this for initialization
    void Start () {
        m_listIndex = 0;
        string path = Path.Combine(AnimationDirectory, KeyframeAnimations[m_listIndex]);
        string json = File.ReadAllText(path);
        m_ActiveData = JsonUtility.FromJson< KeyframeData>(json);
        m_ActiveTargets = new List<GameObject>();
        CreateTargetsForKeyFrame(m_keyFrameIndex);
    }

    void CreateTargetsForKeyFrame(int keyframe)
    {
        if(keyframe < m_ActiveData.KeyFrames.Count)
        {
            foreach(Vec3Wrapper pos in m_ActiveData.KeyFrames[keyframe].vec3List)
            {
                GameObject newTarget = Instantiate(targetPrefab, pos.vector, Quaternion.identity);
                m_ActiveTargets.Add(newTarget);
            }
        }
    }
    
    bool AllTargetsInFrameAreActivated()
    {
        int activatedCount = 0;

        if(m_ActiveTargets.Count == 0)
        {
            return false;
        }

        foreach(GameObject obj in m_ActiveTargets)
        {
            if(obj != null && obj.GetComponent<TargetScript>().Activated == true)
            {
                activatedCount++;
            }
        }

        if(activatedCount == m_ActiveTargets.Count)
        {
            return true;
        }

        return false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(AllTargetsInFrameAreActivated())
        {
            foreach(GameObject obj in m_ActiveTargets)
            {
                obj.GetComponent<TargetScript>().HandleDestruction();
            }
            m_ActiveTargets.Clear();
            m_keyFrameIndex++;
            if (m_keyFrameIndex < m_ActiveData.KeyFrames.Count)
            {
                CreateTargetsForKeyFrame(m_keyFrameIndex);
            }
        }
	}
}
