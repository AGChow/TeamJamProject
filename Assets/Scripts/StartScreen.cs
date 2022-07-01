using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartScreen : MonoBehaviour
{
    public Animator settingsMenuAnimator;
    private bool _isSettingsMenuActive = false;
    public Animator leaderboardMenuAnimator;
    private bool _isLeaderboardMenuActive = false;

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

    void Start() {
        List<Button> allButtons = GameObject.FindObjectsOfType<Button>().ToList();
        foreach(Button button in allButtons) {
            if(button.name != "CloseMenu") {
                button.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
                button.GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;
            }
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
        if(_isLeaderboardMenuActive) {
            leaderboardMenuAnimator.SetTrigger("SettingsExit");
            _isLeaderboardMenuActive = false;
        }
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

    public void Leaderboard()
    {
        if(_isSettingsMenuActive)
        {
            settingsMenuAnimator.SetTrigger("SettingsExit");
            _isSettingsMenuActive = false;
        }
        if(_isLeaderboardMenuActive)
        {
            leaderboardMenuAnimator.SetTrigger("SettingsExit");
        }
        else
        {
            leaderboardMenuAnimator.SetTrigger("SettingsEnter");
        }
        _isLeaderboardMenuActive = !_isLeaderboardMenuActive;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
