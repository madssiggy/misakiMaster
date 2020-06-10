using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//我シングルトンぞ？
public class SceneNavigator : SingletonMonoBehaviour<SceneNavigator>
{
    // シーン関連===========================================================
    private string beforeSceneName;     // 前のシーンを保持
    private string currentSceneName;    // 今のシーンを保持
    private string nextSceneName;       // 次のシーンを保持

    // 各シーンの参照
    public string BeforeSceneName
    {
        get { return beforeSceneName; }
    }
    public string CurrentSceneName
    {
        get { return currentSceneName; }
    }
    public string NextSceneName
    {
        get { return nextSceneName; }
    }

    // フェード関連===========================================================
    [SerializeField]private CanvasFader fader = null;   // フェード用クラス

    //フェード時間
    public const float FADE_TIME = 0.5f;
    private float FadeTime;

    // フェードしているかどうかの参照
    public bool IsFading
    {
        get { return fader.IsFading || fader.Alpha != 0; }
    }
    //フェード後のイベント
    public event Action FadeOutFinished = delegate { };
    public event Action FadeInFinished = delegate { };

    // 初期化(Awake時かその前の初アクセス時、どちらかの一度しか行われない)
    protected override void Init()
    {
        base.Init();

        if (fader == null)
        {
            Reset();
        }

        // 最初のシーン名設定
        currentSceneName = SceneManager.GetSceneAt(0).name;

        // 存在の永続化
        DontDestroyOnLoad(gameObject);
    }
    private void Reset()
    {
        // オブジェクトの名前を設定
        gameObject.name = "SceneNavigator";

        // フェード用のキャンバス作成
        GameObject fadeCanvas = new GameObject("FadeCanvas");
        fadeCanvas.transform.SetParent(transform);
        fadeCanvas.SetActive(false);

        Canvas canvas = fadeCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        fadeCanvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        fadeCanvas.AddComponent<GraphicRaycaster>();
        fader = fadeCanvas.AddComponent<CanvasFader>();
        fader.Alpha = 0;

        // フェード用の画像作成
        GameObject imageObject = new GameObject("FadeImage");
        imageObject.transform.SetParent(fadeCanvas.transform, false);
        imageObject.AddComponent<Image>().color = Color.black;

        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000, 2000);
    }

    //=========================ここから各スクリプトでお呼び出し用の遷移関数

    //=============================
    // フェードなしで遷移する
    //=============================
    public void SceneChange(string SceneName)
    {
        //遷移予定のシーン名をnextSceneに入れる
        nextSceneName = SceneName;

        //シーン名更新
        beforeSceneName = currentSceneName;
        currentSceneName = nextSceneName;

        SceneManager.LoadScene(nextSceneName);
    }
    //=============================
    // フェードありで遷移する
    //=============================
    public void SceneChange_Fade(string SceneName, float fadeTime)
    {
        // もし既にフェード状態であれば
        if (IsFading)
        {
            return;
        }

        //次のシーン名を設定
        nextSceneName = SceneName;

        //フェード時間の設定
        FadeTime = fadeTime;

        //フェードアウト
        fader.gameObject.SetActive(true);
        fader.Play(isFadeOut: false, duration: FadeTime, onFinished: OnFadeOutFinish);
    }
    //====================================
    // フェードありでSceneReload
    //====================================
    public void SceneReload(float fadeTime)
    {
        //フェード時間の設定
        FadeTime = fadeTime;

        //フェードアウト
        fader.gameObject.SetActive(true);
        fader.Play(isFadeOut: false, duration: FadeTime, onFinished: OnFadeOutFinish_Reload);

    }
    //=============================
    // フェードありで前シーンに戻る
    //=============================
    public void SceneBack_Fade(float fadeTime)
    {
        // もし既にフェード状態であれば
        if (IsFading)
        {
            return;
        }

        //フェード時間の設定
        FadeTime = fadeTime;

        //フェードアウト
        fader.gameObject.SetActive(true);
        fader.Play(isFadeOut: false, duration: FadeTime, onFinished: OnFadeOutFinish_Back);
    }
    //=========================ここまで。以下他スクリプトからは呼べません

    //=====================================
    // フェードアウト終了①、シーン遷移つき
    //=====================================
    private void OnFadeOutFinish()
    {
        FadeOutFinished();

        //シーン読み込み、変更
        SceneManager.LoadScene(nextSceneName);

        //シーン名更新
        beforeSceneName = currentSceneName;
        currentSceneName = nextSceneName;

        //フェードイン開始
        fader.gameObject.SetActive(true);
        fader.Alpha = 1;
        fader.Play(isFadeOut: true, duration: FadeTime, onFinished: OnFadeInFinish);
    }
    //=====================================
    // フェードアウト終了②,SceneReload用
    //=====================================
    private void OnFadeOutFinish_Reload()
    {
        FadeOutFinished();

        //シーン再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //フェードイン開始
        fader.gameObject.SetActive(true);
        fader.Alpha = 1;
        fader.Play(isFadeOut: true, duration: FadeTime, onFinished: OnFadeInFinish);
    }
    //=====================================
    // フェードアウト終了③,SceneBack用
    //=====================================
    private void OnFadeOutFinish_Back()
    {
        FadeOutFinished();

        // 前シーンのロード
        SceneManager.LoadScene(beforeSceneName);

        //シーン名更新
        beforeSceneName = currentSceneName;
        currentSceneName = beforeSceneName;

        //フェードイン開始
        fader.gameObject.SetActive(true);
        fader.Alpha = 1;
        fader.Play(isFadeOut: true, duration: FadeTime, onFinished: OnFadeInFinish);
    }
    //==========================
    // フェードイン終了
    //==========================
    private void OnFadeInFinish()
    {
        fader.gameObject.SetActive(false);
        FadeInFinished();
    }

    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }
}
