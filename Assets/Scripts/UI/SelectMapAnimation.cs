using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapAnimation : MonoBehaviour
{
    [Header("[Select Map Animation Move]")]
    public Transform movePos;
    public float moveSpeed;
    public float zoomSpeed;

    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        if (movePos == null)
            movePos = tr;
    }

    // Update is called once per frame
    void Update()
    {
        //Move pos
        tr.position = Vector3.Lerp(tr.position, movePos.position, moveSpeed * Time.deltaTime);
        
        //Zoom size
        tr.localScale = Vector3.Lerp(tr.localScale, movePos.localScale, zoomSpeed * Time.deltaTime);
    }
}
