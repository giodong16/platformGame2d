using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeGUIManger : MonoBehaviour
{
    public static HomeGUIManger Instance;
    public Text coinText;
    public Text heartText;
    public Text stoneText;
    public Text arrowBowText;
    public GameObject arrowBowStatusBar;
    [Header("Slider Setting:")]
    public Slider musicSlider;
    public Slider sfxSlider;


    public GameObject messageDialogPrefabs;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        UpdateCoins();
        UpdateHeart();
        UpdateStone();
        UpdateArrowBowText();
        SetVolumeMusic();
        SetVolumeSFX();
    }
    public void UpdateCoins()
    {
        coinText.text = Pref.Coins.ToString();
    }
    public void UpdateHeart()
    {
        heartText.text = GameManager.Instance.HeartForLevel.ToString();
    }
    public void UpdateStone()
    {
        stoneText.text = Pref.Stones.ToString();
    }
    public void UpdateArrowBowText()
    {
        if(arrowBowStatusBar == null) return;
        if(Pref.BowSkill ==1)
        {
            arrowBowStatusBar.SetActive(true);
            arrowBowText.text = Pref.Arrows.ToString();
            
        }
        else
        {
            arrowBowStatusBar.SetActive(false);
        }
       
    }
    public void SetVolumeSFX()
    {
        sfxSlider.value = Pref.VolumeSFX;
    }
    public void SetVolumeMusic()
    {
        musicSlider.value = Pref.VolumeMusic;
    }
    public void CreateMessage(string title, string message)
    {
        if (messageDialogPrefabs == null) return;
        if (title != null && message != null)
        {
            AudioManager.Instance?.PlaySFX(NameSound.Message.ToString());
            GameObject canvas = GameObject.Find("Canvas");
            if(canvas == null) return ;
            GameObject mesDialog = Instantiate(messageDialogPrefabs,canvas.transform);
            Dialog dialog =  mesDialog.GetComponent<Dialog>();
            if (dialog != null) { 
                dialog.UpdateDialog(title, message);
            }
        }
    }
}
