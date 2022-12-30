using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipManager : MonoBehaviour
{
    [Header("[Tip Text]")]
    public Text tipTextObject;
    public List<string> tips;

    // Start is called before the first frame update
    void Start()
    {
        int test = (int)Random.Range(0, tips.Count - 0.1f);

        Debug.Log("Tip Num : " + test);

        tipTextObject.text = tips[test];
        
    }
}
