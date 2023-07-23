using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource soundEffects_AS;
    public AudioSource backgroundMusic_AS;
    public AudioClip[] soundEffects;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.GetString("AudioListenerStatus") == "True")
        {
            AudioListener.volume = 1;
        }

        if (PlayerPrefs.GetString("AudioListenerStatus") == "False")
        {
            AudioListener.volume = 0;
        }
    }

    public void PlaySoundEffect(int index)
    {
        soundEffects_AS.clip = soundEffects[index];
        soundEffects_AS.Play();
    }

    public void MuteBackgroundMusic(bool musicStatus)
    {
        backgroundMusic_AS.mute = musicStatus;
    }

    public void AudioListenerStatus(bool status)
    {
        if (status)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
        PlayerPrefs.SetString("AudioListenerStatus", status.ToString());
        PlayerPrefs.Save();
    }

}
