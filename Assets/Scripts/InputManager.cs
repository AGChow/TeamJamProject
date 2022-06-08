using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PauseMenu _pauseMenu;
    private Animator _pauseAnimator;

    void Start()
    {
        _pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        _pauseAnimator = _pauseMenu.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonUp("Cancel"))
        {
            if(_pauseMenu.IsPaused())
            {
                _pauseAnimator.SetTrigger("PauseMenuExit");
                Time.timeScale = 1f;
            }
            else
            {
                _pauseAnimator.SetTrigger("PauseMenuEnter");
                Time.timeScale = 0f;
            }

            _pauseMenu.IsPaused(!_pauseMenu.IsPaused());
        }
    }
}
