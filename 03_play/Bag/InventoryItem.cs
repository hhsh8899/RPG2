using UnityEngine;
using System.Collections;

public class InventoryItem : UIDragDropItem {

    public UISprite sprite;
    private int id;
    protected override void Awake() {
        base.Awake();
        sprite = this.GetComponent<UISprite>();
    }

    protected override void Update()
    {
        base.Update();
        if (isHover)
        {
            InventoryDes._instance.Show(id);

            if (Input.GetMouseButtonDown(1))
            {
                bool success = EquipmentUi._instance.Dress(id);
                if (success)
                {
                    transform.GetComponentInParent<InventoryItemGrid>().MinusNember();
                }
            }
        }

    }

    protected override void OnDragDropRelease(GameObject surface) {
      base.OnDragDropRelease(surface);
      if (surface != null) {
           if (surface.tag == Tags.inventory_item_grid) {//当拖放到了一个空的格子里面
              if (surface == this.transform.parent.gameObject) {//拖放到了自己的格子里面




               } else {
                  InventoryItemGrid oldParent = this.transform.parent.GetComponent<InventoryItemGrid>();

                   this.transform.parent = surface.transform;
                    ResetPosition();
                   InventoryItemGrid newParent = surface.GetComponent<InventoryItemGrid>();
                   newParent.SetId(oldParent.id, oldParent.num);

                    oldParent.ClearInfo();
                }

           } else if (surface.tag == Tags.inventory_item) {//当拖放到了一个有物品的格子里面
                InventoryItemGrid grid1 = this.transform.parent.GetComponent<InventoryItemGrid>();
               InventoryItemGrid grid2 = surface.transform.parent.GetComponent<InventoryItemGrid>();
               int id = grid1.id; int num = grid1.num;
               grid1.SetId(grid2.id, grid2.num);
               grid2.SetId(id, num);
            } else if (surface.tag == Tags.shortCut)
            {
                surface.GetComponent<ShortCutGrid>().SetInventory(id);
            }

        }

        ResetPosition();
    }

    void ResetPosition() {
        transform.localPosition = Vector3.zero;
    }

    public void SetId(int id) {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        sprite.spriteName = info.icon_name;
    }
    public void SetIconName(int id,string icon_name) {
        sprite.spriteName = icon_name;
        this.id = id;
    }
    private bool isHover = false;
    public void OnHoverOver()
    {
        isHover = true;
    }
    public void OnHoverOut()
    {
        isHover = false;
    }






}
