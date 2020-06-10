using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubbleScript : MonoBehaviour
{
    public bool canDestroy;//colでon/off
    public bool isDestroy;//消える前にtrueになってTouchがスライム生成
    // Start is called before the first frame update
    void Start()
    {
        canDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator BubbleMove(Vector3 Move)
    {
        Debug.Log("バブルムーブ起動====================");
        //スライム触れるところまで動かす
        do {
            gameObject.transform.position += Move;
            //触れてみて、消せるのであればtrue
            if (canDestroy == true) {
                yield break;
            }
            yield return null;
        } while (canDestroy == false);

        //触れて同じタグでなければスライムを元の位置に戻す
        //do {
        //    gameObject.transform.position -= Move;
        //} while (transform.position == GameObject.Find("TouchManager").GetComponent<Touch>().GetStartObj());
        yield break;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == gameObject.tag) {
            canDestroy = true;
        } else {
            canDestroy = false;
        }
      //  GameObject.Find("StageManager").GetComponent<manager>().isBubbleDestroy = canDestroy;
    }
}

