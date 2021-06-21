using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pressAny : MonoBehaviour
{
    private bool isPress;
    private GameObject container;
    // Start is called before the first frame update
    void Start()
    {
        container = transform.parent.Find("contoner").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPress == false)
        {
            if (Input.anyKey)
            {
                ShowButton();
            }
        }
    }
    void ShowButton()
    {
        gameObject.SetActive(false);
        container.SetActive(true);
        isPress = true;
    }
}
