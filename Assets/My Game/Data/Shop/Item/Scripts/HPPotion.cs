using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHPPotionItem", menuName = "Shop/HPPotionItem")]
public class HPPotion : ItemData
{
    public override bool isCanUse()
    {
        if (Pref.HPPotion <= 0)
        {
            return false;
        }
        return true;
    }
}
