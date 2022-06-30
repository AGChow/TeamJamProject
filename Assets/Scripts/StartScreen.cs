using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScreen : MonoBehaviour
{
    public Animator settingsMenuAnimator;
    private bool _isSettingsMenuActive = false;

    public GameObject bestTimeObject;
    public TMP_Text bestTime;
    public GameObject timerObject;
    public AudioClip defaultMusic;

    void Awake() {
        if(PlayerPrefs.HasKey("BestTime")) {
            bestTimeObject.SetActive(true);
            TimeSpan _bestTimeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("BestTime"));
            bestTime.text = _bestTimeSpan.ToString("mm':'ss'.'ff");
        }
    }

    public void StartGame()
    {
        AudioManager.instance.musicAudioClip = defaultMusic;
        PlayerPrefs.SetFloat("CurrentTime", 0f);
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        if(_isSettingsMenuActive)
        {
            settingsMenuAnimator.SetTrigger("SettingsExit");
        }
        else
        {
            settingsMenuAnimator.SetTrigger("SettingsEnter");
        }
        _isSettingsMenuActive = !_isSettingsMenuActive;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
