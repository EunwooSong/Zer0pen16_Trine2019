using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterManger : MonoBehaviour
{
    [Header("Girl or Man")]
    public GameObject girl;
    public GameObject man;
    private Image girlImage;
    private Image manImage;

    [Header("Spotlight")]
    public GameObject spot;
    public Vector3 lightOffset;
    private Transform spotTr;

    [Header("Light Turn On/Off Sprite")]
    public Sprite girlOn;
    public Sprite girlOff;
    public Sprite manOn;
    public Sprite manOff;

    private void Start()
    {
        girlImage = girl.GetComponent<Image>();
        manImage = man.GetComponent<Image>();
        spotTr = spot.GetComponent<Transform>();

        girlImage.sprite = girlOff;
        manImage.sprite = manOff;

        spot.SetActive(false);
    }

    public void GirlLightON()
    {
        girlImage.sprite = girlOn;

        spotTr.position = girl.GetComponent<Transform>().position + lightOffset;
        spot.SetActive(true);
    }

    public void ManLightON()
    {
        manImage.sprite = manOn;

        spotTr.position = man.GetComponent<Transform>().position + lightOffset;
        spot.SetActive(true);
    }

    public void LightOff()
    {
        girlImage.sprite = girlOff;
        manImage.sprite = manOff;

        spot.SetActive(false);
    }

    //Select UI Ctrl
}
