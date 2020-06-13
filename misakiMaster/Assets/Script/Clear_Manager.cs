using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear_Manager : MonoBehaviour
{
    GameObject ClearSceneImage; // 自分のオブジェクト取得用変数
    public float CfadeStart = 1f; // フェード開始時間
    public bool CfadeIn = true; // trueの場合はフェードイン
    public float CfadeSpeed = 1f; // フェード速度指定

    GameObject SceneNavigatorObj;
    SceneNavigator script;

    // Start is called before the first frame update
    void Start()
    {
        ClearSceneImage = this.gameObject; // 自分のオブジェクト取得

        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        script = SceneNavigatorObj.GetComponent<SceneNavigator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CfadeStart > 0f)
        {
            CfadeStart -= Time.deltaTime;
        }
        else
        {
            if (CfadeIn)
            {
                CfadeInFunc();
            }
        }

        //if (Input.GetKey(KeyCode.Return))
        //{
        //    Debug.Log("前シーンにバック");
        //    script.SceneBack_Fade(0.5f);
        //}

    }

    void CfadeInFunc()
    {
        if (ClearSceneImage.GetComponent<Image>().color.a < 255)
        {
            UnityEngine.Color tmp = ClearSceneImage.GetComponent<Image>().color;
            tmp.a = tmp.a + (Time.deltaTime * CfadeSpeed);
            ClearSceneImage.GetComponent<Image>().color = tmp;
        }
    }

    public void OnClickStartButton()
    {
        Debug.Log("前シーンにバック");
        script.SceneChange_Fade("SELECT STAGE",0.5f);
    }
}
