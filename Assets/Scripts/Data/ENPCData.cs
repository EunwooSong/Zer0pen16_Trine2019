using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ENPCData
{
    public bool isCanGetQuest;     //퀘스트를 받을 수 있는지
    public bool isQuestProceeding; //퀘스트가 진행 중인지
    public bool isQuestDone;       //퀘스트가 완료되었는지
    public bool isGetReward;       //퀘스트에 대한 보상을 받았는지
    public int currentCondition;  //퀘스트 진행도

    public ENPCData(ENPCData data)
    {
        if (data == null)
        {
            isCanGetQuest = true;
            isQuestProceeding = false;
            isQuestDone = false;
            isGetReward = false;
            currentCondition = 0;

            return;
        }

        isCanGetQuest = data.isCanGetQuest;
        isQuestProceeding = data.isQuestProceeding;
        isQuestDone = data.isQuestDone;
        isGetReward = data.isGetReward;
        currentCondition = data.currentCondition;
    }
}
