using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    [SerializeField] GameObject Panel = default;

    [SerializeField] GameObject HowTo1 = default;
    [SerializeField] GameObject HowTo1_2 = default;
    [SerializeField] GameObject HowTo2 = default;
    [SerializeField] GameObject HowTo3 = default;
    [SerializeField] GameObject HowTo4 = default;
    [SerializeField] GameObject HowTo5 = default;

    [SerializeField] GameObject HowToEND = default;

    GameObject StageManager;
    manager script;             // マネージャーのスクリプト

    GameObject SceneNavigatorObj;
    SceneNavigator Scenescript;

    bool flag;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(true);

        HowTo1.SetActive(true);
        HowTo1_2.SetActive(false);
        HowTo2.SetActive(false);
        HowTo3.SetActive(false);
        HowTo4.SetActive(false);
        HowTo5.SetActive(false);
        HowToEND.SetActive(false);

        //ステージマネージャーの取得
        StageManager = GameObject.Find("StageManager");
        //マネージャーが持っているmanagerスクリプト
        script = StageManager.GetComponent<manager>();

        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        Scenescript = SceneNavigatorObj.GetComponent<SceneNavigator>();

        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        int BubbleNum = script.GetBubble();

        if (BubbleNum == 0 && flag == false)
        {
            Panel.SetActive(true);
            HowToEND.SetActive(true);
            flag = true;
        }
    }

    public void OnBackStage()
    {
        Scenescript.SceneChange_Fade("SELECT STAGE",0.5f);
    }
    public void OnSet1_2()
    {
        HowTo1.SetActive(false);
        HowTo1_2.SetActive(true);
    }
    public void OnSet2()
    {
        HowTo1_2.SetActive(false);
        HowTo2.SetActive(true);
    }
    public void OnSet3()
    {
        HowTo2.SetActive(false);
        HowTo3.SetActive(true);
    }
    public void OnSet4()
    {
        HowTo3.SetActive(false);
        HowTo4.SetActive(true);
    }
    public void OnSet5()
    {
        HowTo4.SetActive(false);
        HowTo5.SetActive(true);
    }
    public void OnLast()
    {
        //パネル自体を非表示に
        Panel.SetActive(false);
    }
}
