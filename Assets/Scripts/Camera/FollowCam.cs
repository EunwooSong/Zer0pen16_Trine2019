using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("[Target]")]
    public Transform target;
    public float smoothSpeed;
    private PlayerCtrl player;

    [Header("[First, Last Pos]")]
    public Vector3 first;
    public Vector3 last;

    public float offset;

    [Header("[Y Pos Lock]")]
    public bool isPosYLock;
    public float yPos;

    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
        tr = GetComponent<Transform>();

        if(isPosYLock)
        {
            first.y = yPos;
            last.y = yPos;
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }

    void LateUpdate()
    {
        Vector3 movePos = new Vector3();

        if (player.moveDir == 0.0f)
            movePos = Vector3.Lerp(tr.position, player.tr.position, smoothSpeed * Time.deltaTime * 0.5f);
        else
            movePos = Vector3.Lerp(tr.position, target.position, smoothSpeed * Time.deltaTime);

        if (isPosYLock)
            movePos.y = yPos;
        movePos.z = -20;

        if (target.position.x  < first.x + offset || player.tr.position.x < first.x + offset || movePos.x < first.x + offset)
            tr.position = Vector3.Lerp(tr.position, first, smoothSpeed * Time.deltaTime * 0.5f);

        else if (target.position.x > last.x - offset || player.tr.position.x > last.x - offset || movePos.x >last.x - offset)
            tr.position = Vector3.Lerp(tr.position, last, smoothSpeed * Time.deltaTime * 0.5f);
        else
            tr.position = movePos;
    }
}
