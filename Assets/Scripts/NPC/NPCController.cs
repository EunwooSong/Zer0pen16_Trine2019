using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [HideInInspector]public NPCManager npcMgr;

    [Header("[Zoom Animation]")]
    public GameObject mainCamera;
    private Camera cam;
    float defaultZoomSize;
    public float maxZoomSize;
    public float zoomSpeed;

    [Header("[NPC Info]")]
    public QuestUI questUI;
    public ENPCData npcData;
    public Sprite profileImage;
    public int npcIndexNum;
    public string npcName;
    public EndingNPCQuest forEnding;

    [Header("[NPC Text(Dialog)]")]
    public GameObject dialogPreset;
    [HideInInspector]public DialogBundle dialogBundle;

    public List<string> isCanGetQuest;
    public List<string> isQuestProceeding;
    public List<string> isQuestDone;
    public List<string> isGetReward;
    public string decliendQuest;
    public string acceptQuest;

    public int currentNPCDialog;
    public int randNpcDialog;
    [SerializeField] bool wait;
    [SerializeField] int bugFixer;

    [Header("[Accept, Declined Quest Btn]")]
    public GameObject AcceptDeclinedQuestBtn;
    QuestADBtn adBtn;

    public bool isNowDialog;
    public bool dialogPresetActive;

    [Header("[Quest Done Condition]")]
    public GameObject questNotice;
    private SpriteRenderer questNoticeRender;
    public Sprite questDone;
    public Sprite questDoneYet;

    public int DoneMaxCondition;           //조건 완료

    [Header("[Reward Value]")]
    public int reward;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera"); //카메라 자동으로 불러옴
        cam = mainCamera.GetComponent<Camera>();
        defaultZoomSize = cam.orthographicSize;

        dialogBundle = dialogPreset.GetComponent<DialogBundle>();
        adBtn = AcceptDeclinedQuestBtn.GetComponent<QuestADBtn>();

        //nptice controller
        questNoticeRender = questNotice.GetComponent<SpriteRenderer>();
        QuestAlarmController();

        isNowDialog = false;
        dialogPresetActive = false;

        currentNPCDialog = 0;
        wait = false;

        if (npcIndexNum == 9)
            forEnding = GetComponent<EndingNPCQuest>();

        NPCFeaturesController();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNowDialog)
        {
            if (cam.orthographicSize != maxZoomSize)
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, maxZoomSize, Time.deltaTime * zoomSpeed);

            DialogController();
        }

        else
        {
            if (cam.orthographicSize != defaultZoomSize)
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, defaultZoomSize, Time.deltaTime * zoomSpeed);
        }

        

        QuestAlarmController();
        dialogPreset.SetActive(dialogPresetActive);
    }

    void NPCFeaturesController()
    {
        switch(npcIndexNum)
        {
            case 0:
                if (npcData.currentCondition >= DoneMaxCondition && !npcData.isGetReward)
                    if(!npcMgr.npcData.data[2].isCanGetQuest)
                        if(!npcMgr.npcData.data[3].isCanGetQuest)
                        {
                            npcData.isQuestProceeding = false;
                            npcData.isQuestDone = true;
                        }
                break;

            case 1:
                npcData.isCanGetQuest = false;
                npcData.isGetReward = true;

                if (npcMgr.npcData.data[0].isQuestProceeding)                      //대화걸기
                {
                    if (isNowDialog && npcMgr.npcData.data[0].currentCondition < 1)
                    {
                        npcMgr.npcData.data[0].currentCondition++;
                    }
                }

                randNpcDialog = (int)Random.Range(0, isGetReward.Count);
                break; 

            //Cafe
            case 2:
                if (npcData.currentCondition >= DoneMaxCondition && !npcData.isGetReward)
                {
                    npcData.isQuestProceeding = false;
                    npcData.isQuestDone = true;
                }

                if (npcMgr.npcData.data[3].isQuestProceeding)                       //꽃 배달 이벤트
                {
                    if (isNowDialog && npcMgr.npcData.data[3].currentCondition < 1)
                    {
                        npcMgr.npcData.data[3].currentCondition++; 
                    }
                }

                if (npcMgr.npcData.data[5].isQuestProceeding)                       //물건 배달 이벤트
                {
                    if (isNowDialog && npcMgr.npcData.data[5].currentCondition < 1)
                    {
                        npcMgr.npcData.data[5].currentCondition++;
                    }
                }

                break;

            case 3:
                if (npcData.currentCondition >= DoneMaxCondition && !npcData.isGetReward)
                {
                    npcData.isQuestProceeding = false;
                    npcData.isQuestDone = true;
                }

                if (npcMgr.npcData.data[2].isQuestProceeding && !npcData.isQuestProceeding)     //커피 배달 이벤트
                {
                    npcData.isCanGetQuest = true;
                    npcData.isGetReward = false;

                    if (isNowDialog && npcMgr.npcData.data[2].currentCondition < 1)
                    {
                        npcMgr.npcData.data[2].currentCondition++;
                    }
                }

                else if(!npcData.isQuestProceeding)
                {
                    npcData.isCanGetQuest = false;
                    npcData.isGetReward = true;
                }
                break;

            case 4:
                if (npcData.currentCondition >= DoneMaxCondition && !npcData.isGetReward)       
                {
                    npcData.isQuestProceeding = false;
                    npcData.isQuestDone = true;
                }

                break;
        
            //Shop
            case 5:
                if (npcData.currentCondition >= DoneMaxCondition && !npcData.isGetReward)
                {
                    npcData.isQuestProceeding = false;
                    npcData.isQuestDone = true;
                }

                if (npcMgr.npcData.data[4].isQuestProceeding)                       //물감 배달? 이벤트
                {
                    if (isNowDialog && npcMgr.npcData.data[4].currentCondition < 1)
                    {
                        npcMgr.npcData.data[4].currentCondition++;
                    }
                }

                break;
           
            case 6:
                //전도사? 필요 X
                break;
            case 7:
                //엄마
                if (npcData.currentCondition >= DoneMaxCondition && !npcData.isGetReward)
                {
                    npcData.isQuestProceeding = false;
                    npcData.isQuestDone = true;
                }
                

                break;
            case 8:
                if (npcMgr.npcData.data[7].isQuestProceeding || npcMgr.npcData.data[7].isGetReward)
                {
                    npcData.isQuestDone = true;

                    if (isNowDialog && npcMgr.npcData.data[7].currentCondition < 1)
                    {
                        npcMgr.npcData.data[7].currentCondition++;
                    }
                }
                else
                {
                    npcData.isCanGetQuest = false;
                    npcData.isGetReward = true;
                }
                break;
            case 9:
                if(!npcData.isGetReward)
                {
                    npcData.isCanGetQuest = true;
                }
                break;
        }
    }

    void DialogController()
    {
        //퀘스트 줄 때
        if (npcData.isCanGetQuest)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentNPCDialog < isCanGetQuest.Count - 1)
            {
                currentNPCDialog++;
            }

            if(!wait)
                dialogBundle.dialogText.text = isCanGetQuest[currentNPCDialog];
        }

        //퀘스트 중 일 때 (isQuestProceeding)
        else if (npcData.isQuestProceeding)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentNPCDialog < isQuestProceeding.Count - 1)
            {
                currentNPCDialog++;
            }
                
            if (!wait)
                dialogBundle.dialogText.text = isQuestProceeding[currentNPCDialog];
        }

        //퀘스트 완료, 보상 받을 때 (isQuestDone)
        else if (npcData.isQuestDone)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentNPCDialog < isQuestDone.Count - 1)
            {
                currentNPCDialog++;
            }

            if (!wait)
                dialogBundle.dialogText.text = isQuestDone[currentNPCDialog];
        }

        //보상 지급 완료, 퀘스트 X (isGetReward)
        else if (npcData.isGetReward)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentNPCDialog < isGetReward.Count - 1)
            {
                currentNPCDialog++;
            }

            if (!wait)
                dialogBundle.dialogText.text = isGetReward[currentNPCDialog];

            //case로 일부 인덱스만 적용
            switch(npcIndexNum) {
                //case n:
                case 1:
                    dialogBundle.dialogText.text = isGetReward[randNpcDialog];
                    currentNPCDialog = 0;
                    break;
                case 2:
                    if (npcMgr.npcData.data[3].isQuestProceeding)
                    {
                        dialogBundle.dialogText.text = "와! 꽃 배달 정말 고마워요!";
                        wait = true;
                        currentNPCDialog = isGetReward.Count -1;
                    }

                    if(npcMgr.npcData.data[5].isQuestProceeding)
                    {
                        dialogBundle.dialogText.text = "프라하에서 파리까지...! 정말 고마워요!";
                        wait = true;
                        currentNPCDialog = isGetReward.Count - 1;
                    }
                    break;
            }
        }

        if(npcIndexNum == 5)                                        //물감 배달 이벤트
            if(npcMgr.npcData.data[4].isQuestProceeding && !npcData.isCanGetQuest && !npcData.isQuestDone)
            {
                dialogBundle.dialogText.text = "물감이요? 여기 있어요. 돈은 나중에 성희한테 받을게요.";
                wait = true;

                if (npcData.isQuestProceeding)
                    currentNPCDialog = isQuestProceeding.Count - 1;

                if (npcData.isGetReward)
                    currentNPCDialog = isGetReward.Count - 1;
            }

        dialogBundle.ProfileImage.sprite = profileImage;
        dialogBundle.npcNameText.text = npcName;

        //Btn Controller
        if (isCanGetQuest.Count == currentNPCDialog + 1 && npcData.isCanGetQuest)
        {
            adBtn.ShowUp();

            if (adBtn.isClicked)
                if (adBtn.isAcceptQuest)
                {
                    dialogBundle.dialogText.text = acceptQuest;
                    adBtn.ShowDown();

                    //For Proceeding Ending NPC
                    if (npcIndexNum == 9)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                            forEnding.StartCoroutine(forEnding.StartNPCQuest());

                        if (forEnding.questEnd)
                        {
                            if (forEnding.questClear)
                            {
                                npcData.isCanGetQuest = false;
                                npcData.isGetReward = true;
                                currentNPCDialog = 0;

                                forEnding.ResetQuest();
                                DialogController();
                                return;
                            }
                            else
                            {
                                DialogEnd();
                                npcData.isCanGetQuest = true;
                                npcData.isQuestProceeding = false;

                                forEnding.ResetQuest();
                                return;
                            }
                        }

                        else
                            return;
                    }

                    wait = true;
                    npcData.isQuestProceeding = true;

                    if(npcIndexNum == 6)
                    {
                        npcData.isQuestProceeding = false;
                        if(!npcData.isGetReward)
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>().coin += reward;

                        npcData.isGetReward = true;
                    }
                }

                else
                {
                    adBtn.ShowDown();
                    dialogBundle.dialogText.text = decliendQuest;

                    wait = true;
                }
        }

        //Camera Controller
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (npcData.isCanGetQuest)
            {
                if (currentNPCDialog + 1 != isCanGetQuest.Count || !wait)
                    return;

                //if(npcData.isQuestProceeding)
                //    npcData.isCanGetQuest = false;
            }
            else if (npcData.isQuestProceeding)
            {
                if (currentNPCDialog + 1 != isQuestProceeding.Count)
                    return;
            }
            else if (npcData.isQuestDone)
            {
                if (currentNPCDialog + 1 != isQuestDone.Count)
                    return;
                if(npcIndexNum == 8)
                {
                    DialogEnd();
                    return;
                }

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>().coin += reward;
                npcData.isGetReward = true;
                npcData.isQuestDone = false;
                wait = true;
            }

            else if (npcData.isGetReward)
            {
                switch(npcIndexNum)
                {
                    case 1: DialogEnd(); return;
                }

                if (currentNPCDialog + 1 != isGetReward.Count)
                    return;
            }

            DialogEnd();
        }
    }

    public void DialogStart()
    {
        //대화 시작
        if (dialogPresetActive)
            return;

        Debug.Log("Dialog Start - " + npcName);

        string Error_Fix = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        //Error Fix / 2019.09.04
        if (Error_Fix.Equals("Stage_1") || Error_Fix.Equals("Florist") || Error_Fix.Equals("Shop")
           || Error_Fix.Equals("Hut_1") || Error_Fix.Equals("Stage_4"))
            currentNPCDialog = -1;
        else
            currentNPCDialog = 0;

        isNowDialog = true;
        dialogPresetActive = true;

        mainCamera.GetComponent<FollowCam>().isPosYLock = false;

        NPCFeaturesController();
    }

    public void DialogEnd()
    {
        Debug.Log("Dialog End - " + npcName);

        isNowDialog = false;
        dialogPresetActive = false;
        currentNPCDialog = 0;
        
        wait = false;

        if (adBtn.isAcceptQuest)
            npcData.isCanGetQuest = false;

        adBtn.ResetData();
        adBtn.ShowDown();

        QuestAlarmController();
        mainCamera.GetComponent<FollowCam>().isPosYLock = true;

        bugFixer = 1;

        questUI.CreatQuest();       //퀘스트 창에 카드 제작
    }

    void QuestAlarmController()
    {
        if (npcData.isCanGetQuest)
        {
            questNoticeRender.sprite = questDoneYet;
            questNotice.SetActive(true);
        }

        else if (npcData.isQuestDone)
        {
            questNoticeRender.sprite = questDone;
            questNotice.SetActive(true);
        }

        else if (npcData.isGetReward)
            questNotice.SetActive(false);

        else
            questNotice.SetActive(false);
    }
}