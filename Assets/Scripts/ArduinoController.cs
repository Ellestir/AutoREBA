using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ArduinoController : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint endPoint;
    private string ip = "192.168.50.90"; // Ersetzen Sie dies durch die IP Ihres Arduino
    private int port = 4210; // Der Port, auf den Ihr Arduino hört

    private void Start()
    {
        endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        udpClient = new UdpClient();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            SendData("start vibration");
        }
    }

    private void SendData(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpClient.Send(data, data.Length, endPoint);
        }
        catch (Exception e)
        {
            Debug.Log("Could not send data: " + e.Message);
        }
    }

    private void OnDestroy()
    {
        udpClient.Close();
    }
}