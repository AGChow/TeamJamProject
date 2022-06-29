using System;
using System.Collections;
using UnityEngine;
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
        float startTime = Time.time;
        if(PlayerPrefs.HasKey("CurrentTime"))
            _timeElapsed = PlayerPrefs.GetFloat("CurrentTime");
        else
            _timeElapsed = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void PauseTimer() {
        _timerGoing = false;
        PlayerPrefs.SetFloat("CurrentTime", _timeElapsed);
    }

    public void StopAndRecordTime() {
        _timerGoing = false;
        if(!PlayerPrefs.HasKey("BestTime") || PlayerPrefs.GetFloat("CurrentTime") < PlayerPrefs.GetFloat("BestTime"))
            PlayerPrefs.SetFloat("BestTime", _timeElapsed);
    }

    IEnumerator UpdateTimer() {
        while(_timerGoing) {
            _timeElapsed += Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(_timeElapsed);
            timerText.text = _timePlaying.ToString("mm':'ss'.'ff");

            yield return null;
        }
    }
}
