using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Text PopUp_Text1;
    public Text PopUp_Text2;

    [SerializeField] GameObject PopUpObj;

    // Start is called before the first frame update
    void Start()
    {
        PopUpObj = GameObject.Find("残り回数ポップアップ表示");
        PopUpObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTouchKaisuu()
    {
        int kaisuu = TouchKaisuu.GetOperate();
        PopUp_Text1.text = "残り移動回数　" + kaisuu + "　回";
        PopUp_Text2.text = "現在獲得できる\n　　メダル";

        GameObject MedalImage = GameObject.Find("medal");
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
