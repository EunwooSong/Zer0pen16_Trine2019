using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapLockData
{
    public bool[] stage_Lock;

    public MapLockData(MapLockData data)
    {
        stage_Lock = new bool[4];
        
        for(int i = 0; i < 4; i++)
            stage_Lock[i] = data.stage_Lock[i];
    }
}
