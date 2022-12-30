using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveProfileSystem : MonoBehaviour
{
    public string profilePath;
    private int count;

    [System.Serializable]
    public class Profile
    {
        public Sprite idPhoto;
        public string passport;
        public string surname;
        public string givenNames;
        public string koreanName;
        public string dateOfBirth;
        public string gender;
    }

    [Header("[Profile Set]")]
    public List<Profile> profileInfo;

    public RectTransform parent;
    List<PlayerData> playerData;

    //List<GameObject> profile;
    GameObject profilePrefab;

    [Header("[Profile UI Animation]")]
    private List<UIAnimation> profileAnimation;
    private int profileCount;

    public Transform proCenter;
    public Transform proLeft;
    public Transform proRight;

    // Start is called before the first frame update
    void Awake()
    {
        playerData = new List<PlayerData>();
        profileAnimation = new List<UIAnimation>();

        if (parent == null)
            parent = GetComponent<RectTransform>();

        profilePrefab = Resources.Load(profilePath) as GameObject;
        count = 0;

        while(true)
        {
            PlayerData tmp;
            tmp = SaveSystem.PlayerDataLoad("playerData" + count + ".playerData");

            if (tmp == null)
            {
                Debug.Log("Player Data Load Done!");
                break;
            }
                

            else
            {
                playerData.Add(tmp);
                count++;
            }
        }

        for (int i = 0; i < count; i++)
        {
            GameObject _profile = Instantiate(profilePrefab) as GameObject;

            //passport Set
            profileInfo[playerData[i].gender].passport = "M" + (int)Random.Range(100.0f, 999.0f) + "A" + (int)Random.Range(100.0f, 999.0f);

            _profile.GetComponent<MainProfileCtrl>().idPhoto.sprite = profileInfo[playerData[i].gender].idPhoto;        //증명사진
            _profile.GetComponent<MainProfileCtrl>().passport.text = profileInfo[playerData[i].gender].passport;        //여권번호
            _profile.GetComponent<MainProfileCtrl>().surname.text = profileInfo[playerData[i].gender].surname;          //성
            _profile.GetComponent<MainProfileCtrl>().givenNames.text = profileInfo[playerData[i].gender].givenNames;    //이름
            _profile.GetComponent<MainProfileCtrl>().koreanName.text = profileInfo[playerData[i].gender].koreanName;    //한국이름
            _profile.GetComponent<MainProfileCtrl>().dateOfBirth.text = profileInfo[playerData[i].gender].dateOfBirth;  //생년월일
            _profile.GetComponent<MainProfileCtrl>().gender.text = profileInfo[playerData[i].gender].gender;            //성별
            _profile.GetComponent<MainProfileCtrl>().dateOfLssued.text = playerData[i].dateOfLssued ;                   //생성된 날짜
            _profile.GetComponent<MainProfileCtrl>().dateOfLast.text = playerData[i].dateOfLast;                        //마지막으로 플레이한 날짜

            _profile.GetComponent<MainProfileCtrl>().saveNum.text = "" + (i + 1);
            _profile.transform.parent = parent;
            _profile.transform.position = parent.position;
            
            
            profileAnimation.Add(_profile.GetComponent<UIAnimation>());
            _profile.GetComponent<UIAnimation>().sFirstPos = proRight;

            //오류 해결
            _profile.GetComponent<RectTransform>().localScale = new Vector2(1.0f, 1.0f);
            
        }

        if(profileAnimation[0] != null)
            profileAnimation[0].sFirstPos = proCenter;
        
        SceneInfo.lastProfileNum = count;

        NextProfileLeft();
        NextProfileRight();
    }

    public void NextProfileRight()
    {
        if (count == 1)
            return;

        profileCount++;

        if (profileCount >= profileAnimation.Count)
            profileCount = 0;

        profileAnimation[profileCount].sFirstPos = proCenter;

        if (profileCount == 0)
            profileAnimation[profileAnimation.Count - 1].sFirstPos = proLeft;
        else
            profileAnimation[profileCount - 1].sFirstPos = proLeft;

        if (profileCount == profileAnimation.Count - 1)
        {
            profileAnimation[0].recttr.position = proRight.position;
            profileAnimation[0].sFirstPos = proRight;
        }

        else
        {
            profileAnimation[profileCount + 1].recttr.position = proRight.position;
            profileAnimation[profileCount + 1].sFirstPos = proRight;
        }
    }

    public void NextProfileLeft()
    {
        if (count == 1)
            return;

        profileCount--;

        if (profileCount <= -1)
            profileCount = profileAnimation.Count - 1;

        profileAnimation[profileCount].sFirstPos = proCenter;

        if (profileCount == profileAnimation.Count - 1)
            profileAnimation[0].sFirstPos = proRight;
        else
            profileAnimation[profileCount + 1].sFirstPos = proRight;

        if (profileCount == 0)
        {
            profileAnimation[profileAnimation.Count - 1].recttr.position = proLeft.position;
            profileAnimation[profileAnimation.Count - 1].sFirstPos = proLeft;
        }

        else
        {
            profileAnimation[profileCount - 1].recttr.position = proLeft.position;
            profileAnimation[profileCount - 1].sFirstPos = proLeft;
        }
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.PlayerDataLoad("playerData" + profileCount + ".playerData");
        SceneInfo.saveFileNum = profileCount;

        SceneInfo.sceneName = data.sceneName;

        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }
}
