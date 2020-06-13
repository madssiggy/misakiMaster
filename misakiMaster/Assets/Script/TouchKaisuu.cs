using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchKaisuu : MonoBehaviour
{
    // タッチ状態管理Managerの読み込み
    GameObject RetryPanelMakeObj = default;
    RetryPanelMake RetryPanelMakeScript = default;

    Text boardText;
    Touch touchScript;
    public int Operate;
    private int Operate_kawaru;
    public static int operate;  //ゲッター用

    GameObject StageManager;    // 各値が入ってるマネージャーを呼び出す
    manager script;             // マネージャーのスクリプト

    public GameObject ClearCanvas;
    public int bubblenum;

    // Start is called before the first frame update
    void Start()
    {
        //ステージマネージャーの取得
        StageManager = GameObject.Find("StageManager");
        //マネージャーが持っているmanagerスクリプト
        script = StageManager.GetComponent<manager>();

        RetryPanelMakeObj = GameObject.Find("RetryPanelMake");
        RetryPanelMakeScript = RetryPanelMakeObj.GetComponent<RetryPanelMake>();

        boardText = this.GetComponent<Text>();
        touchScript = GameObject.Find("TouchManager").GetComponent<Touch>();



        Operate_kawaru = Operate;
    }

    // Update is called once per frame
    void Update()
    {
        touchScript = GameObject.Find("TouchManager").GetComponent<Touch>();
  
        boardText.text = ""+(Operate - touchScript.touchNum);

        //Operate_kawaruの中身を元々の移動回数でリセット
        Operate_kawaru = Operate;

        //元々の移動回数からtouchNumを引いた残り移動回数をOperate_kawaruに代入
        Operate_kawaru -= touchScript.touchNum;

        script.CheckBubble();
        bubblenum = script.GetBubble();
        //泡の回数が0になったらクリア
        if (bubblenum == 0)
        {
            ClearCanvas.SetActive(true);
        }
        //残り移動回数が0ならゲームオーバー
        else if (Operate_kawaru == 0)
        {
            RetryPanelMakeScript.SetGameOverFlag();
        }
        else
        {
            Operate_kawaru = Operate;
        }
        operate = Operate - touchScript.touchNum;
    }

    //げったーロボ
    public static int GetOperate()
    {
        return operate;
    }
    public int GetOperate_zikken()
    {
        return Operate;
    }
}
