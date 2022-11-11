using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI message;
    public TextMeshProUGUI score;
    
    void Start()
    {
        string playerName = Welcome.playerName;
        message.text = "Congrats, " + playerName + "!\nYou Scored:";
        score.text = InputManager.score.ToString();
    }

    public void Retry()
    {
        InputManager.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
