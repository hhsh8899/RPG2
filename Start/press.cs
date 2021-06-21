using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class press : MonoBehaviour
{
    private Color HintColor=new Color(255,255,255);


    // Update is called once per frame
    void Update()
    {
        HintColor.a = Mathf.PingPong(0.25f*Time.time, 1F);
        GetComponent<Text>().color = HintColor;


    }
}
