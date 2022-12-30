using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingNPCQuest : MonoBehaviour
{
    [Header("Quests 1~4(or more)")]
    public NPCController npcCtrl;
    public QuestADBtn restart;
    public QuestADBtn[] questADBtns;
    public string[] questInfo;
    public int currentQuest;

    public bool questEnd;
    public bool questClear;

    public void Start()
    {
        npcCtrl = GetComponent<NPCController>();
        questEnd = false;
        questClear = false;

        ResetQuest();
    }

    public IEnumerator StartNPCQuest()
    {
        ResetQuest();

        while(true)
        {
            questADBtns[currentQuest].ShowUp();
            npcCtrl.dialogBundle.dialogText.text = questInfo[currentQuest];
            
            if(questADBtns[currentQuest].isClicked)
            {
                if (questADBtns[currentQuest].isAcceptQuest)
                {
                    questADBtns[currentQuest++].ShowDown();

                    if (currentQuest == questInfo.Length)
                    {
                        questClear = true;
                        break;
                    }
                }

                else {
                    questADBtns[currentQuest].ShowDown();
                    npcCtrl.dialogBundle.dialogText.text = "땡! 다시 처음부터 해볼까?";
                    restart.ShowUp();
                    
                    if(restart.isClicked)
                    {
                        if(restart.isAcceptQuest)
                        {
                            restart.ShowDown();
                            ResetQuest();                                                   //Reset Data
                        }

                        else
                        {
                            questClear = false;
                            restart.ShowDown();
                            break;
                        }
                    }
                }
            }

            yield return null;
        }

        questEnd = true;
        yield return null;
    }

    public void ResetQuest()
    {
        currentQuest = 0;

        foreach (QuestADBtn btn in questADBtns)
        {
            btn.ResetData();
            btn.ShowDown();
        }

        questEnd = false;
        questClear = false;

        restart.ShowDown();
        restart.ResetData();
    }
}
