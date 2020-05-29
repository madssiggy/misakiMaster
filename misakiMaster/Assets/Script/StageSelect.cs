using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    // シーン管理マネージャの取得
    GameObject SceneNavigatorObj;
    SceneNavigator script;

    [SerializeField] GameObject menuPanel = default;

    [SerializeField] GameObject HAZIMARINOUMI = default;
    [SerializeField] GameObject SEIREINOMORI = default;
    [SerializeField] GameObject KARAPPONOKAZAN = default;

    // Start is called before the first frame update
    void Start()
    {
        //BackToMenuメソッドを呼び出す
        InitStageChoice();

        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        script = SceneNavigatorObj.GetComponent<SceneNavigator>();
    }


    // ステージセレクト画面１ではじまりのうみステージを選択した場合
    public void StageChoice_HAZIMARINOUMI()
    {
        Debug.Log("はじまりのうみステージ");
        // menuPanelはActive(true)のままにする
        HAZIMARINOUMI.SetActive(true);
    }
    public void StageChoice_SEIREINOMORI()
    {
        Debug.Log("せいれいのもりステージ");
        // menuPanelはActive(true)のままにする
        SEIREINOMORI.SetActive(true);
    }
    public void StageChoice_KARAPPONOKAZAN()
    {
        Debug.Log("からっぽのかざんステージ");
        // menuPanelはActive(true)のままにする
        KARAPPONOKAZAN.SetActive(true);
    }

    //ステージセレクト画面１に戻るとき
    public void InitStageChoice()
    {
        menuPanel.SetActive(true);

        // 各ステージセレクト画面は非表示にする（ワールド選択が見えなくなるため）
        HAZIMARINOUMI.SetActive(false);
        SEIREINOMORI.SetActive(false);
        KARAPPONOKAZAN.SetActive(false);
    }

    // ここから各ステージの遷移
    public void Stage1_1()
    {
        Debug.Log("はじまりのうみ1-1に遷移");
        script.SceneChange_Fade("1-1",1.0f);
    }

    public void Stage2_1()
    {
        Debug.Log("せいれいのもり1-1に移動");
        script.SceneChange_Fade("2-1",0.5f);
    }
    public void Stage3_1()
    {
        Debug.Log("からっぽのかざん1-1に移動");
        script.SceneChange_Fade("3-1",0.5f);
    }
}
