using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrugNpc : NPC
{
    private AudioSource clip;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clip = GetComponent<AudioSource>();
            clip.Play();
            ShopDrug._instance.TransformState();
        }
    }
}
