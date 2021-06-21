using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum HeroType{
    Swordman,
        Magician
}
public class PlayerStatus : MonoBehaviour
{
    public HeroType heroType;
    public string name = "香如故";
    public int hp=10000;//hp最大值
    public float hp_remain = 10000;//hp剩余的
    public float mp_remain = 10000;
    public int mp=10000;
    public int level=1;//100+level*30
    public int coin=200;
    public float exp = 0;

    public float attack = 20;
    public float def = 20;
    public float speed = 20;

    public int attack_plus=0;
    public int def_plus=0;
    public int speed_plus=0;

    public int point_remain = 0;

    public void GetExp(int exp)
    {
        this.exp += exp;
        int total_exp = 100 + level * 30;
       while (this.exp >= total_exp)
        {
            this.level++;
            this.exp -= total_exp;
            total_exp = 100 + level * 30;

        }
        ExpBar._instance.SetValue(this.exp / total_exp);

    }
    private void Start()
    {
        GetExp(0);
    }
    public void GetDrug(int hp,int mp)
    {
        hp_remain += hp;
        mp_remain += mp;
        if (hp_remain > this.hp)
        {
            hp_remain = this.hp;
        }
        if (mp_remain > this.mp)
        {
            mp_remain = this.mp;
        }
        HeadStatusUi._instance.UpdateShow();
    }

    public void AddCoin(int count)
    {
        coin += count;
    }
    public bool GetPoint(int point=1)
    {
        if (point_remain >= point)
        {
            point_remain -= point;
            return true;
        }
        return false;

    }
    public bool TakeMp(int count)
    {
        if (mp_remain >= count)
        {
            mp_remain -= count;
            HeadStatusUi._instance.UpdateShow();
            return true;
        }
        else
        {
            return false;
        }

    }
}
