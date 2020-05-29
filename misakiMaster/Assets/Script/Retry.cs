using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    public GameObject canvas = default; // 親にするキャンバス
    public GameObject panel = default;  // RetryPanelのPrefabを選択してぶちこむ
    public GameObject Score = default;  // スコアをRetry処理中は非表示にするために必要

    public bool GameOverFlag;

    // Start is called before the first frame update
    void Start()
    {
        // パネルの生成
        panel = (GameObject)Instantiate(panel);
        // 親子関係の生成
        panel.transform.SetParent(canvas.transform, false);

        GameOverFlag = false;

        Score.SetActive(true);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGameOverFlag()
    {
        GameOverFlag = true;
        Score.SetActive(false);
        panel.SetActive(true);
    }
}
