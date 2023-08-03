using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RebaClient : MonoBehaviour
{
    public string serverIP = "192.168.2.92"; 
    public int serverPort = 8888;
    public int bufferSize = 1024;

    private UdpClient udpClient;
    private IPEndPoint serverEndPoint;

    public VisualFeedback visualFeedback;
    public MusicCube musicCube;


    void Start()
    {
        udpClient = new UdpClient();
        serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

        udpClient.BeginReceive(ReceiveDataCallback, null);

        Debug.Log("Client connected to server: " + serverIP + ":" + serverPort);
    }

    private void ReceiveDataCallback(IAsyncResult ar)
    {
        try
        {
            byte[] receivedBytes = udpClient.EndReceive(ar, ref serverEndPoint);
            rebaScore = Encoding.UTF8.GetString(receivedBytes);

            int rebaScore;
            if (int.TryParse(receivedData, out rebaScore))
            {
                Debug.Log("Received REBA Score: " + rebaScore);
                visualFeedback.rebaScore = rebaScore;
                musicCube.REBA = rebaScore;
            } 
            else
            {
                Debug.LogError("Received invalid REBA Score: " + receivedData);
            }

            // Continue listening for REBA score
            udpClient.BeginReceive(ReceiveDataCallback, null);
        }
        catch (Exception e)
        {
            Debug.Log("Error while receiving data: " + e.Message);
        }
    }

    void OnDestroy()
    {
        udpClient.Close();
    }
}
