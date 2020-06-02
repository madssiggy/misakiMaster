using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchKaisuu : MonoBehaviour
{
    Text boardText;
    Touch touchScript;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        boardText = this.GetComponent<Text>();
       touchScript = GameObject.Find("TouchManager").GetComponent<Touch>();
    }

    // Update is called once per frame
    void Update()
    {
        touchScript = GameObject.Find("TouchManager").GetComponent<Touch>();
  
        boardText.text = ""+(score-touchScript.touchNum);
    }
}
