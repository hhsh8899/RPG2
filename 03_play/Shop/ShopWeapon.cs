using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeapon : MonoBehaviour
{
    public static ShopWeapon _instance;
    private TweenPosition tween;
    private bool isShow=false;
    public int[] weaponIdArray;
    public UIGrid grid;
    public GameObject ItemPrefab;
    private GameObject numberDialog;
    private UIInput numberInput;
    private int buyId = 0;
    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
        numberDialog = transform.Find("NumberDialog").gameObject;
        numberInput = transform.Find("NumberDialog/NumberInput").GetComponent<UIInput>();
        numberDialog.SetActive(false);
    }
    private void Start()
    {
        InitWeaponShop();
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
    public void OnCloseBtnClick()
    {
        TransformState();
    }
    void InitWeaponShop()
    {
        foreach(int id in weaponIdArray)
        {
            GameObject itemGo = NGUITools.AddChild(grid.gameObject, ItemPrefab);
            grid.AddChild(itemGo.transform);
            itemGo.GetComponent<WeaponItem>().SetId(id);
        }
    }
    public void OnBuyClick(int id)
    {
        buyId = id;
        numberDialog.SetActive(true);
        numberInput.value = "0";
    }
    public void OnOkButton()
    {
        int count = int.Parse(numberInput.value);
        if (count > 0)
        {
            int price = ObjectsInfo._instance.GetObjectInfoById(buyId).price_buy;
            int total_price = price * count;
            bool success = Inventory._instance.GetCoin(total_price);
            if (success)
            {
                Inventory._instance.GetId(buyId,count);
            }
        
        }
        buyId = 0;
        numberInput.value = "0";
        numberDialog.SetActive(false);

    }
    public void OnClick()
    {
        numberDialog.SetActive(false);
    }
}
