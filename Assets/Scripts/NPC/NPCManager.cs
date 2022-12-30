using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("[NPC Data List]")]
    public NPCData npcData;

    [Header("[Each NPC List]")]
    public List<NPCController> npc;

    [Header("[Dialog Preset]")]
    public GameObject dialogPreset;
    public GameObject qADBtn;

    [Header("[Quest UI]")]
    public QuestUI questUI;

    // Start is called before the first frame update
    void Awake()
    {
        Load();

        questUI = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<QuestUI>();

        if (npcData == null)
            CreateNewNPCData();

        for (int i = 0; i < 10; i++)
        {
            if (npc[i] != null)
            {
                npc[i].npcMgr = this;
                npc[i].npcData = npcData.data[i];           //Input Data
                npc[i].dialogPreset = dialogPreset;         //Input DialogPreset GameObject
                npc[i].AcceptDeclinedQuestBtn = qADBtn;     //Input AcceptDeclinedQuestBtn in NPC Object(controller)
                npc[i].npcIndexNum = i;                     //Input Index Num
                npc[i].questUI = questUI;                   //Input Quest UI 
            }
        }

        Debug.Log("NPC Data Load Done!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        SaveSystem.Save(npcData, "NPCData" + SceneInfo.saveFileNum + ".npcdata");
    }

    void Load()
    {
        npcData = SaveSystem.NPCDataLoad("NPCData" + SceneInfo.saveFileNum + ".npcdata");
    }

    void CreateNewNPCData()
    {
        npcData = new NPCData(null);
        npcData.data = new ENPCData[10];

        for (int i = 0; i < 10; i ++)
        { 
            npcData.data[i] = new ENPCData(null);
        }

        Save();
    }
}