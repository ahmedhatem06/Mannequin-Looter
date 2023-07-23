using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public GameObject settings;
    public bool isSettingsOpen = false;

    [Header("Sprites")]
    public Sprite onSprite;
    public Sprite offSprite;

    [Header("Sound Variables")]
    public Image SoundButtonImage;
    public GameObject soundOnText;
    public GameObject soundOffText;
    private int soundRemainder = 0;

    [Header("Haptic Variables")]
    public Image HapticButtonImage;
    public GameObject HapticOnText;
    public GameObject HapticOffText;
    private int hapticRemainder;

    public event Action PauseTheGame;
    public event Action ResumeTheGame;
    private void Start()
    {
        CheckPlayerPrefs();
    }

    private void CheckPlayerPrefs()
    {
        if (PlayerPrefs.GetString("AudioListenerStatus") == "True")
        {
            SoundButtonImage.sprite = onSprite;
            soundOnText.SetActive(true);
            soundOffText.SetActive(false);
            soundRemainder = 0;
        }

        if (PlayerPrefs.GetString("AudioListenerStatus") == "False")
        {
            SoundButtonImage.sprite = offSprite;
            soundOnText.SetActive(false);
            soundOffText.SetActive(true);
            soundRemainder = 1;
        }

        if (PlayerPrefs.GetString("HapticStatus") == "True")
        {
            HapticButtonImage.sprite = onSprite;
            HapticOnText.SetActive(true);
            HapticOffText.SetActive(false);
            hapticRemainder = 0;
        }

        if (PlayerPrefs.GetString("HapticStatus") == "False")
        {
            HapticButtonImage.sprite = offSprite;
            HapticOnText.SetActive(false);
            HapticOffText.SetActive(true);
            hapticRemainder = 1;
        }
    }

    public void ClickOnOpenSettings()
    {
        settings.SetActive(true);
        isSettingsOpen = true;
        PauseGame();
    }

    public void ClickOnCloseSettings()
    {
        settings.SetActive(false);
        isSettingsOpen = false;
        ResumeGame();
    }

    public void PauseGame()
    {
        PauseTheGame?.Invoke();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        ResumeTheGame?.Invoke();
        Time.timeScale = 1;
    }

    public void ChangeSound()
    {
        if (soundRemainder % 2 == 0)
        {
            SoundButtonImage.sprite = offSprite;
            soundOnText.SetActive(false);
            soundOffText.SetActive(true);
            AudioManager.instance.AudioListenerStatus(false);
        }
        else
        {
            SoundButtonImage.sprite = onSprite;
            soundOnText.SetActive(true);
            soundOffText.SetActive(false);
            AudioManager.instance.AudioListenerStatus(true);
        }
        soundRemainder++;
    }

    public void ChangeHaptic()
    {
        if (hapticRemainder % 2 == 0)
        {
            HapticButtonImage.sprite = offSprite;
            HapticOnText.SetActive(false);
            HapticOffText.SetActive(true);
            PlayerPrefs.SetString("HapticStatus", "False");
        }
        else
        {
            HapticButtonImage.sprite = onSprite;
            HapticOnText.SetActive(true);
            HapticOffText.SetActive(false);
            PlayerPrefs.SetString("HapticStatus", "True");
        }
        hapticRemainder++;
        PlayerPrefs.Save();
    }
}
