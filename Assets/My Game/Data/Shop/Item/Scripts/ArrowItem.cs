using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArrowItem", menuName = "Shop/ArrowItem")]
public class ArrowItem : ItemData
{
    public override void Buy(int number = 1)
    {
        base.Buy(number);
        Pref.Arrows += number;
        HomeGUIManger.Instance?.UpdateArrowBowText();
    }
    public override bool isCanUse()
    {
        if( Pref.Arrows <=0 || Pref.BowSkill == 0 )
        {
            return false;
        }
        return true;
    }
    public override bool isCanSale()
    {
        if ( Pref.BowSkill == 0)
        {
            return false;
        }
        return true;
    }
}
