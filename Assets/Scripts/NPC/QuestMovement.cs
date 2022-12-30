using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMovement : MonoBehaviour
{
    private Transform tr;

    [Header("Quest Movement")]
    public float yOffest;
    public float moveSpeed;

    private Vector3 upOffest;
    private Vector3 downOffest;

    private bool goUp;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        upOffest.x = tr.position.x;
        downOffest.x = tr.position.x;

        upOffest.y = tr.position.y + yOffest;
        downOffest.y = tr.position.y - yOffest;

        goUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(upOffest, tr.position) < 0.1f)
        {
            goUp = false;
        }
        else if(Vector3.Distance(downOffest, tr.position) < 0.1f) {
            goUp = true;
        }


        if(goUp)
        {
            tr.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else
        {
            tr.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }
}
