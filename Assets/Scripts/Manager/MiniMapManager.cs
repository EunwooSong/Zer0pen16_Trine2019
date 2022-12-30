using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    [Header("[Country Info]")]
    public Image countryFlag;
    public Text countryName;

    public List<Sprite> countryFlags;

    [Header("[Size Ctrl]")]
    public GameObject smallSize;
    public GameObject bigSize;

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        sceneName = GetStageName(sceneName);

        countryName.text = sceneName;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            bigSize.SetActive(!bigSize.activeSelf);
        }

        if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
            bigSize.SetActive(false);
    }

    public string GetStageName(string sceneName)
    {
        string tmp = null;

        switch(sceneName)
        {
            case "Bakery":
            case "Cafe":
            case "Florist":
            case "Stage_1":
                tmp = "프랑스-파리";
                countryFlag.sprite = countryFlags[0];
                break;

            case "Hut_1":
            case "Stage_2":
                tmp = "스위스-인터라켄";
                countryFlag.sprite = countryFlags[1];
                break;

            case "Shop":
            case "House":
            case "Stage_3":
                tmp = "헝가리-프라하";
                countryFlag.sprite = countryFlags[2];
                break;

            case "Stage_4":
                tmp = "체코-부다페스트";
                countryFlag.sprite = countryFlags[3];
                break;
        }

        return tmp;
    }

    public void MapSizeUp()
    {
        bigSize.SetActive(true);
    }

    public void MapSizeDown()
    {
        bigSize.SetActive(false);
    }
}
