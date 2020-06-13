using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    // シーン管理マネージャを取得するようの変数====================2020/5/27美咲追加
    GameObject SceneNavigatorObj;
    SceneNavigator script;
    //===============================================
    public int cameraRotate;   //true = X軸、false = Z軸

    public bool isRotate = false;
    public bool isCamera = false;

    public enum Wall
    {
       Front = 0,
        Back,
        Left,
        Right
    }//前左後右で0~3
    public int nowFront;  //現在上にある面が何かを保持する

    public enum SlimeSize
    {
        small,middle,big,
    }
    GameObject[] bubble;

    public int bubbleNum;

    //音を鳴らす
    public AudioClip SE_Destroy;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //シーン管理マネージャの取得 ==================== 2020 / 5 / 27美咲追加
        SceneNavigatorObj = GameObject.Find("SceneNavigator");
        script = SceneNavigatorObj.GetComponent<SceneNavigator>();
        //==============================================================

        cameraRotate = 0;
        nowFront = (int)Wall.Front;

        bubble = GameObject.FindGameObjectsWithTag("bubble");

        bubbleNum = bubble.Length;

        audioSource = GetComponent<AudioSource>();
        //Debug.Log("virusnum=" + virusnum);
    }

    // Update is called once per frame
    void Update()
    {



    }
  

    public void SetFront(int ChangeFront,bool rollWay)
    {
        switch (rollWay)
        {
            case true:
                switch (ChangeFront)
                {
                    case (int)Wall.Front:
                        nowFront = (int)Wall.Left;
                        break;
                    case (int)Wall.Back:
                        nowFront = (int)Wall.Right;
                        break;
                    case (int)Wall.Left:
                        nowFront = (int)Wall.Back;
                            break;
                    case (int)Wall.Right:
                        nowFront = (int)Wall.Front;
                        break;
                    default:break;
                }
                break;

            case false:
                switch (ChangeFront)
                {
                    case (int)Wall.Front:
                        nowFront = (int)Wall.Right;
                        break;
                    case (int)Wall.Back:
                        nowFront = (int)Wall.Left;
                        break;
                    case (int)Wall.Left:
                        nowFront = (int)Wall.Front;
                        break;
                    case (int)Wall.Right:
                        nowFront = (int)Wall.Back;
                        break;
                    default:
                        break;
                }
                break;
                
            default:
                break;

        }
    }//rollWay=trueが左、falseが左

    public void CreatePrefabAsChild(GameObject Parents, GameObject Child, Vector3 Posit = default(Vector3), string tag = default(string))
    {
        Vector3 pos = Posit;
        // プレハブからインスタンスを生成
        GameObject obj = (GameObject)Instantiate(Child, pos, Quaternion.identity);
        // 作成したオブジェクトを子として登録
        obj.transform.parent = Parents.transform;
    }//Parentsで親クラスをGameObjectで直接指定し、Childでプレハブで指定する。

    /*
    public Vector3 MakeVector3(float x,float y,float z)
    {
        return new Vector3(x, y, z);
    }
    */
    public void changeCameraRotateLeft()
    {
        switch (cameraRotate)
        {
            case 0:
                cameraRotate = 1;
                break;
            case 1:
                cameraRotate = 2;
                break;
			case 2:
				cameraRotate = 3;
				break;
			case 3:
				cameraRotate = 0;
				break;
		}
    }
	public void changeCameraRotateRight()
	{
		switch (cameraRotate)
		{
			case 0:
				cameraRotate = 3;
				break;
			case 1:
				cameraRotate = 0;
				break;
			case 2:
				cameraRotate = 1;
				break;
			case 3:
				cameraRotate = 2;
				break;
		}
	}
 
   public  void CheckBubble()
    {
        bubble = GameObject.FindGameObjectsWithTag("bubble");
        bubbleNum = bubble.Length;      //Debug.Log("virusnum=" + virusnum);
    }
    public int GetNowFront()
    {
        return (int)nowFront;
    }
    public int GetBubble()
    {
        return bubbleNum;
    }
    public void se_destroy()
    {
        audioSource.PlayOneShot(SE_Destroy);
    }
}
