using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PauseMenu _pauseMenu;
    private Animator _animator;

    void Start()
    {
        _animator = _pauseMenu.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonUp("Cancel"))
        {
            if(_pauseMenu.IsPaused())
            {
                _animator.SetTrigger("PauseMenuExit");
                Time.timeScale = 1f;
            }
            else
            {
                _animator.SetTrigger("PauseMenuEnter");
                Time.timeScale = 0f;
            }

            _pauseMenu.IsPaused(!_pauseMenu.IsPaused());
        }
    }
}
