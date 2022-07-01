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
        _playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    public void Resume()
    {
        TogglePause(false);
        _animator.SetTrigger("PauseMenuExit");
        CheckSettingsMenuClose();
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
        if(Timer.instance != null)
            Timer.instance.ResetToPrevious();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
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

    public void Pause()
    {
        TogglePause(true);
        _animator.SetTrigger("PauseMenuEnter");
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        _animator.SetTrigger("PauseMenuExit");
        CheckSettingsMenuClose();
        Time.timeScale = 1f;
        TogglePause(false);
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        _playerMovement.IsPaused(_isPaused);
    }

    public void TogglePause(bool val)
    {
        _isPaused = val;
        _playerMovement.IsPaused(_isPaused);
    }

    void CheckSettingsMenuClose()
    {
        if(_isSettingsMenuActive)
        {
            settingsMenuAnimator.SetTrigger("SettingsExit");
            _isSettingsMenuActive = false;
        }
    }
}
