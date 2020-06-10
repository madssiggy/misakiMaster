using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryPanelMake : MonoBehaviour
{
    public GameObject canvas = default; // 親にするキャンバス
    public GameObject panel = default;  // RetryPanelのPrefabを選択してぶちこむ
    public GameObject panel_else = default;  // スコアなどのUIをRetry処理中は非表示にするために必要

    public bool GameOverFlag;

    void Start()
    {
        // パネルの生成
        panel = (GameObject)Instantiate(panel);

        // 生成したパネルとキャンバスの親子関係を設定（親→キャンバス、子→パネル）
        panel.transform.SetParent(canvas.transform, false);

        GameOverFlag = false;   // ゲームオーバーフラグの初期化

        panel_else.SetActive(true);  // スコアなどの（Canvasの子の）UIパネルがもし表示されてなければ表示するようにする
        panel.SetActive(false); // パネルを非表示にする（ゲームオーバーになるまで非表示
    }

    // Update is called once per frame
    void Update()
    {
    }

    //================================================================================
    // ゲームオーバーフラグをtrueにし、RETRY画面を表示する（カウントダウンも開始する）
    //================================================================================
    public void SetGameOverFlag()
    {
        GameOverFlag = true;
        panel_else.SetActive(false);
        panel.SetActive(true);
    }
    public bool GetGameOverFlag()
    {
        return GameOverFlag;
    }
}
