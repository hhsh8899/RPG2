using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    public static SettingUI _instance;
    private TweenPosition tween;
    private bool isShow = false;
    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
    }
    public void TransformState()
    {
        if (isShow == false)
        {
            tween.PlayForward();
            isShow = true;
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }
    public void ExitGame()
    {
        //Ԥ����
#if UNITY_EDITOR    //�ڱ༭��ģʽ��
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void MainMenu()
    {
        Application.LoadLevel("StartScene");
    }
}
