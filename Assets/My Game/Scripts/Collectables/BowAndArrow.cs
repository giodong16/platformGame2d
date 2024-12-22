using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAndArrow : MonoBehaviour
{
    Animator anim;
    public GameObject detailDialog;
    public ItemData itemData;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // mở khóa nút màn hình
            // mở khóa kĩ năng bắn cung
            // open bar mũi tên
            AudioManager.Instance?.PlaySFX(NameSound.CollectKey.ToString());
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                GameObject detailClone = Instantiate(detailDialog, canvas.transform);
                ItemDetailUI itemDetail = detailClone.GetComponent<ItemDetailUI>();
                if (itemDetail)
                {
                    itemDetail.UpdateItemDetail(itemData);
                    itemDetail.Show(true);
                }
            }

            Pref.BowSkill = 1;
            if (MobileControlUI.Instance != null) {
                MobileControlUI.Instance.LockBow();
            }
            if (GUIManager.Instance != null) { 
                GUIManager.Instance.UpdateArrowBar();
            }
          

            Destroy(gameObject);
        }
    }
    
}
