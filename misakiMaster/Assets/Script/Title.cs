using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    // シーン管理マネージャの取得
    GameObject SceneNavigatorObj;
    SceneNavigator script;

    // Start is called before the first frame update
    void Start()
    {
        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        script = SceneNavigatorObj.GetComponent<SceneNavigator>();
    }

    // Update is called once per frame
    void Update()
    {
        // タッチしたら遷移
        if (Input.GetMouseButtonDown(0))
        {
            script.SceneChange_Fade("SELECT STAGE",1.0f);
        }

    }
}
