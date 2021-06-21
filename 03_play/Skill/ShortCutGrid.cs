using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum ShortCutType
{
    Skill,
    Drug,
    None
}

public class ShortCutGrid : MonoBehaviour
{
    public KeyCode keyCode;
    private UISprite icon;
    private ShortCutType type = ShortCutType.None;
    private int id;
    private SkillInfo info;
    private ObjectInfo objectInfo;
    private PlayerStatus playerStatus;
    private PlayerAttack playerAttack;
    
    private void Awake()
    {
        
        icon = transform.Find("Icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
        playerAttack = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
    }


    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (type == ShortCutType.Drug) {
                OnDrugUse();
            }
            else if(type==ShortCutType.Skill)
            {
                bool success = playerStatus.TakeMp(info.mp);
                if (success == false)
                {
                    //什么都不做
                }
                else
                {
                    //释放技能
                    playerAttack.UseSkill(info);
                }


            }
        }
    }
    public void SetSkill(int id)
    {
        this.id = id;
        this.info = SkillsInfo._instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = info.icon_name;
        type = ShortCutType.Skill;

    }
    public void SetInventory(int id)
    {
        this.id = id;
        this.objectInfo = ObjectsInfo._instance.GetObjectInfoById(id);
        if (objectInfo.type == ObjectType.Drug)
        {
            icon.gameObject.SetActive(true);
            icon.spriteName = objectInfo.icon_name;
            type = ShortCutType.Drug;
        }
    }
    private bool success = false;
    public void OnDrugUse()
    {
         success = Inventory._instance.MinusId(id,1);
        if (success)
        {
            playerStatus.GetDrug(objectInfo.hp, objectInfo.mp);
        }
        else if(success==false)
        {

            type = ShortCutType.None;
            icon.gameObject.SetActive(false);
            id = 0;
            info = null;
            objectInfo = null;

        }
    }
  
}
