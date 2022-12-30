using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatPlayerProfile : MonoBehaviour
{
    [Header("[Gender]")]
    public int gender;
    private int count = 0;
    public Transform Empty;

    
    [Header("[Move]")]
    public string sceneName;

    // Start is called before the first frame update
    public void CreateProfile(int gender)
    {
        count++;

        if (count == 1)
        {
            this.gender = gender;

            Debug.Log("Click Again");
            return;
        }

        else if(this.gender != gender)
        {
            count = 1;

            Debug.Log("Click Again");

            this.gender = gender;
            return;
        }

        PlayerCtrl player = new PlayerCtrl();
        
        player.tr = Empty;                      //오류 해결을 위함
        player.tr.position = new Vector3(-18.0f, -2.0f, 0.0f);      //스테이지 1의 시작 위치 정보로 저장시켜야함
        player.gender = gender;
        player.coin = 0;

        string path = "playerData" + SceneInfo.lastProfileNum + ".playerData";

        SaveSystem.CreateProfile(player, path);
        
        SceneInfo.saveFileNum = SceneInfo.lastProfileNum;
        SceneInfo.lastProfileNum += 1;
        SceneInfo.sceneName = sceneName;      //스테이지 1으로 가야함

        Debug.Log("Now Save Profile Count(NUM) : " + SceneInfo.saveFileNum);
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }
}
