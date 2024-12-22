using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData",menuName ="Shop/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemSprite;
    public int price;
    public virtual void Buy(int number = 1)
    {
        Pref.Coins-= price;
        HomeGUIManger.Instance?.UpdateCoins();
    }
    public virtual void UseItem()
    {
    }
    public virtual bool isCanUse()
    {
        return true;
    }
    public virtual bool isCanSale()
    {
        return true;
    }
    public virtual bool IsUniqueItem()
    {
        return false;
    }
}
