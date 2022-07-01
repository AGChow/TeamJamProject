using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryCreditsController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text totalTime;

    private bool sendingScore = false;

    void OnEnable()
    {
        StartCoroutine(WaitForCredits());
    }

    IEnumerator WaitForCredits() {
        yield return new WaitForSeconds(30f);
        ShowScoreInput();
    }

    void ShowScoreInput() {
        TimeSpan _timePlaying = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("CurrentTime"));
        totalTime.text = _timePlaying.ToString("mm':'ss'.'ff");
        totalTime.gameObject.SetActive(true);
    }

    public void SendScore() {
        if(nameInput.text.Length > 0 && nameInput.text.Length < 6 && !nameInput.text.Contains("*") && !sendingScore) {
            sendingScore = true;
            HighScores.UploadScore(nameInput.text, (Int32)(PlayerPrefs.GetFloat("CurrentTime") * 1000) * -1);
            StartCoroutine(WaitBeforeLoading());
        }
    }

    public void NoThanks() {
        SceneManager.LoadScene("StartScreen");
    }

    IEnumerator WaitBeforeLoading() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("StartScreen");
    }
}
