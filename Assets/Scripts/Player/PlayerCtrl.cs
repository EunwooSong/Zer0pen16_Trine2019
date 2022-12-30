using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerCtrl : MonoBehaviour
{
    public int gender;
    public int coin;
    [HideInInspector] public PlayerData data;
    [HideInInspector] public Transform tmp;
    [HideInInspector] public Transform startPoint;
    [Header("[UI]")]
    public Text coinText;

    [Header("[Movement]")]
    public Rigidbody2D rigid;
    public float moveSpeed;
    public float moveDir;
    public Transform tr;
    private float xScale;

    [Header("[Animation]")]
    public Animator anim;

    [Header("[Warp Data]")]
    [SerializeField] private WarpData currentWarpData;

    [Header("[Walk Audio]")]
    public AudioSource audioSource;
    public AudioClip walkClip;

    //is cna npc talk
    [SerializeField] private NPCController npcCtrl;
    [SerializeField] private bool isCanTalkToNPC;
    [SerializeField] private bool isNowTalkToNPC;
    [HideInInspector]public bool stop;
    
    //public List<AnimationC>

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        startPoint = GameObject.FindGameObjectWithTag("StartPoint").GetComponent<Transform>();

        isCanTalkToNPC = false;
        isNowTalkToNPC = false;
        stop = false;

        //Audio Load
        audioSource = GetComponent<AudioSource>();
        walkClip = Resources.Load<AudioClip>("Audio/Walk");
        audioSource.clip = walkClip;
        audioSource.Stop();

        xScale = tr.localScale.x; 

        Load();

        if (!data.sceneName.Equals(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
        {
            tr.position = startPoint.position;
            Debug.Log("Set StartPoint Position!");
        }

        anim.SetInteger("gender", gender);
    }

    void Update()
    {
        //Movement
        if (!isNowTalkToNPC)
            PlayerMove();
        else
            audioSource.Stop();

        //Animation
        AnimationController();
        //Warp
        WarpController();
        //NPC
        NPCCtrl();

        coinText.text = "X" + coin;
    }

    void PlayerMove()
    {
        moveDir = Input.GetAxis("Horizontal");

        if (stop)
            moveDir = 0.0f;

        rigid.velocity = new Vector2(moveDir * moveSpeed, rigid.velocity.y);

        if (!audioSource.isPlaying && moveDir != 0.0f)
            audioSource.Play();

        else if(moveDir == 0.0f)
            audioSource.Stop();
    }

    void AnimationController()
    {
        if (moveDir > 0.1f)
        {
            tr.localScale = new Vector3(xScale, tr.localScale.y, tr.localScale.z);
            anim.SetBool("isWalk", true);
        }
        else if (moveDir < -0.1f)
        {
            tr.localScale = new Vector3(-1 * xScale, tr.localScale.y);
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    void WarpController()
    {
        if (currentWarpData != null)
            if (Input.GetKeyDown(KeyCode.Space))
                currentWarpData.Enter();
    }

    void NPCCtrl()
    {
        if (npcCtrl == null)
            return;

        if (isCanTalkToNPC)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isNowTalkToNPC = true;
                npcCtrl.DialogStart();

                rigid.velocity = Vector3.zero;
                moveDir = 0.0f;
            }
        }

        if (!npcCtrl.dialogPresetActive)
        {
            isNowTalkToNPC = false;
        }
    }

    public void Save()
    {
        SaveSystem.Save(this, "playerData" + SceneInfo.saveFileNum + ".playerData");
    }

    public void PlayerStop()
    {
        stop = true;
    }

    public void PlayerStop_D()
    {
        stop = false;
    }

    public void Load()
    {
        data = SaveSystem.PlayerDataLoad("playerData" + SceneInfo.saveFileNum + ".playerData");

        //위치 불러옴
        Vector3 pos;
        pos.x = data.position[0];
        pos.y = data.position[1];
        pos.z = data.position[2];
        tr.position = pos;

        //성별 불러옴
        gender = data.gender;
        //Load Coin
        coin = data.money;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Stage_1
        if (coll.gameObject.name.Equals("Bakery"))
        {
            Debug.Log("Can Enter Bakery Stage!");
        }

        if (coll.gameObject.tag.Equals("NPC"))
        {
            isCanTalkToNPC = true;
            npcCtrl = coll.GetComponent<NPCController>();
            Debug.Log("Can Talk To NPC");
        }
           

        if (coll.GetComponent<WarpData>() != null)
            currentWarpData = coll.GetComponent<WarpData>();
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("Can not Enter Warp!");
        currentWarpData = null;

        if(coll.gameObject.tag.Equals("NPC"))
        {
            isCanTalkToNPC = false;
        }
    }
}
