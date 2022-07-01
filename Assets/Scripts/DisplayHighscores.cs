using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour 
{
    public TMPro.TextMeshProUGUI[] rNames;
    public TMPro.TextMeshProUGUI[] rScores;
    HighScores myScores;

    void Start() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = i + 1 + ". Fetching...";
        }
        myScores = GetComponent<HighScores>();
        StartCoroutine("RefreshHighscores");
    }
    public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
    {
        highscoreList = highscoreList.OrderBy( x => x.score ).ToArray();
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = i + 1 + ". ";
            if (highscoreList.Length > i && highscoreList[i].score != 0 && !String.IsNullOrEmpty(highscoreList[i].username))
            {
                TimeSpan _timePlaying = TimeSpan.FromSeconds(highscoreList[i].score / 10000000);
                rScores[i].text = _timePlaying.ToString("mm':'ss");
                rNames[i].text = highscoreList[i].username;
            } else {
                rScores[i].text = String.Empty;
                rNames[i].text = String.Empty;
            }
        }
    }
    IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
    {
        while(true)
        {
            myScores.DownloadScores();
            yield return new WaitForSeconds(30);
        }
    }
}
