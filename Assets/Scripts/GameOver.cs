using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public List<string> gameOverMessages = new();
    public TMP_Text gameOverText;

    void OnEnable()
    {
        // select one game over message randomly
        int r = Random.Range(0, gameOverMessages.Count);
        gameOverText.text = gameOverMessages[r];
    }
}
