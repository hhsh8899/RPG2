using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public GameObject magicianPrefab;
    public GameObject swordmanPrefab;
    private void Awake()
    {
        int selectIndex= PlayerPrefs.GetInt("SelectedCharacterIndex");
        string name= PlayerPrefs.GetString("Name");
        GameObject go = null;
        if (selectIndex == 0)
        {
            go = GameObject.Instantiate(swordmanPrefab);     
        }else if (selectIndex == 1)
        {
            go = GameObject.Instantiate(magicianPrefab);
        }
        go.GetComponent<PlayerStatus>().name = name;
    }
}
