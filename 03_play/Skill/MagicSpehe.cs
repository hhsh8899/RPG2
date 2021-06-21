using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpehe : MonoBehaviour
{
    public float attack = 0;
    private List<WolfBaby> wolfList = new List<WolfBaby>();
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.enemy)
        {
            WolfBaby baby = other.GetComponent<WolfBaby>();
            int index = wolfList.IndexOf(baby);
            if (index == -1)
            {
                baby.TakeDamage((int)attack);
                wolfList.Add(baby);
            }
        }
    }
}
