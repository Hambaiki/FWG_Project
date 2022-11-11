using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Welcome : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI text;

    public GameObject field;
    public GameObject enterButton;
    public GameObject beginButton;

    public static string playerName;

    void Start()
    {
        inputField.ActivateInputField();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendName();
        }
    }

    public void SendName()
    {
        playerName = inputField.text;
        string msg = "Welcome, ";
        text.text = msg + playerName;
        inputField.text = "";

        field.SetActive(false); 
        enterButton.SetActive(false);

        beginButton.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
