using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class MobileControlUI : MonoBehaviour
{
    public static MobileControlUI Instance;
    [Header("Skill button:")]
    public Button punchButton;
    public Button swordButton;
    public Button throwButton;
    public Button bowButton;

    [Header("Skill image:")]
    public Image swordImage;
   // public Image throwImage;
    public Image bowImage;
 //   public Image punchImage;

    [Header("Skill sprite:")]
    public Sprite lockSprite;
    public Sprite swordSprite;
  //  public Sprite throwSprite;
    public Sprite bowSprite;
    //public Sprite punchSprite;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LockBow();
        LockSword();
    }

    public void LockBow()
    {
        if(Pref.BowSkill == 0)
        {
            bowImage.sprite = lockSprite;
            bowButton.interactable = false;
            bowButton.GetComponent<OnScreenButton>().enabled = false;
        }
        else
        {
            bowImage.sprite = bowSprite;
            bowButton.interactable = true;
            bowButton.GetComponent<OnScreenButton>().enabled = true;
        }
    }
    public void LockSword()
    {
        if (Pref.SwordSkill == 0) 
        { 
            swordImage.sprite = lockSprite;
            swordButton.interactable = false;
            swordButton.GetComponent<OnScreenButton>().enabled = false;
        }
        else
        {
            swordImage.sprite = swordSprite;
            swordButton.interactable = true;
            swordButton.GetComponent<OnScreenButton>().enabled = true;
        }
    }
}
