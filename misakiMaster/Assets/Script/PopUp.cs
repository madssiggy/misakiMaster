using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Text PopUp_Text1;
    public Text PopUp_Text2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTouchKaisuu()
    {
        int kaisuu = TouchKaisuu.GetOperate();
        PopUp_Text1.text = "残り移動回数　" + kaisuu + "　回";
        PopUp_Text2.text = "現在獲得できるメダル";

        GameObject MedalImage = GameObject.Find("medal");

    }
}
