using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public Animator settingsMenuAnimator;
    private bool _isSettingsMenuActive = false;

    public void StartGame()
    {
        SceneManager.LoadScene("Test");
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
