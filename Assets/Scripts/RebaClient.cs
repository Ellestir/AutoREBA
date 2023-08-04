using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RebaClient : MonoBehaviour
{
    public int port = 8888;
    public int bufferSize = 1024;

    private IPEndPoint serverEndPoint;
    private IPEndPoint endPoint;
    private UdpClient udpClient;

    public static int RebaScore;


    void Start()
    {
        endPoint = new IPEndPoint(IPAddress.Any, port);
        udpClient = new UdpClient(endPoint);
        

        udpClient.BeginReceive(ReceiveDataCallback, null);

        Debug.Log("Server started on port: " + port);
    }

    private void ReceiveDataCallback(IAsyncResult ar)
    {
        try
        {
            byte[] receivedBytes = udpClient.EndReceive(ar, ref serverEndPoint);
            String receivedData = Encoding.UTF8.GetString(receivedBytes);

            int rebaScore;


            if (int.TryParse(receivedData, out rebaScore))
            {
                Debug.Log("Received REBA Score: " + rebaScore);
                RebaScore = rebaScore;
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
