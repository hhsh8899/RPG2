using UnityEngine;
using System.Collections;
using UnityEditor;

public class ButtonContainer : MonoBehaviour {

   
    public void OnNewGame() {
        PlayerPrefs.SetInt("DataFromSave", 0);
        Application.LoadLevel("CharacterCreatScene");
    }
   
    public void OnLoadGame() {
        PlayerPrefs.SetInt("DataFromSave", 1);
        Application.LoadLevel("03_play");
    }
    public void ExitGame()
    {
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
