using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 30;
    public static bool timerIsRunning = false;

    public TextMeshProUGUI timer;
    
    void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }
    
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timer.text = seconds.ToString();

            } else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timer.text = timeRemaining.ToString();
                timerIsRunning = false;

                // delete all entity with certain tag?
                WordManager wordManager = gameObject.GetComponent<WordManager>();
                wordManager.timeOut = true;
            }
        }
    }
}
