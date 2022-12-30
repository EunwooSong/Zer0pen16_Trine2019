using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainSceneManager : MonoBehaviour
{
    [Header("[UI ON/OFF]")]
    public GameObject mainUi;
    public GameObject questUI;
    public bool isUseOnOffFunc;

    [Header("[Blocks]")]
    public UIAnimation main;
    public UIAnimation setting;
    public UIAnimation game;
    public GameObject back;
    public GameObject apply;

    [Header("[Resolution]")]
    public List<UIAnimation> resolution;
    public Transform reCenter;
    public Transform reLeft;
    public Transform reRight;

    [Header("[FullScreen]")]
    public List<UIAnimation> fullScreen;
    public Transform fuCenter;
    public Transform fuLeft;
    public Transform fuRight;

    [Header("[Volume]")]
    public Slider bgm;
    public Slider sfx;

    [Header("[Setting Values]")]
    public int resolCount;          //0 : HD, 1 : FHD, 2 : QHD
    public float bgmVolume;         //배경음악
    public float sfxVolume;         //효과음
    public bool isFullScreen;       //전체화면

    [Header("[Save & Load Path]")]
    public string path;

    [Header("[Sound Controller]")]
    public bool start;
    public AudioSource sfxSource;
    public AudioClip mapClick;
    public AudioClip btnClick;
    public AudioClip bellClip;

    void Awake()
    {
        start = true;

        sfxSource = GetComponent<AudioSource>();
        mapClick = Resources.Load<AudioClip>("Audio/Click_Map");
        btnClick = Resources.Load<AudioClip>("Audio/Click_Btn");
        bellClip = Resources.Load<AudioClip>("Audio/Bell_Clip");

        Load();
        questUI = GameObject.FindGameObjectWithTag("QuestUI");

        foreach (UIAnimation i in resolution)
            i.sFirstPos = reRight;

        foreach (UIAnimation i in fullScreen)
            i.sFirstPos = fuRight;
        
        Save();
    }

    // Start is called before the first frame update
    void Start()
    {
        main.sGoLate = false;
        setting.sGoLate = false;
        game.sGoLate = false;

        //Error Fix : Unassigned Reference Exception
        foreach (UIAnimation re in resolution)
            re.recttr = re.gameObject.GetComponent<RectTransform>();
        foreach (UIAnimation fu in fullScreen)
            fu.recttr = fu.gameObject.GetComponent<RectTransform>();

        resolution[resolCount].sFirstPos = reCenter;

        SetFullScreenLeft();
        SetFullScreenRight();

        SetResolutionLeft();
        SetResolutionRight();   

        back.SetActive(false);
        apply.SetActive(false);

        if (isUseOnOffFunc)
        {
            mainUi.SetActive(false);
        }

        start = false;

        string name = SceneManager.GetActiveScene().name;
        

        if (name.Equals("Stage_1") || name.Equals("Stage_2") || name.Equals("Stage_4") || name.Equals("Stage_5") || name.Equals("MainScene"))
            Debug.Log("Current Scene Name : " + name);

        else
            sfxSource.PlayOneShot(bellClip);

        Debug.Log("Setting Default Succeeded!");
    }

    void Update()
    {
        if (bgm.value != bgmVolume)
        {
            apply.SetActive(true);
        }

        if (sfx.value != sfxVolume)
        {
            apply.SetActive(true);
        }

        if(isUseOnOffFunc)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mainUi.SetActive(!mainUi.activeSelf);
                FirstScene();
            }

            else if (Input.GetAxis("Horizontal") > 0.25f || Input.GetAxis("Horizontal") < -0.25f)
                mainUi.SetActive(false);
        }

        if(questUI != null)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                questUI.SetActive(!questUI.activeSelf);
            }
        }
    }

    //Setting Animation
    public void Save()
    {
        bgmVolume = bgm.value;
        sfxVolume = sfx.value;

        SaveSystem.Save(this, path);

        OptionData.resolCount = resolCount;
        OptionData.bgmVolume = bgmVolume;
        OptionData.sfxVolume = sfxVolume;
        OptionData.isFullScrene = isFullScreen;

        BtnSfxPlayOnce();
    }

    void Load()
    {
        SettingData data = SaveSystem.SettingDataLoad(path);

        if(data == null)
        {
            resolCount = 2;
            bgmVolume = 0.7f;
            sfxVolume = 0.7f;

            bgm.value = bgmVolume;
            sfx.value = sfxVolume;
            isFullScreen = false;

            Save();
            Debug.Log("Set Default!");
            return;
        }

        Debug.Log("Load Success!");
        resolCount = data.resolCount;
        bgmVolume = data.bgmVolume;
        sfxVolume = data.sfxVolume;
        isFullScreen = data.isFullScreen;

        bgm.value = bgmVolume;
        sfx.value = sfxVolume;
    }

    //Resolution Position Ctrl
    public void SetResolutionRight()
    {
        resolCount++;

        if (resolCount >= resolution.Count)
            resolCount = 0;

        resolution[resolCount].sFirstPos = reCenter;

        if (resolCount == 0)
            resolution[resolution.Count - 1].sFirstPos = reLeft;
        else
            resolution[resolCount - 1].sFirstPos = reLeft;

        if (resolCount == resolution.Count - 1)
        {
            resolution[0].recttr.position = reRight.position;
            resolution[0].sFirstPos = reRight;
        }
            
        else
        {
            resolution[resolCount + 1].recttr.position = reRight.position;
            resolution[resolCount + 1].sFirstPos = reRight;
        }

        apply.SetActive(true);
        BtnSfxPlayOnce();
    }
    public void SetResolutionLeft()
    {
        resolCount--;

        if (resolCount <= -1)
            resolCount = resolution.Count - 1;

        resolution[resolCount].sFirstPos = reCenter;

        if (resolCount == resolution.Count - 1)
            resolution[0].sFirstPos = reRight;

        else
            resolution[resolCount + 1].sFirstPos = reRight;

        if (resolCount == 0)
        {
            resolution[resolution.Count - 1].recttr.position = reLeft.position;
            resolution[resolution.Count - 1].sFirstPos = reLeft;
        }

        else
        {
            resolution[resolCount - 1].recttr.position = reLeft.position;
            resolution[resolCount - 1].sFirstPos = reLeft;
        }

        apply.SetActive(true);
        BtnSfxPlayOnce();
    }

    //FullScreen Ctrl
    public void SetFullScreenRight()
    {
        isFullScreen = !isFullScreen;

        if(isFullScreen)
        {
            fullScreen[1].recttr.position = fuRight.position;
            fullScreen[1].sFirstPos = fuCenter;
            fullScreen[0].sFirstPos = fuLeft;
        }
        else
        {
            fullScreen[0].recttr.position = fuRight.position;
            fullScreen[0].sFirstPos = fuCenter;
            fullScreen[1].sFirstPos = fuLeft;

        }

        apply.SetActive(true);
        BtnSfxPlayOnce();
    }
    public void SetFullScreenLeft()
    {
        isFullScreen = !isFullScreen;

        if (isFullScreen)
        {
            fullScreen[1].recttr.position = fuLeft.position;
            fullScreen[1].sFirstPos = fuCenter;
            fullScreen[0].sFirstPos = fuRight;
        }
        else
        {
            fullScreen[0].recttr.position = fuLeft.position;
            fullScreen[0].sFirstPos = fuCenter;
            fullScreen[1].sFirstPos = fuRight;

        }

        apply.SetActive(true);
        BtnSfxPlayOnce();
    }

    //UI Animation Ctrl
    public void UseSetting()
    {
        BtnSfxPlayOnce();
        if (game.sGoLate)
        {
            main.sGoLate = true;
            setting.sGoLate = true;
        }
        else
        {
            main.sGoLate = !main.sGoLate;
            setting.sGoLate = !setting.sGoLate;
            Save();
        }

        if (main.sGoLate)
            back.SetActive(true);
        else
            back.SetActive(false);

        game.sGoLate = false;

        apply.SetActive(false);
    }
    public void StartGame()
    {
        BtnSfxPlayOnce();
        main.sGoLate = true;
        setting.sGoLate = false;
        game.sGoLate = true;
        back.SetActive(true);
        apply.SetActive(false);
    }
    public void FirstScene()
    {
        BtnSfxPlayOnce();
        main.sGoLate = false;
        setting.sGoLate = false;
        game.sGoLate = false;

        back.SetActive(false);
        apply.SetActive(false);
        Save();
    }

    public void BtnSfxPlayOnce()
    {
        if (start)
            return;

        sfxSource.PlayOneShot(btnClick);   
    }

    public void MapSfxPlayOnce()
    {
        if (start)
            return;

        sfxSource.PlayOneShot(mapClick);
    }

    //Move Scene
    public void GoMain()
    {
        SceneInfo.sceneName = "MainScene";
        SceneManager.LoadScene("LoadingScene");
    }

    public void GoSelectCharacter()
    {
        SceneInfo.sceneName = "SelectCharacterScene";
        SceneManager.LoadScene("LoadingScene");
    }

    public void GameClose()
    {
        Save();
        Application.Quit();
    }

    public void SaveScene()
    {
        BtnSfxPlayOnce();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>().Save();
        GetComponent<NPCManager>().Save();

        Debug.Log("SavePlayer Succeeded");
    }
}
