using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUi : MonoBehaviour
{
    public static EquipmentUi _instance;
    private TweenPosition tween;
    private bool isShow = false;
    private GameObject headgear;
    private GameObject armor;
    private GameObject rightHand;
    private GameObject leftHand;
    private GameObject shoe;
    private GameObject accessory;
    private PlayerStatus playerStatus;
    public GameObject equipmentItem;

    public int attack=0;
    public int def=0;
    public int speed=0;

    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
        headgear = transform.Find("Headgear").gameObject;
        armor = transform.Find("Armor").gameObject;
        rightHand = transform.Find("Right-Hand").gameObject;
        leftHand = transform.Find("Left-Hand").gameObject;
        shoe = transform.Find("Shoe").gameObject;
        accessory = transform.Find("Accessory").gameObject;
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();

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

    public bool Dress(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        if (info.type != ObjectType.Equip)
        {
            return false;
        }
        if (playerStatus.heroType == HeroType.Magician)
        {
            if (info.applicationType == ApplicationType.Swordman)
            {
                return false;
            }
        }
        if (playerStatus.heroType == HeroType.Swordman)
        {
            if (info.applicationType == ApplicationType.Magician)
            {
                return false;
            }
        }

        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.LeftHand:
                parent = leftHand;
                break;

            case DressType.RightHand:
                parent = rightHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;


        }

        EquipmentItem item = parent.GetComponentInChildren<EquipmentItem>();
        if (item != null)
        {
            Inventory._instance.GetId(item.id);
            item.SetInfo(info);
        }
        else
        {
            GameObject itemGo = NGUITools.AddChild(parent, equipmentItem);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<EquipmentItem>().SetInfo(info);
        }

        UpdateProperty();
        return true;
    }
    public void TakeOff(int id,GameObject go)
    {
        Inventory._instance.GetId(id);
        GameObject.Destroy(go);
        UpdateProperty();
    }

    void UpdateProperty()
    {
        this.attack = 0;
        this.def = 0;
        this.speed = 0;

        EquipmentItem headgearItem = headgear.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgearItem);
        EquipmentItem armorItem = armor.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgearItem);
        EquipmentItem leftHandItem = leftHand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgearItem);
        EquipmentItem rightHandItem = rightHand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgearItem);
        EquipmentItem shoeItem = shoe.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgearItem);
        EquipmentItem accessoryItem = accessory.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgearItem);
    }
    void PlusProperty(EquipmentItem item)
    {
        if (item != null)
        {
            ObjectInfo equipInfo = ObjectsInfo._instance.GetObjectInfoById(item.id);
            this.attack = equipInfo.attack;
            this.def = equipInfo.def;
            this.speed = equipInfo.speed;
        }
    }
}
