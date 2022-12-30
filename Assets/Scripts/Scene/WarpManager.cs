using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    [Header("[Paris Warp Manager]")]
    public bool isParis;
    public Transform bakery;
    public Transform cafe;
    public Transform florist;

    [Header("[Swiss Warp Manager]")]
    public bool isSwiss;
    public Transform hut_1;

    [Header("[Paris Warp Manager]")]
    public bool isHungary;
    public Transform shop;
    public Transform house;

    private PlayerCtrl player;
    //스위스, 헝가리, ....

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();

        switch(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "Stage_1": isParis = true; break;
            case "Stage_2": isSwiss = true; break;
            case "Stage_3": isHungary = true; break;
        }

        switch (player.data.sceneName)
        {
            case "Bakery":  if(isParis) player.tr.position = bakery.position; break;
            case "Cafe":    if (isParis) player.tr.position = cafe.position; break;
            case "Florist": if (isParis) player.tr.position = florist.position; break;

            case "Hut_1": if(isSwiss) player.tr.position = hut_1.position; break;

            case "House": if(isHungary) player.tr.position = house.position; break;
            case "Shop":  if(isHungary) player.tr.position = shop.position; break;

            default: break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
