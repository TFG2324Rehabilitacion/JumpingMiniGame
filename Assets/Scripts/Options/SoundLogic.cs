using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundLogic : MonoBehaviour
{
    public TextMeshProUGUI soundValueText;
    public int soundValue;
    public Slider slider;
    public float sliderValue;
    public Toggle mute;
    public float lastVolume;
    //public Image imageUnMute;
    // Start is called before the first frame update
    void Start()
    {
        mute.isOn = false;
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        lastVolume = slider.value;
        soundValue = Mathf.RoundToInt(sliderValue * 100);
        RevisarMute();
    }

    public void ChangeSlider(float valor)
    {
        lastVolume = sliderValue;
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        soundValue = Mathf.RoundToInt(sliderValue * 100);
        soundValueText.SetText($"{soundValue} %");
        RevisarMute();
    }

    public void RevisarMute()
    {
        if (sliderValue == 0)
        {
            mute.isOn = true;
        }
        else
        {
            mute.isOn = false;
        }
    }
}
