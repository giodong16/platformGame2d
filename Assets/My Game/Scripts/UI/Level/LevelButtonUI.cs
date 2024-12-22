using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    public Button levelButton;
    private int m_id;
    public Image[] starImage;
    public int Id { get => m_id; set => m_id = value; }
}
