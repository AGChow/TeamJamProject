using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCreditsController : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(WaitForCredits());
    }

    IEnumerator WaitForCredits() {
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene("StartScreen");
    }
}
