using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    private static float MaxTime;       // 時間（カウントダウンしていく）
    public static int Seconds;          // floatからintに変換するのに必要（５～０のカウントダウンであるため）
    public Text text_CountDown;         // 表示用テキスト

    // Start is called before the first frame update
    void Start()
    {
        // カウントダウン開始秒数のセット(整数で表示されるので、カウントダウンしたい数字+1.0fにする）
        MaxTime = 6.0f;

        Seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxTime > 0.0f)
        {
            MaxTime -= Time.deltaTime;
            Seconds = (int)MaxTime;
            text_CountDown.text = Seconds.ToString();
        }
        if(MaxTime <= 0.0f)
        {
            // リトライ→セレクト
            SceneManager.LoadScene("SELECT STAGE");
        }
    }
}
