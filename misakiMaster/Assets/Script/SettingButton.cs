using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingButton : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer = default;

    // シーン管理マネージャの取得
    GameObject SceneNavigatorObj;
    SceneNavigator script;

    [SerializeField] GameObject setting_buttonObj;

    private string scene_name;

    [SerializeField] GameObject vol1 = default;
    [SerializeField] GameObject vol2 = default;
    [SerializeField] GameObject vol3 = default;

    [SerializeField] public static int NowVol = 2;

    // Start is called before the first frame update
    void Start()
    {
        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        script = SceneNavigatorObj.GetComponent<SceneNavigator>();

        vol1 = GameObject.Find("vol1");
        vol2 = GameObject.Find("vol2");
        vol3 = GameObject.Find("vol3");

        scene_name = script.GetCurrentSceneName();

        if (scene_name != "SELECT STAGE")
        {
            setting_buttonObj = GameObject.Find("設定画面");
            setting_buttonObj.SetActive(false);
        }

        if (NowVol == 1)
        {
            vol1.SetActive(true);
            vol2.SetActive(false);
            vol3.SetActive(false);
        }
        else if (NowVol == 2)
        {
            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(false);
        }
        else if (NowVol == 3)
        {
            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(true);
        }

    }
    // 音量UP
    public void OnVolUp()
    {
        if (NowVol == 1)
        {
            Debug.Log("音量２に");
            audioMixer.SetFloat("SE", 0.0f);
            audioMixer.SetFloat("BGM", 0.0f);

            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(false);

            NowVol++;
        }
        else if (NowVol == 2)
        {
            Debug.Log("音量３に");
            audioMixer.SetFloat("SE", 15.0f);
            audioMixer.SetFloat("BGM", 15.0f);

            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(true);

            NowVol++;
        }
    }
    // 音量DOWN
    public void OnVolDown()
    {
        if (NowVol == 2)
        {
            Debug.Log("音量１に");
            audioMixer.SetFloat("SE", -15.0f);
            audioMixer.SetFloat("BGM", -15.0f);
            vol1.SetActive(true);
            vol2.SetActive(false);
            vol3.SetActive(false);

            NowVol--;
        }
        else if (NowVol == 3)
        {
            Debug.Log("音量２に");
            audioMixer.SetFloat("SE", 0.0f);
            audioMixer.SetFloat("BGM", 0.0f);

            vol1.SetActive(true);
            vol2.SetActive(true);
            vol3.SetActive(false);

            NowVol--;
        }
    }
    // セレクト画面に遷移
    public void OnBackSelect()
    {
        script.SceneChange_Fade("SELECT STAGE",1.0f);
    }
    // タイトル画面に遷移
    public void OnBackTitle()
    {
        script.SceneChange_Fade("TITLE", 1.0f);
    }
    // 現在のシーンを再読み込み
    public void OnReload()
    {
        script.SceneReload(1.0f);
    }
    //設定ボタンのパネルの表示オフ
    public void OnDisappearSettingButton()
    {
        setting_buttonObj.SetActive(false);
    }
    //設定ボタンのパネルの表示オン
    public void OnClickSettingButton()
    {
        setting_buttonObj.SetActive(true);
    }

}
