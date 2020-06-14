using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    // タッチ状態管理Managerの読み込み
    [SerializeField] GameObject TouchStateManagerObj;
    TouchStateManager TouchStateManagerScript;

    public LayerMask mask;          // 特定レイヤーのみ判定衝突を行うようにするためのマスク、Unity上で設定（TouchManagerインスペクタ内）
    private GameObject startObj;    // タッチ始点にあるオブジェクトを格納
    private GameObject endObj;      // タッチ終点にあるオブジェクトを格納
    public string currentName;      // タグ判定用のstring変数

    // 削除するバイキンのリスト
    List<GameObject> removableBaikinList = new List<GameObject>();

    public float MaxDistance;

    //マネージャー読み込み======
    public GameObject managerObj;
    manager managerScript;

    //====================================
    //タッチ回数保存===========
        public int touchNum;    //touchKaisuu.csで使ってます。スライムを消すタッチをした場合のみプラスされる
        bool touchFlg;          //スライムを消すタッチであった場合のみtouchNumをプラスする
    //=======================
    //スライム動いて消えるやつ
    BubbleScript startObjScript;
    bool isStartBubbleMove;//動かしてよいスライムが動いているか
    Vector3 CreatePosition;
    bool canCreate;
    Vector3 startMoveWay;
    bool MiddleBubbleisRotX;
   public bool isFusion;//中小の泡を操作中に合体しないように
    //====================================亀山

    //音を鳴らす
    public AudioClip SE_awa;
    public AudioClip SE_Kuttuku;
    AudioSource audioSource;
    private bool awa_Flag;
    private bool Kuttuki_Flag;

    //=========================
    // 初期化処理
    //=========================
    void Start()
    {
        //タッチ状態管理Managerの取得
        TouchStateManagerObj = GameObject.Find("TouchStateManager");
        TouchStateManagerScript = TouchStateManagerObj.GetComponent<TouchStateManager>();

        managerObj = GameObject.Find("StageManager");
        managerScript = managerObj.GetComponent<manager>();
        canCreate = false;
        startMoveWay=new Vector3(0,0,0);
        CreatePosition = new Vector3(0, 0, 0);
        MiddleBubbleisRotX = false;

        //Componentを取得
        audioSource = GetComponent<AudioSource>();
        awa_Flag = false;
        Kuttuki_Flag = false;
        isFusion = false;
    }

    //=========================
    // 更新処理
    //=========================
    void Update()
    {
        if(TouchStateManagerScript.GetTouch() == false && awa_Flag == true)
        {
            awa_Flag = false;
        }
        // タッチされている時
        if (managerScript.isRotate == false && TouchStateManagerScript.GetTouch())
        {
            Kuttuki_Flag = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!(Physics.Raycast(ray, out hit, Mathf.Infinity, mask)))
            {
                if (!awa_Flag)
                {
                    audioSource.PlayOneShot(SE_awa);
                    awa_Flag = true;
                }
            }
            if (startObj == null)
            {
                // Rayが特定レイヤの物体（バイキン）に衝突している場合
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    //　大バイキンにRayが衝突している時
                    if (hit.collider.gameObject.CompareTag("BigSlime") &&
                        isFusion ==false)
                    {
                      Debug.Log("爆発");

                        touchFlg = true;Debug.Log("有効なタッチである");

                        // 処理内容はslimeControl.csのBigSlimeClickAct()の中
                        hit.collider.gameObject.GetComponent<slimeControl>().SlimeDestroy(new Vector3(0, 0, 0));
                        managerScript.CheckBubble();
                    }
                    //　小、中バイキンにRayがぶつかった時
                    else if (hit.collider.gameObject.CompareTag("MiddleSlimeTate")|| hit.collider.gameObject.CompareTag("MiddleSlimeYoko") ||
                             hit.collider.gameObject.CompareTag("SmallSlime"))
                    {
                        isFusion = true;
                        currentName = hit.collider.gameObject.tag;

                        // バイキンオブジェクトを格納
                        startObj = hit.collider.transform.parent.gameObject;
                        endObj = hit.collider.transform.parent.gameObject;
                        
                        // 削除対象オブジェクトリストの初期化
                        removableBaikinList = new List<GameObject>();

                        // 削除対象のオブジェクトを格納
                        PushToList(hit.collider.gameObject);

                        Debug.Log("削除対象追加");
                    }
                }
                else
                {
                }
            }
            //タッチ終了時
            else if(TouchStateManagerScript.GetTouchPhase() == TouchPhase.Ended)
            {
                int remove_cnt = removableBaikinList.Count;

                if (remove_cnt == 2)
                {
                    if (startObj.CompareTag("MiddleSlime"))
                    {
                        //CreateBigBubble();
                    }
                    //小バイキンが消された場合
                    else if (startObj.CompareTag("SmallSlime"))
                    {
                        //CreateMiddleBubble();
                    }

                    //GameObject.Destroy(startObj);
                    //GameObject.Destroy(endObj);
               
                    //startObj.GetComponent<slimeControl>().BubbleMove(Vector3.Normalize(startObj.transform.position - endObj.transform.position));

                }
                // 消す対象外の時
                else
                {
                    for (int i = 0; i < remove_cnt; i++)
                    {
                        removableBaikinList[i] = null;
                    }
                }

                // リスト内のバイキンを消す
                currentName = null;
                startObj = null;
                endObj = null;

            }
            // タッチ中
            else if(TouchStateManagerScript.GetTouchPhase() == TouchPhase.Moved && startObj != null)
            {
                // Rayが特定レイヤの物体（バイキン）に衝突している場合
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider != null)
                    {
                        GameObject hitObj = hit.collider.transform.parent.gameObject;

                        // 同じタグのブロックをクリック＆endObjとは別オブジェクトである時
                        if (hitObj.tag == currentName && endObj != hitObj)
                        {
                   
                            Debug.Log("同タグの別オブジェクトが選択された");
                            // ２つのオブジェクトの距離を取得
                            float distance = Vector2.Distance(hitObj.transform.position, endObj.transform.position);
                            isFusion = false;
                            if (distance <= MaxDistance)
                            {
                                Debug.Log("z値を取得し比較");
                                // zが同じであれば
                                
                                if (((managerScript.GetNowFront() == (int)manager.Wall.Front) || (managerScript.GetNowFront() == (int)manager.Wall.Back)) &&
                                    Mathf.Floor(Mathf.Abs(startObj.transform.parent.position.z))/(MaxDistance/2) == 
                                    Mathf.Floor(Mathf.Abs(hitObj.transform.parent.position.z)) /(MaxDistance/2)
                                )
                                {
                                    DecideBubble(hitObj);
                                }
                                else  if (((managerScript.GetNowFront() == (int)manager.Wall.Right) || (managerScript.GetNowFront() == (int)manager.Wall.Left))&&
                                            Mathf.Floor(Mathf.Abs(startObj.transform.parent.position.x)) / (MaxDistance / 2) ==
                                            Mathf.Floor(Mathf.Abs(hitObj.    transform.parent.position.x)) / (MaxDistance / 2))
                                {
                                    DecideBubble(hitObj);
                                }
                            }
                        }
                    }
                }
            }
      
                
        }

        if (touchFlg == true)
        {
            //ここに中小のバイキン削除、バイキン生成を移す
            touchNum++;
            managerScript.CheckBubble();
            touchFlg = false;
        }
    }

    //==============================================================
    //　選択されているバイキンを除去リストに格納する
    //==============================================================
    void PushToList(GameObject obj)
    {
        // 除去リストに選択しているオブジェクトを追加
        removableBaikinList.Add(obj);

        // どのオブジェクトが除去リスト入りしているかわかりやすいように名前に_をつけたす
        obj.name = "_" + obj.name;
    }

    /*==================================================
     生成されるバイキンの角度を
     managerに保存されているカメラ位置に対応したRotateで生成する
     ===================================================    */

    Vector3 CreateSlimeQuarternion()
    {
        //角度別バイキン生成
        Vector3 compared = startObj.transform.position;
        Vector3 compare = endObj.transform.position;
        Vector3 Return;
        Return = new Vector3(0, 0, 0);
        //if (managerScript.cameraRotate % 2 == 0)
        //    prefRotate.y = 0;
        //else
        //    prefRotate.y = 90;
        int nowFront = managerScript.nowFront;
        switch (nowFront) {
                case (int)manager.Wall.Left:
                Return.y = 90f;
                break;
            case (int)manager.Wall.Right:
                    Return.y = 270f;
                    break;
                case (int)manager.Wall.Front:
                Return.y = 0f;
                break;
            case (int)manager.Wall.Back:
                default:
                Return.y = 180f;
                break;
            }

        //位置取得。
        //if (Mathf.Floor(compare.x) / (MaxDistance / 2) ==
        //    Mathf.Floor(compared.x) / (MaxDistance / 2)) {
        //    //縦長バイキン生成
        //    prefRotate.z = 90;
        //} else if (Mathf.Floor(compare.y) / (MaxDistance / 2) ==
        //    Mathf.Floor(compared.y) / (MaxDistance / 2)) {
        //    //横長バイキン生成
        //    prefRotate.z = 0;
        //}

        //    Return.y = startObj.transform.parent.transform.rotation.y;

        return Return;
    }

    Vector3 CreateBigSlimeQuarternion()
    {
        Vector3 Return;
        Return = new Vector3(0, 0, 0);
        Vector3 compared = startObj.transform.position;
        Vector3 compare = endObj.transform.position;
        int nowFront = managerScript.nowFront;
        //if (Mathf.Floor(compare.x) / (MaxDistance / 2) ==
        // Mathf.Floor(compared.x) / (MaxDistance / 2)) {
        //    //縦長バイキン生成
        //   Return.z = 90;
        //} else {
        //    Return.z = 0;
        //}
        if (startObj.tag == "MiddleSlimeTate") {
            switch (nowFront) {
                case (int)manager.Wall.Left:
                    Return.y = 90f;
                    break;
                case (int)manager.Wall.Right:
                    Return.y = 270f;
                    break;
                case (int)manager.Wall.Front:
                    Return.y = 0f;
                    break;
                case (int)manager.Wall.Back:
                default:
                    Return.y = 180f;
                    break;
            }
        } else if (startObj.tag == "MiddleSlimeYoko") {
            switch (nowFront) {
                case (int)manager.Wall.Left:
                    Return.y = 90f;
                    break;
                case (int)manager.Wall.Right:
                    Return.y = 270f;
                    break;
                case (int)manager.Wall.Front:
                    Return.y = 0f;
                    break;
                case (int)manager.Wall.Back:
                default:
                    Return.y = 180f;
                    break;
            }
        }

        if (MiddleBubbleisRotX == true) {
            Return.x = 90f;
            MiddleBubbleisRotX = false;
        }


        return Return;
    }

    public Vector3 GetStartObj()
    {
        return startObj.transform.position;
    }

    int Create = 0;
    public void CreateSlime(string tag)
    {
        Create++;
        Debug.Log("Touch.Cs.CreateSlime::Create=" + Create+"==============================================");
        if (Create == 2) {
            if (tag == "MiddleSlimeTate"|| tag == "MiddleSlimeYoko")
                CreateBigBubble();
            else if (tag == "SmallSlime")
                CreateMiddleBubble();
            Create = 0;
        }
    }
    
    void CreateMiddleBubble()
    {
      
        int nowFront = managerScript.nowFront;
        slimeControl slc = endObj.GetComponent<slimeControl>();
        Debug.Log("Way" + startMoveWay+"========================================");
        string PrefPath = null;
//        "Prefab/Fields/FieldInMidYoko";
        switch (nowFront) {
            case (int)manager.Wall.Left:
            case (int)manager.Wall.Right:
                if (startObj.transform.position.x == endObj.transform.position.x) {
                    if (startObj.transform.position.y == endObj.transform.position.y) {
                        PrefPath = "Prefab/Fields/FieldInMidYoko";
                        Debug.Log("z>y,Tate");
                    } else if (startObj.transform.position.z == endObj.transform.position.z) {
                        PrefPath = "Prefab/Fields/FieldInMidTate";
                        Debug.Log("z>y,Tate");
                    }

                }
                    break;
            case (int)manager.Wall.Front:
            case (int)manager.Wall.Back:
            default:
                if (startObj.transform.position.z == endObj.transform.position.z) {
                    if (startObj.transform.position.y == endObj.transform.position.y) {
                        PrefPath = "Prefab/Fields/FieldInMidYoko";
                        Debug.Log("z>y,Tate");
                    } else if (startObj.transform.position.x == endObj.transform.position.x) {
                        PrefPath = "Prefab/Fields/FieldInMidTate";
                        Debug.Log("z>y,Tate");
                    }

                }
                break;
        }
        //  if (startObj.transform.localPosition.x == endObj.transform.localPosition.x)


        GameObject obj = (GameObject)Resources.Load(PrefPath);
        //生成したプレハブをFieldCenterに登録する。
        GameObject tmp = null;
 //       Debug.Log("スタート位置:"+startObj.transform.localPosition + "エンド位置:"+endObj.transform.localPosition+"発生予定位置:"+CreatePosition);
        //プレハブを元に、インスタンスを生成
        tmp = Instantiate(obj,
          CreatePosition,
                     Quaternion.Euler(CreateSlimeQuarternion()));
        tmp.transform.parent = GameObject.Find("FieldCenter").transform;
        Debug.Log("終点側に中バイキンを生成");
        touchFlg = true;
        MiddleBubbleisRotX = false;
        Debug.Log("有効なタッチである");

        // くっつく音をいれる
        if (!Kuttuki_Flag)
        {
            audioSource.PlayOneShot(SE_Kuttuku);
            Kuttuki_Flag = true;
        }
    }
    void CreateBigBubble()
    {
        Debug.Log("スタート位置:" + startObj.transform.localPosition + "エンド位置:" + endObj.transform.localPosition + "発生予定位置:" + CreatePosition);
        Debug.Log("親：" + startObj.transform.parent.gameObject);
        GameObject obj = (GameObject)Resources.Load("Prefab/Fields/FieldInBIg");
        //プレハブを元に、インスタンスを生成
        GameObject tmp = Instantiate(obj,
        CreatePosition,
                      Quaternion.Euler(CreateBigSlimeQuarternion()));
        //生成したプレハブをFieldCenterに登録する。
        tmp.transform.parent = GameObject.Find("FieldCenter").transform;
        Debug.Log("終点側に大バイキンを生成");
        touchFlg = true;

        Debug.Log("有効なタッチである");

        // くっつく音をいれる
        if (!Kuttuki_Flag)
        {
            audioSource.PlayOneShot(SE_Kuttuku);
            Kuttuki_Flag = true;
        }

    }
   public void SetStartAndEnd(GameObject start,GameObject end)
    {
        startObj = start;
        endObj = end;
    }

