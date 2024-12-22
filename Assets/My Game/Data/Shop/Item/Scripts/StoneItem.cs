using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewStoneItem",menuName ="Shop/StoneItem")]
public class StonesItem : ItemData
{
    public override void Buy(int number = 1)
    {
        base.Buy(number);
        Pref.Stones += number;
        HomeGUIManger.Instance?.UpdateStone();
    } 
    public override bool isCanUse()
    {
        if (Pref.Stones <= 0)
        {
            return false;
        }
        return true;
    }

}
