using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctionBar : MonoBehaviour
{
   public void OnStatusButtonClick()
    {
        Status._instance.TransformState();
    }
    public void OnBagButtonClick()
    {
        Inventory._instance.TransformState();
    }
    public void OnEquipButtonClick()
    {
        EquipmentUi._instance.TransformState();
    }
    public void OnSkillButtonClick()
    {
        SkillUi._instance.TransformState();
    }
    public void OnSettingButtonClick()
    {
        SettingUI._instance.TransformState();
    }
}
