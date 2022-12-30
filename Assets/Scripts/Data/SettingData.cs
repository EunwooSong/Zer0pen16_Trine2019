using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    public float bgmVolume;
    public float sfxVolume;
    public int resolCount;
    public bool isFullScreen;

    public SettingData(MainSceneManager msm)
    {
        bgmVolume = msm.bgmVolume;
        sfxVolume = msm.sfxVolume;
        resolCount = msm.resolCount;
        isFullScreen = msm.isFullScreen;
    }
}
