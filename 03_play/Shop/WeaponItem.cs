using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    private int id;
    private ObjectInfo info;

    private UISprite icon_sprite;
    private UILabel name_label;
    private UILabel effect_label;
    private UILabel sellPrice_label;


    private void Awake()
    {
        icon_sprite = transform.Find("Icon").GetComponent<UISprite>();
        name_label = transform.Find("Describe/Name").GetComponent<UILabel>();
        effect_label = transform.Find("Describe/Effect").GetComponent<UILabel>();
        sellPrice_label = transform.Find("Describe/Price").GetComponent<UILabel>();

    }
    public void SetId(int id)
    {
        this.id = id;
        info = ObjectsInfo._instance.GetObjectInfoById(id);

        icon_sprite.spriteName = info.icon_name;
        name_label.text = info.name;
        if (info.attack > 0)
        {
            effect_label.text = "加伤害：" + info.attack;
        }
        if (info.def > 0)
        {
            effect_label.text = "加防御：" + info.def;
        }
        if (info.speed > 0)
        {
            effect_label.text = "加速度：" + info.speed;
        }
        sellPrice_label.text = info.price_sell.ToString();

    }
    public void OnBuyClick()
    {
        ShopWeapon._instance.OnBuyClick(id);
    }
}
