using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void SceneChange(string sceneName)
    {
        SceneInfo.sceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}