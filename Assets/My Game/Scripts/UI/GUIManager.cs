using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    [Header("Dialog:")]
    public Dialog pauseDialog;
    public Dialog gameoverDialog;
    public Dialog winDialog;
    public RequirementAndTasksDialog requirementAndTasksDialog;
    public int gemHideCount = 0;

    private Dialog currentDialog;
    [Header("Bar")]
 //   public GameObject stonesBar;
    public GameObject arrowsBar;

    [Header("Text:")]
   // public Text levelText;
    public Text heartCountText;
    public Text stonesText;
    public Text arrowsText;
    public Text gemsText;
    public Text textCoin;

    [Header("Image:")]
    public Image keyImage;
    public Animator UIKeyAnim;
    public Image heartFill;

    [Header("Slider Setting:")]
    public Slider musicSlider;
    public Slider sfxSlider;

    public GameObject messageDialogPrefabs;

    public Dialog CurrentDialog { get => currentDialog; set => currentDialog = value; }
    public bool isSpecical = false;

    public  void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
     //   base.Awake();
    }
    public void Start()
    {
        GameManager.Instance?.ResetData();
        if (!isSpecical)
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.IsHadKey = false;
                UpdateHeartBar(GameManager.Instance.CurrentHP, GameManager.Instance.TotalHP);
            }
           
            CalculateTotalGemForLevel();
            UpdateHeartText();
            UpdateStoneBar();
            UpdateArrowBar();
            UpdateGemsText();

            SetVolumeMusic();
            SetVolumeSFX();
        }    
        StartCoroutine(DelayShowRequirementAndTasksDialog());
        
    } 
    //BAR
    public void UpdateHeartText()
    {
        if (heartCountText != null)
            if(GameManager.Instance)
                heartCountText.text = GameManager.Instance.HeartForLevel.ToString();
    }
    public void UpdateHeartBar(int currentHP, int totalHP)
    {
        if (heartCountText != null)
        {
            heartFill.fillAmount = (float)currentHP / totalHP;
        }
    }
    public void UpdateTextCoin(int coin)
    {
        if (textCoin == null) return;
        textCoin.text = "x" + coin;
    }

    public void UpdateStoneBar()
    {
        if(stonesText != null)
        {
            stonesText.text = "x"+ Pref.Stones.ToString();
            if (Pref.Stones <=0)
                StartCoroutine(Warning(stonesText));
        }
        
    }

    public void UpdateArrowBar()
    {
        if(arrowsBar == null || arrowsText == null) return;
        if(Pref.BowSkill == 0) { 
            arrowsBar.SetActive(false);
        }
        else
        {
            arrowsBar.SetActive(true);
            arrowsText.text = "x"+ Pref.Arrows.ToString();
            if (Pref.Arrows <= 0)
                StartCoroutine(Warning(stonesText));
        }
        
    }

    public void UpdateGemsText()
    {
        if (gemsText != null && GameManager.Instance)
        {
            gemsText.text = GameManager.Instance.gemForLevel.ToString() + "/" + GameManager.Instance.toltaGemForLevel;
        }
    }
    // KEY
    public void ShowKey(bool isShow)
    {
        if (UIKeyAnim != null)
        {
            UIKeyAnim.SetBool("IsHadKey", isShow);
        }
       /* if (isShow)
        {
            // keyImage.color = Color.white;
            
        }
        else 
        { 
            keyImage.color = Color.black;
        }*/
    }

    //AUDIO
    public void UpdateVolumeMusic()
    {
        if(AudioManager.Instance != null && musicSlider!=null)
        {
            AudioManager.Instance.MusicVolume(musicSlider.value);
        }
    }
    public void UpdateVolumeSFX()
    {
        if (AudioManager.Instance != null && sfxSlider!=null)
        {
            AudioManager.Instance.SFXVolume(sfxSlider.value);
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


    //STARS
    public void CalculateTotalGemForLevel()
    {
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
        if (GameManager.Instance)
        {
            GameManager.Instance.toltaGemForLevel =  gemHideCount;
            if (gems != null && gems.Length > 0)
            {
                if (GameManager.Instance)
                {
                    GameManager.Instance.toltaGemForLevel += gems.Length;
                }
            }
        }
        
    }

    //DIALOG
    public void ShowGameoverDialog()
    {
        HideCurrentDialog();
        currentDialog = gameoverDialog;
        gameoverDialog.Show(true);
    }

    public void ShowWinDialog()
    {
        HideCurrentDialog();
        currentDialog = winDialog;
        winDialog.Show(true);
    }

    public void ShowGamePause()
    {
        HideCurrentDialog();
        currentDialog = pauseDialog;
        pauseDialog.Show(true);
    }
    public void ShowRequirementAndTasksDialog()
    {
        HideCurrentDialog();
        if (requirementAndTasksDialog != null)
        {
            currentDialog = requirementAndTasksDialog;
            requirementAndTasksDialog.Show(true);
        }
    
    }
    IEnumerator DelayShowRequirementAndTasksDialog()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance?.PlaySFX(NameSound.RequireDialog.ToString());
        ShowRequirementAndTasksDialog();
    }


    public void HideCurrentDialog()
    {
        if (currentDialog != null)
        {
            currentDialog.Show(false);
        }
    }
    public void CreateMessage(string title, string message)
    {
        if (messageDialogPrefabs == null) return;
        if (title != null && message != null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas == null) return;
            GameObject mesDialog = Instantiate(messageDialogPrefabs, canvas.transform);
            Dialog dialog = mesDialog.GetComponent<Dialog>();
            if (dialog != null)
            {
                dialog.UpdateDialog(title, message);
            }
        }
    }

   // public void 
    IEnumerator Warning(Text text)
    {
        text.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        text.color = Color.white;
    }
}
