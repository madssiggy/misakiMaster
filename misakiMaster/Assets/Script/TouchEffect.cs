using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    // シーン管理マネージャの取得
    GameObject SceneNavigatorObj;
    SceneNavigator script;

    [SerializeField] GameObject CLICK_PARTICLE = default; // PS_TouchStarを割り当てる
    [SerializeField] GameObject DRAG_PARTICLE = default;  // PS_DragStarを割り当てる

    private GameObject m_ClickParticle = default;
    private GameObject m_DragParticle = default;

    private ParticleSystem m_ClickParticleSystem = default;
    private ParticleSystem m_DragParticleSystem = default;

    // タッチ状態管理Managerの読み込み
    [SerializeField] GameObject TouchStateManagerObj;
    public TouchStateManager TouchStateManagerScript;

    private bool DragFlag;     // ドラッグしはじめのときにtrueにする（連続でParticle.Play()させないため）
    bool touch;

    //音を鳴らす
    public AudioClip SE_awa;
    private bool awa_Flag;
    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        // フラグの初期化
        DragFlag = false;
        touch = false;

        // パーティクルを生成
        m_ClickParticle = (GameObject)Instantiate(CLICK_PARTICLE);
        m_DragParticle = (GameObject)Instantiate(DRAG_PARTICLE);

        // パーティクルの再生停止を制御するためコンポーネントを取得
        m_ClickParticleSystem = m_ClickParticle.GetComponent<ParticleSystem>();
        m_DragParticleSystem = m_DragParticle.GetComponent<ParticleSystem>();

        // 再生を止める（万が一急に再生されないように）
        m_ClickParticleSystem.Stop();
        m_DragParticleSystem.Stop();

        //タッチ状態管理Managerの取得
        TouchStateManagerObj = GameObject.Find("TouchStateManager");
        TouchStateManagerScript = TouchStateManagerObj.GetComponent<TouchStateManager>();

        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        script = SceneNavigatorObj.GetComponent<SceneNavigator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // パーティクルをマウスカーソルに追従させる
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 30.0f;  // OverLay以外のCanvasより手前に移動させる
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        touch = TouchStateManagerScript.GetTouch();

        if (m_DragParticle != null)
        {
            m_DragParticle.transform.position = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z);
        }
        //タッチされていたら
        if (touch)
        {
            // タッチ開始時
            if (TouchStateManagerScript.GetTouchPhase() == TouchPhase.Began)
            {
                m_ClickParticle.transform.position = mousePosition;

                m_ClickParticleSystem.Play();   // １回再生(ParticleSystemのLoopingにチェックを入れていないため)
            }
            // タッチ終了
            else if (TouchStateManagerScript.GetTouchPhase() == TouchPhase.Ended)
            {
                // Particleの放出を停止する
                m_ClickParticleSystem.Stop();
                m_DragParticleSystem.Stop();

                DragFlag = false;
            }
            // タッチ中
            else if(TouchStateManagerScript.GetTouchPhase() == TouchPhase.Moved)
            {
                if (!DragFlag)
                {
                    m_DragParticleSystem.Play();    // ループ再生(Loopingにチェックが入っている)
                    //string scene_name = TouchStateManagerScript.GetCurrentSceneName();
                    if (script.GetCurrentSceneName() == "TITLE" ||
                        script.GetCurrentSceneName() == "SELECT STAGE")
                    {
                        audioSource.PlayOneShot(SE_awa);
                    }
                    DragFlag = true;
                }

            }
        }

    }
}