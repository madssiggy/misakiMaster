using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Text PopUp_Text1;
    public Text PopUp_Text2;

    [SerializeField] GameObject PopUpObj;
    public GameObject bronzMedal  = default;
    public GameObject silverMedal = default;
    public GameObject goldMedal   = default;

    Touch touchScript;
    TouchKaisuu touchKaisuuScript;

    // Start is called before the first frame update
    void Start()
    {
        PopUpObj = GameObject.Find("残り回数ポップアップ表示");
        PopUpObj.SetActive(false);

        touchScript = GameObject.Find("TouchManager").GetComponent<Touch>();
        touchKaisuuScript = GameObject.Find("NokorikaisuuText").GetComponent<TouchKaisuu>();

        bronzMedal.SetActive(false);
        silverMedal.SetActive(false);
        goldMedal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTouchKaisuu()
    {
        touchScript = GameObject.Find("TouchManager").GetComponent<Touch>();

        int kaisuu = touchKaisuuScript.GetOperate_zikken();
        int min = touchScript.touchNum;
        int operate = kaisuu - min;
        PopUp_Text1.text = "残り移動回数 " + (kaisuu - min) + " 回";
        PopUp_Text2.text = "現在獲得できる\n　　メダル";

        if (operate >= 8)
        {
            bronzMedal.SetActive(false);
            silverMedal.SetActive(false);
            goldMedal.SetActive(true);
        }
        else if (operate >= 5)
        {
            bronzMedal.SetActive(false);
            silverMedal.SetActive(true);
            goldMedal.SetActive(false);
        }
        else if (operate >= 3)
        {
            bronzMedal.SetActive(true);
            silverMedal.SetActive(false);
            goldMedal.SetActive(false);
        }
    }

    public void OnActive()
    {
        PopUpObj.SetActive(true);
    }

    public void OnBack()
    {
        PopUpObj.SetActive(false);
    }
}
