using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RebaServer : MonoBehaviour
{
    public string serverIP = "192.168.2.92";
    public int serverPort = 8888;
    public int bufferSize = 1024;

    private UdpClient udpServer;
    private IPEndPoint endPoint;
    private string previousRebaScore;
    private string currentRebaScore;

    void Start()
    {
        udpServer = new UdpClient();
        endPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

        previousRebaScore = REBA_Score.Score.ToString();


        Debug.Log("Client connected to server: " + serverIP + ":" + serverPort);
    }

    void Update() {
        currentRebaScore = REBA_Score.Score.ToString();
        if (previousRebaScore != currentRebaScore)
        {
            previousRebaScore = currentRebaScore;

            // Send the updated REBA Score to the client
            SendRebaScoreToClient(previousRebaScore);
        }
    }

    private void SendRebaScoreToClient(string rebaScore)
    {
        try
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(rebaScore);
            udpServer.Send(messageBytes, messageBytes.Length, endPoint);
            Debug.Log("Sent REBA Score to client: " + rebaScore);
        }
        catch (Exception e)
        {
            Debug.Log("Error while sending data: " + e.Message);
        }
    }

    void OnDestroy()
    {
        udpServer.Close();
    }
}



