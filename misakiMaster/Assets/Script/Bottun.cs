using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottun : MonoBehaviour
{
    public bool ClickSide;//左右どちらかを判定する。L=false,R=True
    public bool isClicked;
    manager script;
    GameObject FieldObject;
    field FieldScript;

    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
        script = GameObject.Find("StageManager").GetComponent<manager>();

        FieldObject = GameObject.Find("FieldCenter");
        FieldScript = FieldObject.GetComponent<field>();
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
        if (ClickSide == false && script.isCamera == false && script.isRotate == false) {
            //trueで左回転
            script.SetTop(script.nowTop, true);
            StartCoroutine(FieldScript.RollYL());
        }

        //==============================
        //　Y軸で右にステージが90度傾く
        //==============================
        if (ClickSide == true && script.isCamera == false && script.isRotate == false) {
            //falseで右回転
            script.SetTop(script.nowTop, false);
            StartCoroutine(FieldScript.RollYR());
        }


    }
    public void SetisClicked(bool check)
    {
        isClicked = check;
    }
}
