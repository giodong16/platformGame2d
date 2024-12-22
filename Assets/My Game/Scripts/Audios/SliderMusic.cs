using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMusic : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        if(slider != null)
        {
            slider.onValueChanged.AddListener(SettingValue);
        }
    }
    private void SettingValue(float t)
    {
        if (AudioManager.Instance != null && slider != null)
        {
            AudioManager.Instance.MusicVolume(slider.value);
        }
    }
}
