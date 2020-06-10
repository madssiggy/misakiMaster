using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutInBoard : MonoBehaviour
{
    Text boardText;
    TouchKaisuu TouchCountScript;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        boardText = this.GetComponent<Text>();
        TouchCountScript = GameObject.Find("NokorikaisuuText").transform.GetComponent<TouchKaisuu>();
        boardText.text = "" + (TouchCountScript.Operate);
    }

    // Update is called once per frame
    void Update()
    {
        TouchCountScript = GameObject.Find("NokorikaisuuText").GetComponent<TouchKaisuu>();

        boardText.text =(TouchCountScript.Operate)+"回以内のタッチで\nクリアしよう";
    }
}
