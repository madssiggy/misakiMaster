using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchStateManager : MonoBehaviour
{
    public bool       IsTouch;                    // タッチしているかどうかのフラグ
    public Vector2    TouchPos;                   // タッチしている座標
    public TouchPhase Phase;                      // タッチの状態（開始、最中、終了）

    //==============================
    // 初期化処理
    //==============================
    void Start()
    {
        this.IsTouch = false;
        this.TouchPos = new Vector2(0, 0);
        this.Phase = TouchPhase.Began;
    }

    //=========================
    // 更新処理
    //=========================
    void Update()
    { 
        this.IsTouch = false;

        // マウス操作（エディタ上）
        if (Application.isEditor)
        {
            // 押した瞬間
            if (Input.GetMouseButtonDown(0))
            {                    
                this.IsTouch = true;
                this.Phase = TouchPhase.Began;
                Debug.Log("押した瞬間");
            }
            // 離した瞬間
            else if (Input.GetMouseButtonUp(0))
            {
                this.IsTouch = true;
                this.Phase = TouchPhase.Ended;
                Debug.Log("離した瞬間");
            }
            // 押しっぱなし
            else if (Input.GetMouseButton(0))
            {
                this.IsTouch = true;
                this.Phase = TouchPhase.Moved;
                Debug.Log("押しっぱなし");
            }

            // 座標取得
            if (this.IsTouch)
            {
                this.TouchPos = Input.mousePosition;
            }

        }
        // 実機（スマホ）使用時
        else
        {
            if (Input.touchCount > 0)
            {
                UnityEngine.Touch touch = Input.GetTouch(0);

                this.TouchPos = touch.position;
                this.Phase = touch.phase;
                this.IsTouch = true;
            }
        }
    }

    //=========================
    // タッチ情報の取得
    //=========================
    public bool GetTouch()
    {
        return this.IsTouch;
    }
    public Vector2 GetTouchPos()
    {
        return this.TouchPos;
    }
    public TouchPhase GetTouchPhase()
    {
        return this.Phase;
    }

}

