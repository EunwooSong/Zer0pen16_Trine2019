using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    Slider loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation oper = SceneManager.LoadSceneAsync(SceneInfo.sceneName);

        oper.allowSceneActivation = false;

        float timer = 0.0f;

        while (!oper.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (oper.progress >= 0.9f)
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, 1f, timer);

                if (loadingBar.value == 1.0f)
                    oper.allowSceneActivation = true;
            }

            else
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, oper.progress, timer);

                if (loadingBar.value >= oper.progress)
                    timer = 0f;
            }
        }
    }
}
