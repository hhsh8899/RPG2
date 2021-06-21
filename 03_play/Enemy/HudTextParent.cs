using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudTextParent : MonoBehaviour
{
    public static HudTextParent _instance;

    private void Awake()
    {
        _instance = this;
    }
}
