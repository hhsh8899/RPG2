using UnityEngine;
using System.Collections;

public class InventoryItemGrid : MonoBehaviour {

    public  int id=0;
    private ObjectInfo info = null;
    public int num = 0;

    public UILabel numLabel;

	// Use this for initialization
	void Start () {
        numLabel = this.GetComponentInChildren<UILabel>();
	}

    public void SetId(int id, int num = 1) {
        this.id = id;
        info = ObjectsInfo._instance.GetObjectInfoById(id);
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        item.SetIconName(id,info.icon_name);
        numLabel.enabled = true;
        this.num = num;
        numLabel.text = num.ToString();
    }

    public void PlusNumber(int num = 1) {
        this.num += num;
        numLabel.text = this.num.ToString();
    }
    public bool MinusNember(int number=1)
    {
        if (this.num >= number)
        {
            this.num -= number;
            numLabel.text = this.num.ToString();
            if (this.num == 0)
            {
                ClearInfo();
                GameObject.Destroy(GetComponentInChildren<InventoryItem>().gameObject);
              
            }
            return true;
        }
        return false;
    }

    //清空 格子存的物品信息
    public void ClearInfo() {
        id = 0;
        info = null;
        num = 0;
        numLabel.enabled = false;
    }


}
