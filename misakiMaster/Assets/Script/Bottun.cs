using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottun : MonoBehaviour
{
    public bool ClickSide;//左右どちらかを判定する。L=false,R=True
    public bool isClicked;
    manager script;
    //GameObject FieldObject;
    //field FieldScript;

    GameObject cameraObject;
    cameraControl cameraScript;

    // シーン管理マネージャの取得
    GameObject SceneNavigatorObj;
    SceneNavigator scene_script;

    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
        script = GameObject.Find("StageManager").GetComponent<manager>();

        //Field
        cameraObject = GameObject.Find("Main Camera");//("FieldCenter");
                                                      //Field
        cameraScript = cameraObject.GetComponent<cameraControl>();

        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        scene_script = SceneNavigatorObj.GetComponent<SceneNavigator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        isClicked = true;
        Debug.Log("押された");

        //===============================
        //　Y軸で左にステージが90度傾く
        //===============================
        if (ClickSide == false && script.isCamera == false && script.isRotate == false
         ) {
            //trueで左回転

            StartCoroutine(cameraScript.RollYR());
        }

        //==============================
        //　Y軸で右にステージが90度傾く
        //==============================
        if (ClickSide == true && script.isCamera == false && script.isRotate == false
            ) {
            //falseで右回転
   
            StartCoroutine(cameraScript.RollYL());
        }

    }
    public void SetisClicked(bool check)
    {
        isClicked = check;
    }
    //　シーンの再読み込み(呼び出すだけでハイ、Reload）
    public void OnReload()
    {
        scene_script.SceneReload(1.0f);
    }
    public void DestroyUI()
    {
        Destroy(this.gameObject);
    }
}
