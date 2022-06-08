using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused = false;
    private PlayerMovement _playerMovement;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Resume()
    {
        _isPaused = false;
        _animator.SetTrigger("PauseMenuExit");
        Time.timeScale = 1f;
    }

    public void Settings()
    {

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

    public IEnumerator EnableButtonAnimation(bool val)
    {
        yield return new WaitForSeconds(.3f);
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<Animator>().enabled = val;
        }
    }
}
