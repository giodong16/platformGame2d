using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Dialog
{
    public GameObject shopItemUIPrefab;
    public ItemData[] itemDatas;
    public ItemDetailUI shopItemDetails;
    public Transform root;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (isShow)
        {
            CreateShopItemUIs();
        }
         
    }
    public void CreateShopItemUIs()
    {
        ClearRoot();

        foreach (ItemData itemData in itemDatas) {
            if (!itemData.isCanSale()) continue;
            GameObject newItem = Instantiate(shopItemUIPrefab,root);
            ShopItemUI shopItemUI = newItem.GetComponent<ShopItemUI>();
            if (shopItemUI != null) { 
                shopItemUI.SetUpUI(itemData);
                shopItemUI.imageButton.onClick.AddListener(()=> OnShopItemUIClick(itemData));
            }
        }
    }
    public void ClearRoot()
    {
        foreach (Transform child in root)
        {
            Destroy(child.gameObject);
        }
    }
    public void OnShopItemUIClick(ItemData itemData)
    {
        if (shopItemDetails == null) return;
        shopItemDetails.UpdateItemDetail(itemData);
    }
}
