using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBtnCtrl : MonoBehaviour
{
    public Text gender;
    public Text saveNum;
    public PlayerCtrl player;

    // Update is called once per frame
    void Update()
    {
        gender.text = "" + player.gender;
        saveNum.text = "" + SceneInfo.saveFileNum;
    }

    public void saveNumUp()
    {
        SceneInfo.saveFileNum += 1;
    }

    public void saveNumDown()
    {
        SceneInfo.saveFileNum -= 1;

        if (SceneInfo.saveFileNum < 0)
            SceneInfo.saveFileNum = 0;
    }


    public void genderUp()
    {
        player.gender += 1;
    }

    public void genderDown()
    {
        player.gender -= 1;
    }


    public void Save()
    {
        player.Save();
    }

    public void Load()
    {
        player.Load();
    }
}
