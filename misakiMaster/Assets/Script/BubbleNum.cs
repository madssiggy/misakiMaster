using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleNum : MonoBehaviour
{
    Text boardText;
    manager managerScript;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        boardText = this.GetComponent<Text>();
        managerScript = GameObject.Find("StageManager").GetComponent<manager>();
        boardText.text = "" + (managerScript.bubbleNum);
    }

    // Update is called once per frame
    void Update()
    {
        managerScript = GameObject.Find("StageManager").GetComponent<manager>();

        boardText.text = "" + (managerScript.bubbleNum);
    }
}
