using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpData : MonoBehaviour
{
    public string warpSceneName;

    public void Enter()
    {
        GameObject.FindGameObjectWithTag("GameMgr").GetComponent<MainSceneManager>().SaveScene();

        SceneInfo.sceneName = warpSceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }
}
