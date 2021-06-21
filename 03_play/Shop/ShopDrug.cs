using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrug : MonoBehaviour
{
    public static ShopDrug _instance;
    private TweenPosition tween;
    private bool isShow = false;

    private UIInput numberInput;
    private GameObject numberDialog;
    private int buy_id;
    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
        numberDialog = transform.Find("NumberDialog").gameObject;
        numberInput = transform.Find("NumberDialog/NumberInput").GetComponent<UIInput>();
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
    public void OnCloseButtonClick()
    {
        TransformState();
    }
    public void OnBuyId1001()
    {
        Buy(1001);
    }
    public void OnBuyId1002()
    {
        Buy(1002);
    }

    public void OnBuyId1003()
    {
        Buy(1003);
    }
    public void OnOkClick()
    {
        int count = int.Parse(numberInput.value);
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(buy_id);
        int price = info.price_buy;
        int price_total = price * count;
        bool success = Inventory._instance.GetCoin(price_total);
        if (success)
        {
            if (count > 0) { Inventory._instance.GetId(buy_id, count); }
            
        }
        numberDialog.SetActive(false);
    }
    void Buy(int id)
    {
        ShowNumberDialog();
        buy_id = id;
    }
    void ShowNumberDialog()
    {
        numberDialog.SetActive(true);
        numberInput.value = "0";
    }
    






}
