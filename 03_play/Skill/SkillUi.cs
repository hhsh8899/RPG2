using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUi : MonoBehaviour
{
    public static SkillUi _instance;
    private TweenPosition tween;
    private bool isShow = false;


    public int[] magicianSkillIdList;
    public int[] swordmanSkillIdList;
    private PlayerStatus playerStatus;
    public UIGrid grid;
    public GameObject skillItemPrefab;
    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
    }
    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        int[] idList=null;
        switch (playerStatus.heroType) {
            case HeroType.Magician:
                idList = magicianSkillIdList;
                break;
            case HeroType.Swordman:
                idList = swordmanSkillIdList;
                break;
        
        }
        foreach(int id in idList)
        {
            GameObject itemGo = NGUITools.AddChild(grid.gameObject, skillItemPrefab);
            grid.AddChild(itemGo.transform);
            itemGo.GetComponent<SkillItem>().SetId(id);
        }

    }
    public void TransformState()
    {
        if (isShow == false)
        {
            tween.PlayForward();
            isShow = true;
            UpdateShow();
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }
    void UpdateShow()
    {
        SkillItem[] items = this.GetComponentsInChildren<SkillItem>();
        foreach(SkillItem item in items)
        {
            item.UpdateShow(playerStatus.level);
        }

    }
}
