using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WS_Client : MonoBehaviour
{
    WebSocket ws;

    string player1;
    string player2;

    void Start()
    {
        player1 = "Guide";

        ws = new WebSocket("ws://localhost:8080");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message recieved from " + ((WebSocket)sender).Url + ", Data: " + e.Data);
        };
        ws.Connect();
    }

    void Update()
    {
        if(ws == null)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send(player1);
        }
    }
}
