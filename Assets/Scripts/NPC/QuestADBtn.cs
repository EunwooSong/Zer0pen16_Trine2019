using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestADBtn : MonoBehaviour
{
    private UIAnimation uiAnim;
    public bool isAcceptQuest;
    public bool isClicked;

    private void Start()
    {
        uiAnim = GetComponent<UIAnimation>();
        ResetData();
    }

    public void ShowUp()
    {
        uiAnim.SmoothGoLater();
    }

    public void ShowDown()
    {
        uiAnim.SmoothGoFirst();
    }

    public void Accept()
    {
        isClicked = true;
        isAcceptQuest = true;
    }

    public void Declined()
    {
        isClicked = true;
        isAcceptQuest = false;
    }

    public void ResetData()
    {
        isAcceptQuest = false;
        isClicked = false;
    }
}
