using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : Dialog
{
    public Text nameText;
    public Text descriptionText;
    public Image imageItem;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (!isShow)
        {
            ClearItemDetails();
        }
        if (isShow && anim != null)
        {
            anim.SetBool("IsAppear",true);
        } 
    }
    public override void Close()
    {
        base.Close();
        ClearItemDetails();
    }
    public void UpdateItemDetail(ItemData itemData)
    {
        ClearItemDetails();
        nameText.text = itemData.itemName;
        descriptionText.text =itemData.description;
        imageItem.sprite =itemData.itemSprite;
        Show(true);
    }
    public void ClearItemDetails()
    {
        nameText.text = "";
        descriptionText.text = "";
        imageItem.sprite = null;
    }
}
