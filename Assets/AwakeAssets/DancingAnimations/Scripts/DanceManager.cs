using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DanceManager : MonoBehaviour {
    public GameObject targetPrefab;
    private StarfieldMover m_StartFieldMover;
    public string AnimationDirectory = "Q:\\TeamSheeps\\TeamSheeps2018\\TeamSheeps2018\\Assets\\AwakeAssets\\DancingAnimations\\Dances\\";
    public List<string> KeyframeAnimations;
    private KeyframeData m_ActiveData = new KeyframeData();
    private int m_listIndex = 0;
    private int m_keyFrameIndex = 0;
    private List<GameObject> m_ActiveTargets;
    private float m_currentScore;
    private bool m_playerIsTardy = false;

    public bool UsePeriodicPointTimer = false;
    public float freePointInterval = 0f;
    private float m_LastStarfieldPosY;
    float countdownToPoint;
    public int winningScore = 100;
    public float tardyTimeout = 4f;
    private float m_tardyCounter = 0f;

    void Awake()
    {
        m_StartFieldMover = FindObjectOfType<StarfieldMover>();
        m_LastStarfieldPosY = m_StartFieldMover.transform.position.y;
    }

    private void SetupNextDanceAnimation()
    {
        m_keyFrameIndex = 0;
        m_tardyCounter = 0f;
        string path = Path.Combine(AnimationDirectory, KeyframeAnimations[m_listIndex]);
        string json = File.ReadAllText(path);
        m_ActiveData = JsonUtility.FromJson<KeyframeData>(json);
        CreateTargetsForKeyFrame(m_keyFrameIndex);

        countdownToPoint = freePointInterval;
    }

    // Use this for initialization
    void Start () {
        m_currentScore = 0;
        m_ActiveTargets = new List<GameObject>();
        m_listIndex = 0;
        m_tardyCounter = 0f;
        m_playerIsTardy = false;
        SetupNextDanceAnimation();
    }

    public int GetCurrentScore()
    {
        return (int)m_currentScore;
    }

    public bool PlayerIsTardy()
    {
        return m_playerIsTardy;
    }

    void CreateTargetsForKeyFrame(int keyframe)
    {
        // Create a copy of the targets for the last frame so that we can
        // use them as our starting positions
        List<GameObject> lastActiveTargets = new List<GameObject>();
        foreach (GameObject obj in m_ActiveTargets)
        {
            lastActiveTargets.Add(obj);
            //m_currentScore++;
        }
        m_ActiveTargets.Clear();
        m_tardyCounter = 0f;

        // Create a new set of targets based on the next key frame (starting them at the
        // position of our last targets)
        if (m_ActiveData.KeyFrames.Count > 0 && keyframe < m_ActiveData.KeyFrames.Count)
        {
            for(int index = 0; index < m_ActiveData.KeyFrames[keyframe].vec3List.Count; index++)
            {
                Vector3 startingPosition;
                if (lastActiveTargets.Count == 0)
                {
                    startingPosition = m_ActiveData.KeyFrames[keyframe].vec3List[index].vector;
                }
                else
                {
                    startingPosition = lastActiveTargets[index].transform.position;
                }
                GameObject newTarget = Instantiate(targetPrefab, startingPosition, Quaternion.identity);
                Vector3 targetPosition = m_ActiveData.KeyFrames[keyframe].vec3List[index].vector;
                if (m_LastStarfieldPosY != m_StartFieldMover.transform.position.y)
                {
                    targetPosition.y = m_StartFieldMover.transform.position.y + m_StartFieldMover.Speed * 8f;
                }
                newTarget.GetComponent<TargetScript>().TargetPostion = targetPosition;
                m_ActiveTargets.Add(newTarget);
            }
        }

        // Destroy the last active targets, we no longer need them
        foreach (GameObject obj in lastActiveTargets)
        {
            obj.GetComponent<TargetScript>().HandleDestruction();
        }
        lastActiveTargets.Clear();
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
        m_currentScore += Time.deltaTime;
        //if (freePointInterval > 0f && UsePeriodicPointTimer)
        //{
        //    countdownToPoint -= Time.deltaTime;
        //    if (countdownToPoint < 0f)
        //    {
        //        countdownToPoint = freePointInterval;
        //        m_currentScore++;
        //    }
        //}
        if(AllTargetsInFrameAreActivated())
        {
            m_keyFrameIndex++;
            if (m_keyFrameIndex < m_ActiveData.KeyFrames.Count)
            {
                CreateTargetsForKeyFrame(m_keyFrameIndex);
            }
            else
            {
                m_listIndex++;
                SetupNextDanceAnimation();
            }
        }

        m_tardyCounter += Time.deltaTime;
        if(m_tardyCounter > tardyTimeout)
        {
            m_playerIsTardy = true;
        }
        else
        {
            m_playerIsTardy = false;
        }
    }
}
