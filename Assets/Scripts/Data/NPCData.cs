using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public ENPCData[] data;

    public NPCData(NPCData npcData)
    {
        if (npcData == null)
            return;

        data = new ENPCData[10];

        for(int i = 0; i < 10; i ++)
        {
            data[i] = new ENPCData(npcData.data[i]);
        }
    }
}
