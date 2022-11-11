using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI scoreText;

    public static int score = 0;
    public static int increment = 1;

    void Start()
    {
        inputField.ActivateInputField();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer(inputField.text);
            resetField();

            inputField.ActivateInputField();
        }
    }

    public void CheckAnswer(string input)
    {
        WordManager wordManager = gameObject.GetComponent<WordManager>();
        if (wordManager.timeOut == false)
        {
            wordManager.FindWord(input);
        }   
    }

    public void resetField()
    {
        inputField.text = "";
    }

    public void updateScore()
    {
        score += increment;
        scoreText.text = score.ToString();
    }
}
