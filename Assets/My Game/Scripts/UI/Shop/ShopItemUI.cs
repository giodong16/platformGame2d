using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image imageItem;
    public Text priceText;
    public Button imageButton;
    public Button buyButton;

    private ItemData currentItemData;
    public void SetUpUI(ItemData item ) //ShopItem item
    {
        currentItemData = item;
        imageItem.sprite = item.itemSprite;
        priceText.text = item.price.ToString();
        buyButton.onClick.AddListener(()=> BuyItem(item));
    }
    public void BuyItem(ItemData item)//ShopItem item
    {
        if(Pref.Coins < item.price) return;
        item.Buy(1);
        //tạm làm lẹ trường hợp đặc biệt
        if (item.IsUniqueItem())
        {
            Destroy(gameObject);
        }

    }

}
[System.Serializable]
public class ShopItem
{
    public ItemShortName itemShortName;
    public string name;
    public string description;
    public Sprite sprite;
    public int price;

    public ShopItem(ItemShortName itemShortName, string name, string description, Sprite sprite, int price)
    {
        this.itemShortName = itemShortName;
        this.name = name;
        this.description = description;
        this.sprite = sprite;
        this.price = price;
    }
}
public enum ItemShortName
{
    Heart,
    Stone,
    Arrow

}