using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewSwordItem",menuName ="Shop/SwordItem")]
public class SwordItem : ItemData
{
    public override void Buy(int number = 1)
    {
        base.Buy(number);
        Pref.SwordSkill = 1;
    }
    public override bool isCanUse()
    {
        if ( Pref.SwordSkill == 0)
        {
            return false;
        }
        return true;
    }
    public override bool isCanSale()
    {
        if (Pref.SwordSkill == 1)
        {
            return false;
        }
        return true;
    }
    public override bool IsUniqueItem()
    {
        return true;
    }
}
