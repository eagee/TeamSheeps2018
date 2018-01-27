using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class KeyframeData
{
    public string Name;
    public List<Vec3ListWrapper> KeyFrames = new List<Vec3ListWrapper>();
}
