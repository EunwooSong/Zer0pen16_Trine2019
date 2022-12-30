using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    public RectTransform recttr;
    
    [Header("-=Zoom Motion=-")]
    public bool isUseZoomMotion;        //줌 모션을 사용할 것인지
    public Vector3 zFirstScale;           //처음 위치
    public Vector3 zLaterScale;           //나중 위치
    public float zoomSpeed;             //줌 속도
    public bool zGoLate;                //시작은 FirstPos에서, 이 값이 참이면 Later로 거짓이면 First로 

    [Header("-=Smooth Motion=-")]
    public bool isUseSmoothMotion;      //부드러운 이동모션을 사용할 ㅅ것인지
    public Transform sFirstPos;
    public Transform sLaterPos;
    public float moveSpeed;             //이동 속도
    public bool sGoLate;                //시작은 FirstPos에서, 이 값이 참이면 Later로 거짓이면 First로 

    [Header("-=Blink Animation=-")]
    public bool isUseBlinkAnim;         //이 에니메이션을 사용할 것인지 확인 후 코루틴 실행(계속 반복)
    public Image target;                //UIAnimation 이므로 Image의 color의 a값을 바꿔줌으로써 반짝임 구현
    public bool isTurnOff;              //turnOff가 참이면 다시 투명도 증가시킴
    public float bDecStrength;

    //[Header("-=Vibration Animation=-")]
    //public bool isUseVibrationAnim;     //이 에니메이션을 사용할 것인지 확인 후 코루틴 실행 이는 while문이며 bool로 실행을 감지 (멈출때 까지 진행)
    //public float virStrength;           //0부터 이 수치만큼까지의의 진동 세기를을 랜덤으로 받아옴
    //public Vector3 vFirstPos;           //처음 위치 - 진동 종료후 처음 위치로 돌림 + 연산에 사용
    //public bool virAnimOn;              //진동 효과 키기(이 효과가 지속됨)

    //public bool isUseSmoothVibrationAim;//부드러운 진동을 사용할 것인지 확인 (단, 사용한다고 하였을때 딱 한번만 실행됨)
    //public float vDecStrength;           //감소 세기
    //public float currnetStrength;       //현재 세기 - 현재 세기가 0이면 virAnimOn 종료


    void Awake()
    {
        recttr = GetComponent<RectTransform>();     
    }

    // Start is called before the first frame update
    void Start()
    {
        zFirstScale += recttr.localScale;
        zLaterScale += recttr.localScale;

        //vFirstPos = rectr.position;

        //줌 모션
        if (isUseZoomMotion)
            recttr.localScale = zFirstScale;

        //부드러운 이동 모션
        if (isUseSmoothMotion)
            recttr.position = sFirstPos.position;

        if (sLaterPos == null)
            sLaterPos = recttr;

        //무한 깜박임
        if (isUseBlinkAnim)
        {
            target = GetComponent<Image>();
            StartCoroutine(BlinkAnim());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isUseZoomMotion)
        {
            ZoomMotion();
        }

        if(isUseSmoothMotion)
        {
            SmoothMotion();
        }
        
    }

    void ZoomMotion()
    {
        //줌 모션
        if (zGoLate)
        {
            recttr.localScale = Vector3.Lerp(recttr.localScale, zLaterScale, zoomSpeed * Time.smoothDeltaTime);
        }
        else
        {
            recttr.localScale = Vector3.Lerp(recttr.localScale, zFirstScale, zoomSpeed * Time.smoothDeltaTime);
        }
    }

    void SmoothMotion()
    {
        //이동 모션
        if (sGoLate)
        {
            recttr.position = Vector3.Lerp(recttr.position, sLaterPos.position, moveSpeed * Time.smoothDeltaTime);
        }
        else
        {
            recttr.position = Vector3.Lerp(recttr.position, sFirstPos.position, moveSpeed * Time.smoothDeltaTime);
        }
    }

    //부드러운 줌 모션 활성화/비활성화
    public void ZoomGoFirst()
    {
        zGoLate = false;
    }
    public void ZoomGoLater()
    {
        zGoLate = true;
    }

    //부드러운 이동 모션 활성화/비활성화
    public void SmoothGoFirst()
    {
        sGoLate = false;
    }
    public void SmoothGoLater()
    {
        sGoLate = true;
    }
    public void ResetSPos()
    {

    }

    //투명도를 이용하여 BlinkAnim 구현
    IEnumerator BlinkAnim()
    {
        float Opacity = 1;

        while(true)
        {
            if (!isTurnOff) {
                Opacity -= bDecStrength * Time.deltaTime;

                if (Opacity <= 0.0f)
                {
                    Opacity = 0.0f;
                    isTurnOff = true;
                }

                target.color = new Color(1, 1, 1, Opacity);
            }

            else {
                Opacity += bDecStrength * Time.deltaTime;

                if (Opacity >= 1.0f)
                {
                    Opacity = 1.0f;
                    isTurnOff = false;
                }

                target.color = new Color(1, 1, 1, Opacity);
            }

            yield return null;
        }
    }
}