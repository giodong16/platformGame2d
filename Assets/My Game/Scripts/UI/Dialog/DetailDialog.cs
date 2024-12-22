using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailDialog : Dialog
{
    ItemData itemData;
    public Text nameText;
    public Text descriptionText;
    public Image imageItem;
    public void UpdateDetail(ItemData itemData)
    {
        ClearItemDetails();
        nameText.text = itemData.itemName;
        descriptionText.text = itemData.description;
        imageItem.sprite = itemData.itemSprite;
        Show(true);
    }
    public void ClearItemDetails()
    {
        nameText.text = "";
        descriptionText.text = "";
        imageItem.sprite = null;
    }
}
