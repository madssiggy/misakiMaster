using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIn : MonoBehaviour
{
    RectTransform meganepos;
    Vector3 Pos;
    public float Speed;
    public float Accel;
    // Start is called before the first frame update
    void Start()
    {
        meganepos = GetComponent<RectTransform>();
        Pos = new Vector3(0, 0, 0);
        StartCoroutine(CutInMode());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
         
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            Pos.x +=Speed;
        }

 
        StartCoroutine(CutInMode());
    }
    IEnumerator CutInMode()
    {
        float Move = Speed;
        float x=300f;
        do {
            x -= Move;
            meganepos.localPosition =new Vector3(x,0,0);
            Move += Accel;
            yield return null;
        } while (x >= 0f);
        x = 0;
        int time = 150;
        do {
            time -= 1;
            yield return null;
        } while (time >0);
        Move = Speed;
        do {
            x -= Move ;
            meganepos.localPosition = new Vector3(x, 0, 0);
            Move += Accel;
            yield return null;
        } while (x >= -300f);
        Destroy(GameObject.Find("CutInBoard"));
        Destroy(this.gameObject.transform.root.gameObject);
        
        yield break;
    }
}
