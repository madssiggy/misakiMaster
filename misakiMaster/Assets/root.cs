using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class root : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            GameObject tmp = gameObject.transform.root.gameObject;

            Debug.Log("根＝" + tmp +"からみたローカル位置"+gameObject.transform.localPosition);
            Debug.Log("根＝" + tmp + "からみたworld位置" + gameObject.transform.position);

            Debug.Log("根のworld位置" + tmp.transform.position );
            Debug.Log("根のlocal位置"  + tmp.transform.localPosition);

        } else if (Input.GetKeyDown(KeyCode.Backspace)) {
            GameObject tmp = gameObject.transform.parent.gameObject;

            Debug.Log("親＝" + tmp+ "からみたローカル位置" + gameObject.transform.localPosition);
            Debug.Log("親＝" + tmp + "からみたworld位置" + gameObject.transform.position);

            Debug.Log("親のworld位置" + tmp.transform.position);
            Debug.Log("親のlocal位置" + tmp.transform.localPosition);
        }
    }
}
