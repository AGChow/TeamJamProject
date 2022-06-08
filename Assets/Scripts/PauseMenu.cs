using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator settingsMenuAnimator;
    private bool _isPaused = false;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    private bool _isSettingsMenuActive = false;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Resume()
    {
        _isPaused = false;
        _animator.SetTrigger("PauseMenuExit");
        if(_isSettingsMenuActive)
        {
            settingsMenuAnimator.SetTrigger("SettingsExit");
            _isSettingsMenuActive = false;
        }
        Time.timeScale = 1f;
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

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public bool IsPaused()
    {
        return _isPaused;
    }

    public void IsPaused(bool val)
    {
        _isPaused = val;
    }
}
