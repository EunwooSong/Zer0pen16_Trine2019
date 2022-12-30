using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [Header("[Quest Info]")]
    public RectTransform parent;
    [HideInInspector]public GameObject questPrefab;
    [HideInInspector]public NPCManager npcManager;

    public GameObject[] questCardGroup;
    private int questCardCount;

    [System.Serializable]
    public class QuestInfo
    {
        public Sprite npcProfile;
        public string npcName;
        public string npcQuest;
    }
    public QuestInfo[] questInfo;

    // Start is called before the first frame update
    void Start()
    {
        questPrefab = Resources.Load("Prefabs/Quest_Group") as GameObject;

        questCardCount = 0;
        questCardGroup = new GameObject[10];

        npcManager = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<NPCManager>();
        CreatQuest();

        questInfo[4].npcQuest = "상점에서 물감을 사다주자!";
        gameObject.SetActive(false);
    }

    public void CreatQuest()
    {
        RemoveQuestCard();

        for(int i = 0; i < 9; i ++)
        {
            if(npcManager.npcData.data[i].isQuestProceeding)
            {
                GameObject _questCard = Instantiate(questPrefab) as GameObject;
                QuestInfoPreset _questInfo = _questCard.GetComponent<QuestInfoPreset>();

                _questInfo.npcProfile.sprite = questInfo[i].npcProfile;
                _questInfo.npcName.text = questInfo[i].npcName;
                _questInfo.npcQuest.text = questInfo[i].npcQuest;

                _questCard.transform.parent = parent;
                _questCard.transform.localScale = new Vector2(1.0f, 1.0f);

                questCardGroup[questCardCount++] = _questCard;
            }
        }
    }

    void RemoveQuestCard()
    {
        for (int i = 0; i < questCardCount; i++)
        {
            if (questCardGroup[i] == null)
                continue;

            Destroy(questCardGroup[i]);
        }  

        questCardCount = 0;
    }
}
