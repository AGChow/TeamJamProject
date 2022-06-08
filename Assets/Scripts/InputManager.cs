using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PauseMenu _pauseMenu;

    void Start()
    {
        _pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if(Input.GetButtonUp("Cancel"))
        {
            if(_pauseMenu.IsPaused())
            {
                _pauseMenu.UnPause();
            }
            else
            {
                _pauseMenu.Pause();
            }

            _pauseMenu.TogglePause();
        }
    }
}
