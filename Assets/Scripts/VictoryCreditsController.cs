using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryCreditsController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text totalTime;

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
        if(nameInput.text.Length > 0 && nameInput.text.Length < 6 && !nameInput.text.Contains("*")) {
            HighScores.UploadScore(nameInput.text, (Int32)(PlayerPrefs.GetFloat("CurrentTime") * 1000));
            SceneManager.LoadScene("StartScreen");
        }
    }

    public void NoThanks() {
        SceneManager.LoadScene("StartScreen");
    }
}
