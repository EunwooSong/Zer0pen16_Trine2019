using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hot_AirBalloon : MonoBehaviour
{
    [Header("[Hot-Air Balloon GameObjects]")]
    public List<Transform> balloons;

    [System.Serializable]
    public class Values
    {
        public Transform tr;
        public Vector3 moveDir;
        public Vector3 myUpOffset;
        public Vector3 myDownOffset;
        public bool goUp;
        public bool speedChange;
        public float speed;

        public void checkUpDown(Vector3 myPos)
        {
            if(Vector3.Distance(myPos, myUpOffset) < 0.1f)
            {
                goUp = false;
                speedChange = true;
            }
            else if(Vector3.Distance(myPos, myDownOffset) < 0.1f) {
                goUp = true;
                speedChange = true;
            }
        }

        public void RandSpeed(float minSpeed, float maxSpeed)
        {
            speed = Random.Range(minSpeed, maxSpeed);
            speedChange = false;
        }

        public void Move()
        {
            if(goUp)
            {
                tr.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                //tr.position = Vector3.Lerp(tr.position, myDownOffset, speed * Time.smoothDeltaTime);
                tr.Translate(Vector3.down * speed * Time.deltaTime);
            }

            checkUpDown(tr.position);
        }
    }
    public Values[] balloonValues; //열기구의 정보 저장

    [Header("[Balloons Speed]")]
    public float minSpeed;
    public float maxSpeed;

    [Header("[Balloons Up/Down Offset]")]
    public Vector3 upOffset;
    public Vector3 downOffset;

    void Start() {
        
        bool tmp = false;

        balloonValues = new Values[balloons.Count];

        for (int i = 0; i < balloons.Count; i++)
        {
            balloonValues[i] = new Values();

            balloonValues[i].myUpOffset = balloons[i].position + upOffset;
            balloonValues[i].myDownOffset = balloons[i].position + downOffset;
            balloonValues[i].tr = balloons[i];
            
            balloonValues[i].RandSpeed(minSpeed, maxSpeed);
            tmp = !tmp;
            balloonValues[i].goUp = tmp;
        }
    }

    void Update()
    {
        for (int i = 0; i < balloons.Count; i++)
        {
            balloonValues[i].Move();

            if (balloonValues[i].speedChange)
                balloonValues[i].RandSpeed(minSpeed, maxSpeed);     //속도 재설정
        }
    }
}