public void setStartMoveWay(Vector3 way) {
        startMoveWay = way;
    }

    void DecideBubble(GameObject hitObj)
    {
        Debug.Log("削除します");
        bool canBubbleAct = false;
        // 削除対象のオブジェクトを格納
        endObj = hitObj;
        string ObjTag = startObj.tag;

        if (startObj.CompareTag("MiddleSlimeTate") || startObj.CompareTag("MiddleSlimeYoko")) {
            if (ObjTag == "MiddleSlimeYoko") {
                switch (managerScript.GetNowFront()) {
                    case (int)manager.Wall.Right:
                    case (int)manager.Wall.Left:
                        /*上から見て平行でも合体できるようにするためのスクリプト。
    RorLからみた位置であれば、xが同じであることが最低条件で、Ｙが同じでかつどちらの泡も角度が0or180なら
    rot.xを９０にした状態bigBubbleが生成されるようにする
    */ if (startObj.transform.position.x == endObj.transform.position.x ) {
   
                            if (startObj.transform.position.z == endObj.transform.position.z) {
                                canBubbleAct = true;
                            }
                            else if ((startObj.transform.position.y == endObj.transform.position.y)&&
                                (((Mathf.Abs(startObj.transform.localEulerAngles.y) ==0f) || (Mathf.Abs(startObj.transform.localEulerAngles.y) == 180f))
                                && ((Mathf.Abs(endObj.transform.localEulerAngles.y) == 0f) || (Mathf.Abs(endObj.transform.localEulerAngles.y) == 180f)))) {
                                canBubbleAct = true;
                                MiddleBubbleisRotX = true;
                            }
                        }
                        break;
                    default:
                        /*正面or背面から見た場合は奥行き（z軸）が同じなのが最低条件で、
                         （xが同じ）という条件と、（Ｙが同じ）かつ（どちらの泡も角度が９０か２７０）ならrot.x=90*/
                        if (startObj.transform.position.z == endObj.transform.position.z) {
                            if (startObj.transform.position.x == endObj.transform.position.x) {
                                canBubbleAct = true;
                            } else if ((startObj.transform.position.y == endObj.transform.position.y) &&
                                  (((Mathf.Abs(startObj.transform.localEulerAngles.y) == 90f) || (Mathf.Abs(startObj.transform.localEulerAngles.y) == 270f))
                                  && ((Mathf.Abs(endObj.transform.localEulerAngles.y) == 90f) || (Mathf.Abs(endObj.transform.localEulerAngles.y) == 270f)))) {
                                canBubbleAct = true;
                                MiddleBubbleisRotX = true;
                            }
                        }
                        break;
                }
            }

            if ((ObjTag == "MiddleSlimeTate")) {
                switch (managerScript.GetNowFront()) {
                    case (int)manager.Wall.Right:
                    case (int)manager.Wall.Left:
                        if (startObj.transform.position.x == endObj.transform.position.x
                            && startObj.transform.position.y == endObj.transform.position.y) {
                            canBubbleAct = true;
                        }
                        break;
                    default:
                        if (startObj.transform.position.y == endObj.transform.position.y
                            && startObj.transform.position.z == endObj.transform.position.z) {
                            canBubbleAct = true;
                        }
                        break;
                }
            }
        }
        //小バイキンが消された場合
        else if (startObj.CompareTag("SmallSlime")) {
            switch (managerScript.GetNowFront()) {
                case (int)manager.Wall.Right:
                case (int)manager.Wall.Left:
                    //xがおなじでなければ成立しない
                    if (startObj.transform.position.x == endObj.transform.position.x) {
                        if (startObj.transform.position.y == endObj.transform.position.y|| startObj.transform.position.z == endObj.transform.position.z) {
                            canBubbleAct = true;
                        }
                    }
                    break;
                default:
                    //zがおなじでなければ成立しない
                    if (startObj.transform.position.z == endObj.transform.position.z) {
                        if (startObj.transform.position.y == endObj.transform.position.y || startObj.transform.position.x == endObj.transform.position.x) {
                            canBubbleAct = true;
                        }
                    }
                    break;
           }


        }
        if (canBubbleAct == true) {
            startObj.GetComponent<slimeControl>().goSign(endObj);
            startMoveWay = Vector3.Normalize(endObj.transform.localPosition - startObj.transform.localPosition);

            startObj.GetComponent<slimeControl>().SetQuarternion(CreateSlimeQuarternion());
            CreatePosition = ((startObj.transform.localPosition + endObj.transform.localPosition) / 2);
            // 削除対象のオブジェクトを格納
            PushToList(hitObj);
     
        }
      //  Debug.Log("start.rot=" + startObj.transform.rotation + "local" + startObj.transform.localRotation);
    }
}
