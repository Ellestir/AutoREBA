using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RebaServer : MonoBehaviour
{
    public int port = 8888;
    public int bufferSize = 1024;

    private UdpClient udpServer;
    private IPEndPoint endPoint;
    private string previousRebaScore;
    private AutoReba autoReba; 

    void Start()
    {
        endPoint = new IPEndPoint(IPAddress.Any, port);
        udpServer = new UdpClient(endPoint);

        previousRebaScore = autoReba.rebaScore;

        Debug.Log("Server started on port: " + port);   
    }

    void Update() {
        if (autoReba != null && previousRebaScore != autoReba.rebaScore)
        {
            previousRebaScore = autoReba.rebaScore;

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



