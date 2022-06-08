using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public List<string> gameOverMessages = new();
    public TMP_Text gameOverText;

    void OnEnable()
    {
        int r = Random.Range(0, gameOverMessages.Count);
        gameOverText.text = gameOverMessages[r];
        GetComponent<Animator>().SetTrigger("ShowGameOver");
    }
}
