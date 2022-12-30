using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 데이터 저장
[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int gender;          //0 = 남자, 1 = 여자
    public int money;
    public string sceneName;
    public string dateOfLssued;
    public string dateOfLast;

    public PlayerData(PlayerCtrl player)
    {
        position = new float[3];
        position[0] = player.tr.position.x;
        position[1] = player.tr.position.y;
        position[2] = player.tr.position.z;
    
        gender = player.gender;

        money = player.coin;
    }
}
