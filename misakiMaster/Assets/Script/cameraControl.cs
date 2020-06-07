using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    GameObject targetObj;
    GameObject Manager;
    Vector3 targetPos;
    manager script;
    float cameraAngle = 0.0f;

    Bottun BottunScriptL;
    Bottun BottunScriptR;
    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("FieldCenter");
        targetPos = targetObj.transform.position;

        Manager = GameObject.Find("StageManager");
        script = Manager.GetComponent<manager>();

        BottunScriptL = GameObject.Find("ButtonL").GetComponent<Bottun>();
        BottunScriptR = GameObject.Find("ButtonR").GetComponent<Bottun>();
    }

    // Update is called once per frame
    void Update()
    {
     //   CameraRoll();
    }

    void CameraRoll()
    {
		
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        // キーを押している間
        if (Input.anyKey) {
            // カメラ移動量
            float InputX = 0f;//Input.GetAxis("Mouse X");
            if (Input.GetKeyDown(KeyCode.A)&&script.isRotate==false) {
                InputX = -90.0f;
				script.isCamera = true;
                script.changeCameraRotateLeft();
            }

            if (Input.GetKeyDown(KeyCode.D) && script.isRotate == false) {
                InputX = 90.0f;
				script.isCamera = true;
				script.changeCameraRotateRight();

            }
                
            // float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, InputX );
			// カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
			// transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
			script.isCamera = false;
		}
    }
    public IEnumerator RollYL()
    {
        Debug.Log("カメラ回転-----------------------------------------------------");
        //回転中のフラグを立てる
        script.isRotate = true;
        //回転処理
        float sumAngle = 0f; //angleの合計を保存

        while (sumAngle > -90f) {
            cameraAngle = -1.0f; //ここを変えると回転速度が変わる
            sumAngle += cameraAngle;

            // 90度以上回転しないように値を制限
            if (sumAngle < -90.0f) {
                cameraAngle -= sumAngle + 90.0f;
            }

            transform.RotateAround
                 (
                     targetObj.transform.position,
                     Vector3.up,
                     cameraAngle
                 );
            yield return null;
        }
        BottunScriptL.SetisClicked(false);




        //回転中のフラグを倒す
        script.isRotate = false;
        script.SetFront(script.nowFront, false);
        Mathf.Ceil(transform.rotation.x);
        Mathf.Ceil(transform.rotation.y);
        Mathf.Ceil(transform.rotation.z);
        yield break;
    }

    public IEnumerator RollYR()
    {
        Debug.Log("カメラ回転-------------------------------------------------");
        //回転中のフラグを立てる
        script.isRotate = true;
        //回転処理
        float sumAngle = 0f; //angleの合計を保存

        while (sumAngle < 90f) {
            cameraAngle = 1.0f; //ここを変えると回転速度が変わる
            sumAngle += cameraAngle;

            // 90度以上回転しないように値を制限
            if (sumAngle > 90f) {
                cameraAngle -= sumAngle - 90f;
            }
            transform.RotateAround
                        (
                           targetObj .transform.position,
                            Vector3.up,
                            cameraAngle
                        );


            yield return null;
        }
        BottunScriptR.SetisClicked(false);
        //回転中のフラグを倒す
        script.isRotate = false;
        script.SetFront(script.nowFront, true);
        Mathf.Ceil(transform.rotation.x);
        Mathf.Ceil(transform.rotation.y);
        Mathf.Ceil(transform.rotation.z);
        yield break;
    }
}
