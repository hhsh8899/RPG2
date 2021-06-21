using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory _instance;

    private TweenPosition tween;
    private int coinCount = 1000;//�������

    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    public UILabel coinNumberLabel;
    public GameObject inventoryItem;

    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetId(Random.Range(1001, 1004));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetId(Random.Range(2001, 2023));
        }


    }

    //ʰȡ��id����Ʒ������ӵ���Ʒ������
    //����ʰȡ��Ʒ�Ĺ���
    public void GetId(int id, int count = 1)
    {
        //��һ���ǲ��������е���Ʒ���Ƿ���ڸ���Ʒ
        //�ڶ� ������ڣ���num +1

        InventoryItemGrid grid = null;
        foreach (InventoryItemGrid temp in itemGridList)
        {
            if (temp.id == id)
            {
                grid = temp; break;
            }
        }
        if (grid != null)
        {//���ڵ���� 
            grid.PlusNumber(count);
        }
        else
        {//�����ڵ����
            foreach (InventoryItemGrid temp in itemGridList)
            {
                if (temp.id == 0)
                {
                    grid = temp; break;
                }
            }
            if (grid != null)
            {//���� ���������ڣ����ҿյķ���Ȼ����´�����Inventoryitem�ŵ�����յķ�������
                GameObject itemGo = NGUITools.AddChild(grid.gameObject, inventoryItem);
                itemGo.transform.localPosition = Vector3.zero;
                itemGo.GetComponent<UISprite>().depth = 4;
                grid.SetId(id, count);
            }
        }
    }

    private bool isShow = false;

    void Show()
    {
        isShow = true;
        tween.PlayForward();
    }

    void Hide()
    {
        isShow = false;
        tween.PlayReverse();
    }


    public void TransformState()
    {// ת��״̬
        if (isShow == false)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    public bool GetCoin(int count)
    {
        if (coinCount >= count)
        {
            coinCount -= count;
            coinNumberLabel.text = coinCount.ToString();
            return true;
        }
        return false;
    }
    public void AddCoin(int count)
    {
        coinCount += count;
        coinNumberLabel.text = coinCount.ToString();
    }

    public bool MinusId(int id,int count=1)
    {
        InventoryItemGrid grid = null;
        foreach (InventoryItemGrid temp in itemGridList)
        {
            if (temp.id == id)
            {
                grid = temp; break;
            }
        }
        if (grid == null)
        {
            return false;
        }
        else
        {
            bool isSuccess = grid.MinusNember(count);
            return isSuccess;
        }
    }
}
