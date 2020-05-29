using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlphaChange : MonoBehaviour
{
    public bool Flag;
    public float Alpha;
    private float red;
    private float green;
    private float blue;

    void Start()
    {
        // 色情報の取得
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        Alpha = GetComponent<Image>().color.a;

        Flag = true;    
    }
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, Alpha);

        if (Flag)
        {
            Alpha -= 0.01f;
            if(Alpha <= 0.3f)
            {
                Flag = false;
            }
        }
        else if(!Flag)
        {
            Alpha += 0.01f;
            if (Alpha >= 1.0f)
            {
                Flag = true;
            }
        }
    }
}
