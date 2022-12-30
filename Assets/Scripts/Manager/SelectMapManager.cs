using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMapManager : MonoBehaviour
{
    [Header("[Country Data]")]
    public GameObject countryDataGameObj;
    MapData countryData;

    //Country Info Count : 0 ~ 3, 4p
    [System.Serializable]
    public class CountryInfo
    {
        public Sprite mapImage;
        public string countryName;
        public string countryInfo;
        public string sceneName;
        public int mapCost;
    }
    public List<CountryInfo> countryInfo;

    [Header("[MapData]")]
    public List<GameObject> mapLockGameObj;
    public MapLockData mlData;
    public int stageNum;

    [Header("[Country Animation]")]
    public List<Transform> countryPos;
    public SelectMapAnimation countryAnimation;
    public GameObject setDefaultBtn;

    public GameObject Info;
    //[Header("")]

    // Start is called before the first frame update
    private void Start()
    {
        countryData = countryDataGameObj.GetComponent<MapData>();
        countryDataGameObj.SetActive(false);
        setDefaultBtn.SetActive(false);

        countryInfo[2].mapImage = Resources.Load<Sprite>("Image/Czechia");


        if (SaveSystem.MapLockDataLoad("MapDataLock" + SceneInfo.saveFileNum + ".mapDataLock") == null)
        {
            mlData.stage_Lock = new bool[4];
            for(int i = 0; i < 4; i++)
            {
                mlData.stage_Lock[i] = true;
            }
            mlData.stage_Lock[0] = false;

            Save();
            Debug.Log("Create New Map Lock Data!");
        }

        mlData = SaveSystem.MapLockDataLoad("MapDataLock" + SceneInfo.saveFileNum + ".mapDataLock");
        
        for(int i = 0; i < 4; i ++)
        {
            mapLockGameObj[i].SetActive(mlData.stage_Lock[i]);
        }

        refreshLockMode();
    }

    //맵 기본정보 생성
    private void SetMapInfo(int mapNum)
    {
        //Set Map 
        countryData.mapImage.sprite = countryInfo[mapNum].mapImage;
        countryData.mapName.text = countryInfo[mapNum].countryName;
        countryData.mapInfo.text = countryInfo[mapNum].countryInfo;

        if (mlData.stage_Lock[mapNum])
            countryData.canUnLock.SetActive(true);
        else
            countryData.canUnLock.SetActive(false);

        //Set Stage Name
        SceneInfo.sceneName = countryInfo[mapNum].sceneName;
    }

    //맵 선택
    public void SelectStage(int stage_num)
    {
        SetMapInfo(stage_num);
        stageNum = stage_num;

        countryAnimation.movePos = countryPos[stage_num];
        countryDataGameObj.SetActive(true);
        setDefaultBtn.SetActive(true);

        GameObject.FindGameObjectWithTag("GameMgr").GetComponent<MainSceneManager>().MapSfxPlayOnce();
    }

    //초기값
    public void SelectDefault()
    {
        SetMapInfo(0);

        countryAnimation.movePos = countryPos[4];

        setDefaultBtn.SetActive(false);
        countryDataGameObj.SetActive(false);
    }

    public void LockCtrl(int num, bool isLock)
    {
        mlData.stage_Lock[num] = isLock;
    }

    //새로고침
    public void refreshLockMode()
    {
        for (int i = 0; i < 4; i++)
        {
            mapLockGameObj[i].SetActive(mlData.stage_Lock[i]);
        }

        if (mlData.stage_Lock[stageNum])
            countryData.canUnLock.SetActive(true);
        else
            countryData.canUnLock.SetActive(false);
    }

    //UnLock하기
    public void UnLock()
    {
        PlayerCtrl player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();

        if (player.coin >= countryInfo[stageNum].mapCost) {
            player.coin -= countryInfo[stageNum].mapCost;
            mlData.stage_Lock[stageNum] = false;

            Debug.Log("Map UnLock Succeeded - " + player.coin);
            Save();
            refreshLockMode();
        }
        else
        {
            StartCoroutine(LowCoin());
        }
    }

    public void MoveScene()
    {
        Save();
        GameObject.FindGameObjectWithTag("GameMgr").GetComponent<MainSceneManager>().SaveScene();

        if (mlData.stage_Lock[stageNum] || !countryData.gameObject.activeSelf)
            return;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }

    public IEnumerator LowCoin()
    {
        Info.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        Info.SetActive(false);
    }

    public void Save()
    {
        SaveSystem.Save(mlData, "MapDataLock" + SceneInfo.saveFileNum + ".mapDataLock");
    }
}
