using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject settingsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Test");
    }

    public void Settings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
