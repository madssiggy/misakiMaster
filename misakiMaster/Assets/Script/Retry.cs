using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    public AudioClip SE_betyo;
    AudioSource audioSource;

    // シーン管理マネージャを取得する用の変数=======
    GameObject SceneNavigatorObj;
    SceneNavigator SceneScript;
    //==============================================

    // ゲームオーバーの取得用=======================
    GameObject RetryPanelMakeObj;
    RetryPanelMake RetryScript;
    //==============================================

    public GameObject image4_01 = default;
    public GameObject image4_02 = default;
    public GameObject image4_03 = default;
    public GameObject image4_END = default;
  
    private bool Flag_4_01;
    private bool Flag_4_02;
    private bool Flag_4_03;

    // カウントダウンに関わる処理用の変数===========
    private static float MaxTime;       // 時間（カウントダウンしていく）
    public static int Seconds;          // floatからintに変換するのに必要（５～０のカウントダウンであるため）
    public Text text_CountDown;         // 表示用テキスト
    //===============================================

    
    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();

        //シーン管理マネージャの取得 ===================================
        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        SceneScript = SceneNavigatorObj.GetComponent<SceneNavigator>();
        //==============================================================

        image4_01.SetActive(false);
        image4_02.SetActive(false);
        image4_03.SetActive(false);
        image4_END.SetActive(false);

        Flag_4_01 = false;
        Flag_4_02 = false;
        Flag_4_03 = false;

        // カウントダウン開始秒数のセット(整数で表示されるのでカウントダウンしたい数字+1.0fにする）
        MaxTime = 6.0f;
        Seconds = 0;
        //===============================================

        RetryPanelMakeObj = GameObject.Find("RetryPanelMake");
        RetryScript = RetryPanelMakeObj.GetComponent<RetryPanelMake>();
    }
    void Update()
    {
        if (RetryScript.GetGameOverFlag())
        {
            // カウントダウンの処理(整数で表示されるので1.0fにする）
            if (MaxTime > 1.0f)
            {
                MaxTime -= Time.deltaTime;
                Seconds = (int)MaxTime;
                text_CountDown.text = Seconds.ToString();

                if (Seconds == 3.0f)
                {
                    if (!Flag_4_01)
                    {
                        image4_01.SetActive(true);
                        //音(べちょっ)を鳴らす
                        audioSource.PlayOneShot(SE_betyo);

                        Flag_4_01 = true;
                    }
                }
                else if(Seconds == 2.0f)
                {
                    if (!Flag_4_02)
                    {
                        image4_02.SetActive(true);
                    //音(べちょっ)を鳴らす
                    audioSource.PlayOneShot(SE_betyo);
                        Flag_4_02 = true;
                    }
                }
                else if (Seconds == 1.0f)
                {
                    if (!Flag_4_03)
                    {
                        image4_03.SetActive(true);
                        //音(べちょっ)を鳴らす
                        audioSource.PlayOneShot(SE_betyo);
                        Flag_4_03 = true;
                    }
                }
            }

            
            //カウントダウンが０になったら強制GiveUp判定
            else if (MaxTime <= 1.0f)
            {
                image4_END.SetActive(true);
                // ゲーム（リトライ）→ステージセレクトに遷移
                SceneScript.SceneChange_Fade("SELECT STAGE", 0.5f);
            }
        }
    }
    // タッチされたらSceneReloadする（再度同じステージのゲーム画面へ）
    public void Reload_OnClick()
    {
        // ゲーム（リトライ）→ゲームに遷移（シーンをReload）
        SceneScript.SceneReload(0.5f);
    }
    // タッチされたらSceneChangeする(ステージセレクトへ）
    public void GiveUp_OnClick()
    {
        // ゲーム（リトライ）→ステージセレクトに遷移
        SceneScript.SceneChange_Fade("SELECT STAGE", 0.5f);
    }
}
