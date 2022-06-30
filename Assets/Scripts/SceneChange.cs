using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public string nextScene;
    public GameObject CameraManager;

    public bool activated;

    private void Start()
    {
        CameraManager = FindObjectOfType<CameraManager>().gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && activated == false)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    public IEnumerator LoadNextScene()
    {
        if(Timer.instance != null)
            Timer.instance.PauseTimer();
            
        activated = true;
        FindObjectOfType<Player>().GetComponent<PlayerMovement>().SceneTransition();
        StartCoroutine(CameraManager.GetComponent<CameraManager>().ExitScene());
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(nextScene);
    }
    
}
