using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIn : MonoBehaviour
{
    RectTransform meganepos;
    Vector3 Pos;
    public float Speed;
    public float Accel;
    public float CutInOutPos;
    // Start is called before the first frame update
    void Start()
    {
        meganepos = GetComponent<RectTransform>();
        Pos = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator CutInMode()
    {
        float Move = Speed;
        float x=CutInOutPos;
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
        } while (x >= -CutInOutPos);
        Destroy(GameObject.Find("CutInBoard"));
        Destroy(this.gameObject.transform.root.gameObject);
        
        yield break;
    }
    public void DestroyUI()
    {
        Destroy(this.gameObject);
    }
}
