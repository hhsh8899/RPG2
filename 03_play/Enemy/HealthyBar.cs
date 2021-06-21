using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthyBar : MonoBehaviour
{
    Image healthBar;
 
   public WolfBaby baby;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
        healthBar.fillAmount = baby.GetHp();
    }
}
