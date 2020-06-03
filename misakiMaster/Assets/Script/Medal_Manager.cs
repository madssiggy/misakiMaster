using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medal_Manager : MonoBehaviour
{

    public GameObject bronzMedal;
    public GameObject silverMedal;
    public GameObject goldMedal;

    public int bronzNum;
    public int silverNum;
    public int goldNum;

    private int Cnum;
    // Start is called before the first frame update
    void Start()
    {
        Cnum = manager.GetbubbleNum();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cnum <= bronzNum) 
        {
            bronzMedal.SetActive(true);
        }

        else if (Cnum <= silverNum)
        {
            bronzMedal.SetActive(true);
            silverMedal.SetActive(true);
        }

        else if (Cnum <= goldNum)
        {
            bronzMedal.SetActive(true);
            silverMedal.SetActive(true);
            goldMedal.SetActive(true);
        }
    }
}
