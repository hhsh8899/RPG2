using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public int id;
    private SkillInfo info;
    private UISprite iconNameSprite;
    private UILabel nameLable;
    private UILabel applyTypeLable;
    private UILabel desLable;
    private UILabel mpLable;

    private GameObject iconMask;

   public void UpdateShow(int level)
    {
        if (info.level <= level)
        {
            iconMask.SetActive(false);
            iconNameSprite.GetComponent<SkillItemIcon>().enabled = true;
        }
        else
        {
            iconMask.SetActive(true);
            iconNameSprite.GetComponent<SkillItemIcon>().enabled = false;
        }

    }
    void InitProperty()
    {
        iconNameSprite = transform.Find("Icon_name").GetComponent<UISprite>();
        nameLable = transform.Find("Property/NameBg/Name").GetComponent<UILabel>();
        applyTypeLable = transform.Find("Property/ApplyTypeBg/ApplyType").GetComponent<UILabel>();
        desLable = transform.Find("Property/DesBg/Des").GetComponent<UILabel>();
        mpLable = transform.Find("Property/MpBg/Mp").GetComponent<UILabel>();
        iconMask = transform.Find("IconMask").gameObject;
        iconMask.gameObject.SetActive(false);
    }
    public void SetId(int id)
    {
        InitProperty();
        this.id = id;
        info = SkillsInfo._instance.GetSkillInfoById(id);
        iconNameSprite.spriteName = info.icon_name;
        nameLable.text = info.name;
        switch (info.applyType)
        {
            case ApplyType.Buff:
                applyTypeLable.text = "增强";
                break;
            case ApplyType.Passive:
                applyTypeLable.text = "增益";
                break;
            case ApplyType.SingleTarget:
                applyTypeLable.text = "个体技能";
                break;
            case ApplyType.MultiTarget:
                applyTypeLable.text = "群体技能";
                break;
        }
        desLable.text = info.des;
        mpLable.text = info.mp + "MP";

    }

}
