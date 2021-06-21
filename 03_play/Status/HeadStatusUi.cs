using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStatusUi : MonoBehaviour
{
    public static HeadStatusUi _instance;

    private UILabel name;
    private UISlider hpBar;
    private UISlider mpBar;

    private UILabel hpLable;
    private UILabel mpLable;
    private PlayerStatus playerStatus;


    private void Awake()
    {
        _instance = this;
        name = transform.Find("Name").GetComponent<UILabel>();
        hpBar = transform.Find("Hp").GetComponent<UISlider>();
        mpBar = transform.Find("Mp").GetComponent<UISlider>();

        hpLable = transform.Find("Hp/Thumb/Label").GetComponent<UILabel>();
       mpLable= transform.Find("Mp/Thumb/Label").GetComponent<UILabel>();
       

    }
    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateShow();
    }
    public void UpdateShow()
    {
        name.text = "LV." + playerStatus.level + " " + playerStatus.name;
        hpBar.value = playerStatus.hp_remain / playerStatus.hp;
        mpBar.value = playerStatus.mp_remain / playerStatus.mp;
       hpLable.text= playerStatus.hp_remain+"/"+ playerStatus.hp;
        mpLable.text = playerStatus.mp_remain + "/" + playerStatus.mp;
    }


}
