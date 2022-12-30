using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    Transform tr;

    [Header("[Center Transform]")]
    public Transform center;
    public float releaseRes = 1;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tr.position = Vector3.Lerp(tr.position, mousePos*releaseRes + center.position, 3.0f * Time.deltaTime);
        //Debug.Log("범위? : " + Input.mousePosition / 100 * 20);
    }
}
