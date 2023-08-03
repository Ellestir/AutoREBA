using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RebaServer : MonoBehaviour
{
    public int port = 8888;
    public int bufferSize = 1024;

    private UdpClient udpClient;
    private IPEndPoint endPoint;

    void Start()
    {
        endPoint = new IPEndPoint(IPAddress.Any, port);
        udpServer = new UdpClient(endPoint);

        // Start listening for REBA Score
        udpServer.BeginReceive(ReceiveDataCallback, null);

        Debug.Log("Server started on port: " + port);   
    }

    private void ReceiveDataCallback(IAsyncResult ar)
    {
        try
        {
            byte[] receivedBytes = udpServer.EndReceive(ar, ref endPoint);
            string rebaScore = Encoding.UTF8.GetString(receivedBytes);

            Debug.Log("Received REBA Score:" + rebaScore);

            // Send the REBA Score 
            byte[] messageBytes = Encoding.UTF8.GetBytes(rebaScore);
            udpServer.Send(messageBytes, messageBytes.Length, endPoint);

            // Continue listening for REBA score 
            udpServer.BeginReceive(ReceiveDataCallback, null);
        }
        catch (Exception e)
        {
            Debug.Log("Error while receiving data: " + e.Message);
        }
    }

    void OnDestroy()
    {
        udpServer.Close();
    }
}



