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

    void Awake() {
        if(PlayerPrefs.HasKey("BestTime")) {
            bestTimeObject.SetActive(true);
            bestTime.text = PlayerPrefs.GetFloat("BestTime").ToString();
        }
    }

    public void StartGame()
    {
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
