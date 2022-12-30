using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [Header("[BGM Manager]")]
    public List<AudioSource> bgmSource;
    public List<AudioSource> sfxSource;

    private int resolCount;
    private float bgmVolume;
    private float sfxVolume;
    private bool isFullScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        //Auto Input SfxSources
        if (GameObject.FindGameObjectWithTag("Player") != null)
            sfxSource.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>());

        sfxSource.Add(GameObject.FindGameObjectWithTag("GameMgr").GetComponent<MainSceneManager>().sfxSource);

        //Audio자동추가 시스템 추후 개발해야함
        resolCount = OptionData.resolCount;
        bgmVolume = OptionData.bgmVolume;
        sfxVolume = OptionData.sfxVolume;
        isFullScreen = OptionData.isFullScrene;

        SetResolution();
        SetBGMVolume();
        SetSFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmVolume != OptionData.bgmVolume)
            SetBGMVolume();

        if (sfxVolume != OptionData.sfxVolume)
            SetSFXVolume();

        if (resolCount != OptionData.resolCount || isFullScreen != OptionData.isFullScrene)
            SetResolution();
    }

    void SetResolution()
    {
        resolCount = OptionData.resolCount;
        isFullScreen = OptionData.isFullScrene;

        switch(resolCount)
        {
            case 0:
                Screen.SetResolution(800, 450, isFullScreen);
                Debug.Log("SD Resolution Change Success!");
                break;

            case 1:
                Screen.SetResolution(1280, 720, isFullScreen);
                Debug.Log("HD Resolution Change Success!");
                break;

            case 2:
                Screen.SetResolution(1920, 1080, isFullScreen);
                Debug.Log("FHD Resolution Change Success!");
                break;

            case 3:
                Screen.SetResolution(2560, 1440, isFullScreen);
                Debug.Log("QHD Resolution Change Success!");
                break;

            case 4:
                Screen.SetResolution(3840, 2160, isFullScreen);
                Debug.Log("UHD Resolution Change Success!");
                break;
        }
    }

    void SetBGMVolume()
    {
        bgmVolume = OptionData.bgmVolume;

        foreach(AudioSource bgm in bgmSource)
        {
            bgm.volume = bgmVolume;
        }

        Debug.Log("Set New BGM Volume Succeeded!");
        return;
    }

    void SetSFXVolume()
    {
        sfxVolume = OptionData.sfxVolume;

        foreach (AudioSource sfx in sfxSource)
        {
            sfx.volume = sfxVolume;
        }

        Debug.Log("Set New SFX Volume Succeeded!");
        return;
    }
}
