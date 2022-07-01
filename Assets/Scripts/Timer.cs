using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public TMP_Text timerText;
    private TimeSpan _timePlaying;
    private bool _timerGoing;
    private float _timeElapsed;

    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        _timerGoing = false;
    }

    public void StartTimer() {
        _timerGoing = true;
        CheckAndSetCurrentTime();
        StartCoroutine(UpdateTimer());
    }

    public void PauseTimer() {
        _timerGoing = false;
        PlayerPrefs.SetFloat("CurrentTime", _timeElapsed);
    }

    public void ResetToPrevious() {
        _timerGoing = false;
        CheckAndSetCurrentTime();
        UpdateTimerText();
    }

    public void StopAndRecordTime() {
        _timerGoing = false;
        if(!PlayerPrefs.HasKey("BestTime") || _timeElapsed < (PlayerPrefs.GetFloat("BestTime")))
            PlayerPrefs.SetFloat("BestTime", _timeElapsed);
    }

    IEnumerator UpdateTimer() {
        while(_timerGoing) {
            _timeElapsed += Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }
    }

    void UpdateTimerText() {
        _timePlaying = TimeSpan.FromSeconds(_timeElapsed);
        timerText.text = _timePlaying.ToString("mm':'ss'.'ff");
    }

    void CheckAndSetCurrentTime() {
        if(PlayerPrefs.HasKey("CurrentTime") && SceneManager.GetActiveScene().name != "StartScreen")
            _timeElapsed = PlayerPrefs.GetFloat("CurrentTime");
        else {
            _timeElapsed = 0f;
            PlayerPrefs.SetFloat("CurrentTime", _timeElapsed);
        }
    }

    public void ResetToZero() {
        _timerGoing = false;
        _timeElapsed = 0f;
        PlayerPrefs.SetFloat("CurrentTime", _timeElapsed);
        timerText.text = String.Empty;
    }
}
